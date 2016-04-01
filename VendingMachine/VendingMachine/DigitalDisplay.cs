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

        public void UserPressedCoinReturn(){

            m_displayMessage = "INSERT COIN";
            m_currentState = displayState.insertCoins;
        }

        public string DisplayMessage(){

            displayState currentDisplayState = GetCurrentState();

            string displayMessage;
            displayState newState;

            long currentTime = System.DateTime.Now.Ticks;
            long lastDisplayMessageTime = GetLastMessageTime().Ticks;
            long elapsedTime = currentTime - lastDisplayMessageTime;

            System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

            switch (currentDisplayState){

                case displayState.displayPrice:

                    if (timeSpan.Seconds > 3){

                        CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
                        decimal depositAmount = coinAccepter.GetCurrentDeposit();

                        if (depositAmount > 0){

                            newState = displayState.displayDeposit;
                            displayMessage = depositAmount.ToString("C2");
                            SetMessageAndState(displayMessage, newState);
                        }
                        else{

                            newState = displayState.insertCoins;
                            displayMessage = "INSERT COINS";
                            SetMessageAndState(displayMessage, newState);
                        }
                    }

                    break;

                case displayState.thankYou:

                    if (timeSpan.Seconds > 3){

                       newState = displayState.insertCoins;
                        displayMessage = "INSERT COINS";
                        SetMessageAndState(displayMessage, newState);
                    }

                    break;
                default:

                    break;
            }

            //SetCurrentState(currentDisplayState);
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
