
namespace VendingMachineNS { 

    public class VendingMachineController{

        private CoinAccepter m_coinAcceptor;
        private ControlPanel m_controlPanel;
        private DigitalDisplay m_digitalDisplay;
        private ProductDispenser m_productDispenser;

        public VendingMachineController(){

            m_coinAcceptor = new CoinAccepter();
            m_digitalDisplay = new DigitalDisplay(this);
            m_controlPanel = new ControlPanel(this);
            m_productDispenser = new ProductDispenser(this);

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

        public ProductDispenser GetProductDispenser() {

            return m_productDispenser;
        }
    }
}
