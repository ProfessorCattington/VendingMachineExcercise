
namespace VendingMachineNS { 

    public class ControlPanel{

        private const decimal m_candyPrice = 0.65m;
        private const decimal m_colaPrice = 1.0m;
        private const decimal m_chipsPrice = 0.5m;

        public enum buttons{

            candy,
            chip,
            cola,
            coinReturn
        }

        private VendingMachineController m_vendingMachineController;

        public ControlPanel(VendingMachineController vendingMachineController) {

            m_vendingMachineController = vendingMachineController;
        }

        public void UserPushedAButton(Product product){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
            decimal currentDeposit = coinAccepter.GetCurrentDeposit();

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();

            string productName = product.GetName();
            decimal productPrice = product.GetPrice();
            int productStock = product.GetStock();

            if (productStock == 0){

                string message = "SOLD OUT";
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.displayPrice;
                digitalDisplay.SetMessageAndState(message, displayState);
            }
            else if (coinAccepter.WeHaveEnoughForChange(productPrice)){

                string message = "EXACT CHANGE ONLY";
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.displayPrice;
                digitalDisplay.SetMessageAndState(message, displayState);
            }
            else if (currentDeposit < productPrice){

                string message = "PRICE " + productPrice.ToString("C2");
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.displayPrice;
                digitalDisplay.SetMessageAndState(message, displayState);
            }
            else{

                productDispenser.SetLastProductDispensed(productName);
                string displayMessage = "THANK YOU";
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.thankYou;
                digitalDisplay.SetMessageAndState(displayMessage, displayState);
                coinAccepter.CheckIfWeOweTheUserChange(productPrice);
            }
        }

        //switch (button){

        //    case buttons.candy:

        //        new CandyButtonStrategy(m_vendingMachineController, m_candyPrice);

        //        break;

        //    case buttons.chip:

        //        new ChipButtonStrategy(m_vendingMachineController, m_chipsPrice);

        //        break;

        //    case buttons.cola:

        //        new ColaButtonStrategy(m_vendingMachineController, m_colaPrice);

        //        break;

        //    case buttons.coinReturn:

        //        new CoinReturnButtonStrategy(m_vendingMachineController);

        //        break;

        //    default:
        //        break;

        //}
        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
