
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

            switch (button)
            {

                case buttons.candy:

                    if (currentDeposit < m_candyPrice){

                        digitalDisplay.SetMessage("PRICE " + m_candyPrice.ToString("C2"));
                    }

                    else{

                        productDispenser.SetLastProductDispensed("Candy");
                        digitalDisplay.UserMadeAPurchase();
                        coinAccepter.CheckIfWeOweTheUserChange(m_candyPrice);
                    }
                    break;

                case buttons.chip:

                    if (currentDeposit < m_chipsPrice){

                        digitalDisplay.SetMessage("PRICE " + m_chipsPrice.ToString("C2"));
                    }

                    else{

                        productDispenser.SetLastProductDispensed("Chips");
                        digitalDisplay.UserMadeAPurchase();
                        coinAccepter.CheckIfWeOweTheUserChange(m_chipsPrice);
                    }
                    break;

                case buttons.cola:

                    if (currentDeposit < m_colaPrice){

                        digitalDisplay.SetMessage("PRICE " + m_colaPrice.ToString("C2"));
                    }

                    else{

                        productDispenser.SetLastProductDispensed("Cola");
                        digitalDisplay.UserMadeAPurchase();
                        coinAccepter.CheckIfWeOweTheUserChange(m_colaPrice);
                    }
                    break;

                case buttons.coinReturn:

                    float noMoneySpent = 0;

                    coinAccepter.CheckIfWeOweTheUserChange(noMoneySpent);

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
