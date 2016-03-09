
namespace VendingMachineNS { 

    public class ControlPanel{

        private VendingMachineController m_vendingMachineController;

        public ControlPanel(VendingMachineController vendingMachineController) {

            m_vendingMachineController = vendingMachineController;
        }

        public void UserPushedCandyButton(){

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            
            digitalDisplay.SetPrice("$0.65");
        }
    }
}
