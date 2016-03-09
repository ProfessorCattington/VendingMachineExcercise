namespace VendingMachineNS{

   public class DigitalDisplay {

        private CoinAccepter m_coinAccepter;
        private string m_displayDepositAmount;
        private string m_displayPriceAmount;

        public DigitalDisplay(CoinAccepter coinAccepter){

            m_coinAccepter = coinAccepter;
        }

        public string DisplayCurrentDeposit(){

            m_displayDepositAmount = m_coinAccepter.GetCurrentDeposit().ToString("C2");

            if(m_displayDepositAmount == "$0.00"){

                m_displayDepositAmount = "INSERT COIN";
            }

            return m_displayDepositAmount;
        }

        public void SetPrice(string price){

            m_displayPriceAmount = price;
        }

        public string GetPrice(){

            return m_displayPriceAmount;
        }
    }
}
