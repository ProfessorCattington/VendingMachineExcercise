namespace VendingMachineNS{

   public class DigitalDisplay {

        private VendingMachineController m_vendingMachineController;
        private string m_displayMessage = "INSERT COINS";
        private System.DateTime m_lastDisplayMessageTime;

        private displayState m_currentState = displayState.insertCoins;

        private enum displayState{

            madeAPurchase,
            productSoldOut,
            displayPrice,
            insertCoins,
            displayDeposit
        }

        public DigitalDisplay(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;
        }

        private string DisplayCurrentDeposit(){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
            string displayDepositAmount = coinAccepter.GetCurrentDeposit().ToString("C2");

            return displayDepositAmount;
        }

        public void SetMessage(string message){

            m_displayMessage = message;
        }

        public void UserInsertCoins(float deposit){

            m_displayMessage = deposit.ToString("C2");
            m_currentState = displayState.displayDeposit;
        }

        public void UserMadeAPurchase(){

            m_displayMessage = "THANK YOU";
            m_currentState = displayState.madeAPurchase;
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

                    if(timeSpan.Seconds < 3){

                        //do nothing. the output string was set earlier
                    }
                    else{

                        CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
                        float depositAmount = coinAccepter.GetCurrentDeposit();

                        if (depositAmount > 0){

                            m_currentState = displayState.displayDeposit;
                            m_displayMessage = depositAmount.ToString("C2");
                        }
                        else {

                            m_currentState = displayState.insertCoins;
                            m_displayMessage = "INSERT COINS";
                        }
                    }

                    break;

                case displayState.madeAPurchase:

                    if (timeSpan.Seconds < 3){

                    }
                    else{

                        m_currentState = displayState.insertCoins;
                        m_displayMessage = "INSERT COINS";
                    }

                    break;

                case displayState.productSoldOut:

                    if (timeSpan.Seconds < 3){

                      
                    }
                    else{

                        CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
                        float depositAmount = coinAccepter.GetCurrentDeposit();

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
    }
}
