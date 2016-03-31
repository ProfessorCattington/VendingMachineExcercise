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

        public void SetMessageAndState(string message, displayState state){

            m_displayMessage = message;
            m_currentState = state;
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
            m_currentState = displayState.displayPrice;
            m_lastDisplayMessageTime = System.DateTime.Now;
        }

        public void UserPressedCoinReturn(){

            m_displayMessage = "INSERT COIN";
            m_currentState = displayState.insertCoins;
        }

        public string DisplayMessage(){

            new DisplayChanger(this);
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

        public void SetCurrentState(displayState state){

            m_currentState = state;
        }

        public string GetCurrentMessage(){

            return m_displayMessage;
        }
    }
}
