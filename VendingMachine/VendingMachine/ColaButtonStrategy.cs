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

                digitalDisplay.UserSelectedASoldOutProduct();
            }
            else if (coinAccepter.WeHaveEnoughForChange(colaPrice)) { 
            
                digitalDisplay.UserSelectedExactChangeOnlyProduct();
            }
            else if (currentDeposit < colaPrice){

                digitalDisplay.UserHasntDepositedEnough(colaPrice.ToString("C2"));
            }
            else if (productStock > 0){

                productDispenser.SetLastProductDispensed("Cola");
                digitalDisplay.UserMadeAPurchase();
                coinAccepter.CheckIfWeOweTheUserChange(colaPrice);
            }
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
