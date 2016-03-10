
namespace VendingMachineNS { 

    public class ControlPanel{

        private const float m_candyPrice = 0.65f;
        private const float m_colaPrice = 1.0f;
        private const float m_chipsPrice = 0.5f;

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

        public void UserPushedAButton(buttons button){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
            float currentDeposit = coinAccepter.GetCurrentDeposit();

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();
            SnackBox snackBox = m_vendingMachineController.GetSnackBox();
            int productStock;

            switch (button)
            {

                case buttons.candy:

                    productStock = snackBox.GetProductStock("Candy");

                    if (productStock == 0){

                        digitalDisplay.UserSelectedASoldOutProduct();
                    }
                    else if (coinAccepter.WeHaveEnoughForChange(m_candyPrice)){

                        digitalDisplay.UserSelectedExactChangeOnlyProduct();
                    }
                    else if (currentDeposit < m_candyPrice){

                        digitalDisplay.UserHasntDepositedEnough(m_candyPrice.ToString("C2"));
                    }

                    else{

                        productDispenser.SetLastProductDispensed("Candy");
                        digitalDisplay.UserMadeAPurchase();
                        coinAccepter.CheckIfWeOweTheUserChange(m_candyPrice);
                    }
                    break;

                case buttons.chip:

                    productStock = snackBox.GetProductStock("Chips");

                    if (productStock == 0){

                        digitalDisplay.UserSelectedASoldOutProduct();
                    }
                    else if (coinAccepter.WeHaveEnoughForChange(m_chipsPrice)){

                        digitalDisplay.UserSelectedExactChangeOnlyProduct();
                    }
                    else if (currentDeposit < m_chipsPrice){

                        digitalDisplay.UserHasntDepositedEnough(m_chipsPrice.ToString("C2"));
                    }

                    else{

                        productDispenser.SetLastProductDispensed("Chips");
                        digitalDisplay.UserMadeAPurchase();
                        coinAccepter.CheckIfWeOweTheUserChange(m_chipsPrice);
                    }
                    break;

                case buttons.cola:

                    productStock = snackBox.GetProductStock("Cola");

                    if(productStock == 0){

                        digitalDisplay.UserSelectedASoldOutProduct();
                    }
                    else if (coinAccepter.WeHaveEnoughForChange(m_colaPrice)){

                        digitalDisplay.UserSelectedExactChangeOnlyProduct();
                    }
                    else if (currentDeposit < m_colaPrice){

                        digitalDisplay.UserHasntDepositedEnough(m_colaPrice.ToString("C2"));
                    }

                    else if (productStock > 0) { 

                        productDispenser.SetLastProductDispensed("Cola");
                        digitalDisplay.UserMadeAPurchase();
                        coinAccepter.CheckIfWeOweTheUserChange(m_colaPrice);
                    }
       
                    break;

                case buttons.coinReturn:

                    coinAccepter.UserPressedCoinReturn();
                    digitalDisplay.UserPressedCoinReturn();
                    productDispenser.SetLastProductDispensed("None");
                    break;

                default:
                    break;

            }
        }
        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
