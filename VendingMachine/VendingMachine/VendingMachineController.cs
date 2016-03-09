
namespace VendingMachineNS { 

    public class VendingMachineController{

        private CoinAccepter m_coinAcceptor;
        private ControlPanel m_controlPanel;
        private DigitalDisplay m_digitalDisplay;

        public VendingMachineController(){

            m_coinAcceptor = new CoinAccepter();
            m_digitalDisplay = new DigitalDisplay(this);
            m_controlPanel = new ControlPanel(this);

        }

        public CoinAccepter GetCoinAccepter(){

            return m_coinAcceptor;
        }

        public DigitalDisplay GetDigitalDisplay(){

            return m_digitalDisplay;
        }

        public ControlPanel GetControlPanel(){

            return m_controlPanel;
        }
    }
}
