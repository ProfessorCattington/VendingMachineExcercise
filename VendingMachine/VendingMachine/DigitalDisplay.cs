namespace VendingMachineNS{

   public class DigitalDisplay {

        private VendingMachineController m_vendingMachineController;
        private string m_displayMessage = "INSERT COINS";
        private System.DateTime m_lastDisplayMessageTime;

        private displayState m_currentState = displayState.insertCoins;

        public enum displayState{

            thankYou,
            productSoldOut,
            displayPrice,
            insertCoins,
            exactChange,
            displayDeposit
        }

        public DigitalDisplay(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;
            m_lastDisplayMessageTime = System.DateTime.Now;
        }

        public void SetMessage(string message){

            m_displayMessage = message;
        }

        public void UserInsertCoins(decimal deposit){

            m_displayMessage = deposit.ToString("C2");
            m_currentState = displayState.displayDeposit;
        }

        public void UserMadeAPurchase(){

            m_displayMessage = "THANK YOU";
            m_currentState = displayState.thankYou;
            m_lastDisplayMessageTime = System.DateTime.Now;
        }

        public void UserSelectedASoldOutProduct(){

            m_displayMessage = "SOLD OUT";
            m_currentState = displayState.productSoldOut;
            m_lastDisplayMessageTime = System.DateTime.Now;
        }

        public void UserHasntDepositedEnough(string productPrice){

            m_displayMessage = "PRICE " + productPrice;
            m_currentState = displayState.displayPrice;
            m_lastDisplayMessageTime = System.DateTime.Now;
        }

        public void UserSelectedExactChangeOnlyProduct() {

            m_displayMessage = "EXACT CHANGE ONLY";
            m_currentState = displayState.exactChange;
            m_lastDisplayMessageTime = System.DateTime.Now;
        }

        public void UserPressedCoinReturn(){

            m_displayMessage = "INSERT COIN";
            m_currentState = displayState.insertCoins;
        }

        public string DisplayMessage(){

            System.DateTime buttonPressTime = System.DateTime.Now;
            long buttonPressElapsedTime = buttonPressTime.Ticks - m_lastDisplayMessageTime.Ticks;

            System.TimeSpan timeSpan = new System.TimeSpan(buttonPressElapsedTime);

            switch (m_currentState){

                case displayState.displayDeposit:

                    break;

                case displayState.insertCoins:

                   m_displayMessage = "INSERT COINS";

                   break;

                case displayState.displayPrice:

                    new DisplayPriceStrategy(this);

                    break;

                case displayState.thankYou:

                    new DisplayThankYouStrategy(this);

                    break;

                case displayState.productSoldOut:

                    new DisplaySoldOutStrategy(this);

                    break;

                case displayState.exactChange:

                    if(timeSpan.Seconds > 3){

                        CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
                        decimal depositAmount = coinAccepter.GetCurrentDeposit();

                        if (depositAmount > 0){

                            m_currentState = displayState.displayDeposit;
                            m_displayMessage = depositAmount.ToString("C2");
                        }
                        else{

                            m_currentState = displayState.insertCoins;
                            m_displayMessage = "INSERT COINS";
                        }

                    }
                    break;
            }

            return m_displayMessage;
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }

        public System.DateTime GetLastMessageTime(){

            return m_lastDisplayMessageTime;
        }

        public displayState GetCurrentState(){

            return m_currentState;
        }

        public string GetCurrentMessage(){

            return m_displayMessage;
        }
    }
}
