namespace VendingMachineNS{

   public class DigitalDisplay {

        private CoinAccepter m_coinAccepter;

        public DigitalDisplay(CoinAccepter coinAccepter){

            m_coinAccepter = coinAccepter;
        }

        public string GetDisplayOutput(){

            string formattedOutput = m_coinAccepter.GetCurrentDeposit().ToString("C2");

            return formattedOutput;
        }
    }
}
