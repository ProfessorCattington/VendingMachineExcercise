
namespace VendingMachineNS { 

    public class ControlPanel{

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

        public void UserPushedCoinReturnButton(){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
            coinAccepter.UserPressedCoinReturn();

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserPressedCoinReturn();

            ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();
            productDispenser.SetLastProductDispensed("None");
        }
      
        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
