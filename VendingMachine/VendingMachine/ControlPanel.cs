
namespace VendingMachineNS { 

    public class ControlPanel{

        private DigitalDisplay m_digitalDisplay;

        public ControlPanel(DigitalDisplay digitalDisplay) {

            m_digitalDisplay = digitalDisplay;
        }

        public void UserPushedCandyButton(){

            m_digitalDisplay.SetPrice("$0.65");
        }
    }
}
