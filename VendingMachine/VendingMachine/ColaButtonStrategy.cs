using VendingMachineNS;

namespace VendingMachineNS {

    public class ColaButtonStrategy {

        private VendingMachineController m_vendingMachineController;

        public ColaButtonStrategy(VendingMachineController vendingMachineController, decimal colaPrice) {

            m_vendingMachineController = vendingMachineController;

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
            decimal currentDeposit = coinAccepter.GetCurrentDeposit();

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();
            SnackBox snackBox = m_vendingMachineController.GetSnackBox();
            int productStock = snackBox.GetProductStock("Cola");

            m_vendingMachineController = vendingMachineController;

            if (productStock == 0){

                string message = "SOLD OUT";
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.productSoldOut;
                digitalDisplay.SetMessageAndState(message, displayState);
            }
            else if (coinAccepter.WeHaveEnoughForChange(colaPrice)) { 
            
                digitalDisplay.UserSelectedExactChangeOnlyProduct();
            }
            else if (currentDeposit < colaPrice){

                string message = "PRICE " + colaPrice.ToString("C2");
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.displayPrice;
                digitalDisplay.SetMessageAndState(message, displayState);
            }
            else if (productStock > 0){

                productDispenser.SetLastProductDispensed("Cola");
                string displayMessage = "THANK YOU";
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.thankYou;
                digitalDisplay.SetMessageAndState(displayMessage, displayState);
                coinAccepter.CheckIfWeOweTheUserChange(colaPrice);
            }
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
