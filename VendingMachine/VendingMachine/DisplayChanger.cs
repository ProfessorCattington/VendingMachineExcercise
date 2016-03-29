using VendingMachineNS;

namespace VendingMachineNS{

    public class DisplayChanger{

        private DigitalDisplay m_digitalDisplay;

        public DisplayChanger(DigitalDisplay digitalDisplay){

            m_digitalDisplay = digitalDisplay;
        }

        public DigitalDisplay GetDigitalDisplay(){

            return m_digitalDisplay;
        }
    }
}
