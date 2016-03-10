
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

        public void UserPushedAButton(buttons button){

            switch (button){

                case buttons.candy:

                    new CandyButtonStrategy(m_vendingMachineController, m_candyPrice);

                    break;

                case buttons.chip:

                    new ChipButtonStrategy(m_vendingMachineController, m_chipsPrice);

                    break;

                case buttons.cola:

                    new ColaButtonStrategy(m_vendingMachineController, m_colaPrice);
       
                    break;

                case buttons.coinReturn:

                    new CoinReturnButtonStrategy(m_vendingMachineController);

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
