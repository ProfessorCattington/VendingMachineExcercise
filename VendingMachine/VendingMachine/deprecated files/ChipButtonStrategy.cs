using VendingMachineNS;

namespace VendingMachineNS{

    public class ChipButtonStrategy{

        private VendingMachineController m_vendingMachineController;

        public ChipButtonStrategy(VendingMachineController vendingMachineController, decimal chipsPrice){

            m_vendingMachineController = vendingMachineController;

            m_vendingMachineController = vendingMachineController;

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
            decimal currentDeposit = coinAccepter.GetCurrentDeposit();

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();
            SnackBox snackBox = m_vendingMachineController.GetSnackBox();
            int productStock = snackBox.GetProductStock("Chips");

            if (productStock == 0){

                string message = "SOLD OUT";
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.displayPrice;
                digitalDisplay.SetMessageAndState(message, displayState);
            }
            else if (coinAccepter.WeHaveEnoughForChange(chipsPrice)){

                string message = "EXACT CHANGE ONLY";
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.displayPrice;
                digitalDisplay.SetMessageAndState(message, displayState);
            }
            else if (currentDeposit < chipsPrice){

                string message = "PRICE " + chipsPrice.ToString("C2");
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.displayPrice;
                digitalDisplay.SetMessageAndState(message, displayState);
            }
            else{

                productDispenser.SetLastProductDispensed("Chips");
                string displayMessage = "THANK YOU";
                DigitalDisplay.displayState displayState = DigitalDisplay.displayState.thankYou;
                digitalDisplay.SetMessageAndState(displayMessage, displayState);
                coinAccepter.CheckIfWeOweTheUserChange(chipsPrice);
            }
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
