
namespace VendingMachineNS { 

    public class VendingMachineController{

        private CoinAccepter m_coinAcceptor;
        private DigitalDisplay m_digitalDisplay;

        public VendingMachineController(){

            m_coinAcceptor = new CoinAccepter();
            m_digitalDisplay = new DigitalDisplay(m_coinAcceptor);

        }

        public CoinAccepter GetCoinAccepter(){

            return m_coinAcceptor;
        }

        public DigitalDisplay GetDigitalDisplay(){

            return m_digitalDisplay;
        }
    }
}
