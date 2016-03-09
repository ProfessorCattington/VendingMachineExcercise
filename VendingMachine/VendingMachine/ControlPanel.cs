
namespace VendingMachineNS { 

    public class ControlPanel{

        private const float m_candyPrice = 0.65f;
        private const float m_colaPrice = 1.0f;
        private const float m_chipsPrice = 0.5f;

        private VendingMachineController m_vendingMachineController;

        public ControlPanel(VendingMachineController vendingMachineController) {

            m_vendingMachineController = vendingMachineController;
        }

        public void UserPushedCandyButton(){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();

            float currentDeposit = coinAccepter.GetCurrentDeposit();

            if(currentDeposit < m_candyPrice){

                DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();

                digitalDisplay.SetMessage(m_candyPrice);
            }
        }

        public void UserPushedChipsButton(){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();

            float currentDeposit = coinAccepter.GetCurrentDeposit();

            if (currentDeposit < m_chipsPrice){

                DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
                digitalDisplay.SetMessage(m_chipsPrice);
            }

        }

        public void UserPushedColaButton()
        {

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();

            float currentDeposit = coinAccepter.GetCurrentDeposit();

            if (currentDeposit < m_colaPrice){

                DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
                digitalDisplay.SetMessage(m_colaPrice);
            }

            else {

                ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();
                productDispenser.SetLastProductDispensed("Cola");

                DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
                digitalDisplay.SayThankYou();

            }

        }
    }
}
