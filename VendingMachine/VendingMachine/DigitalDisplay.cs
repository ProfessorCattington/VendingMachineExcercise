namespace VendingMachineNS{

   public class DigitalDisplay {

        private VendingMachineController m_vendingMachineController;
        private string m_displayMessage = "INSERT COINS";
        private bool m_userMadeAPurchase = false;
        private bool m_productWasSoldOut = false; // move to a state machine if we need to look at another bool
        private System.DateTime m_lastDisplayMessageTime;

        public DigitalDisplay(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;
        }

        public string DisplayCurrentDeposit(){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();

            string displayDepositAmount = coinAccepter.GetCurrentDeposit().ToString("C2");

            if (displayDepositAmount == "$0.00"){

                displayDepositAmount = "INSERT COINS";
            }

            return displayDepositAmount;
        }

        public void SetMessage(string message){

            m_displayMessage = message;
        }

        public void UserMadeAPurchase(){

            m_displayMessage = "THANK YOU";
            m_userMadeAPurchase = true;
            m_lastDisplayMessageTime = System.DateTime.Now;
        }

        public void UserSelectedASoldOutProduct(){

            m_displayMessage = "SOLD OUT";
        }

        public string DisplayMessage(){

            if (m_userMadeAPurchase){

                System.DateTime currentTime = System.DateTime.Now;
                long elapsedTime = currentTime.Ticks - m_lastDisplayMessageTime.Ticks;

                System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

                if (timeSpan.Seconds < 3){

                    return "THANK YOU";
                }
                else{

                    m_userMadeAPurchase = false;
                    m_displayMessage = "INSERT COINS";
                }
            }

            return m_displayMessage;
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
