namespace VendingMachineNS{

   public class DigitalDisplay {

        private VendingMachineController m_vendingMachineController;
        private string m_displayDepositAmount;
        private string m_displayMessage;

        public DigitalDisplay(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;
        }

        public string DisplayCurrentDeposit(){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();

            m_displayDepositAmount = coinAccepter.GetCurrentDeposit().ToString("C2");

            if(m_displayDepositAmount == "$0.00"){

                m_displayDepositAmount = "INSERT COIN";
            }

            return m_displayDepositAmount;
        }

        public void SetMessage(float price){

            m_displayMessage = "PRICE " + price.ToString("C2");
        }

        public void SayThankYou(){

            m_displayMessage ="THANK YOU";
        }

        public string GetMessage(){

            return m_displayMessage;
        }
    }
}
