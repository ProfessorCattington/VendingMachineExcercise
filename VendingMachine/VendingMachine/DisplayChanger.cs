using VendingMachineNS;

namespace VendingMachineNS{

    public class DisplayChanger{

        private DigitalDisplay m_digitalDisplay;

        public DisplayChanger(DigitalDisplay digitalDisplay){

            m_digitalDisplay = digitalDisplay;

            DigitalDisplay.displayState currentDisplayState = digitalDisplay.GetCurrentState();

            long currentTime = System.DateTime.Now.Ticks;
            long lastDisplayMessageTime = digitalDisplay.GetLastMessageTime().Ticks;
            long elapsedTime = currentTime - lastDisplayMessageTime;

            System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

            switch (currentDisplayState){

                case DigitalDisplay.displayState.exactChange:

                    if (timeSpan.Seconds > 3){

                        CoinAccepter coinAccepter = digitalDisplay.GetVendingMachineController().GetCoinAccepter();
                        decimal depositAmount = coinAccepter.GetCurrentDeposit();

                        if (depositAmount > 0){

                            currentDisplayState = DigitalDisplay.displayState.displayDeposit;
                            digitalDisplay.SetMessage(depositAmount.ToString("C2"));
                        }
                        else{

                            currentDisplayState = DigitalDisplay.displayState.insertCoins;
                            digitalDisplay.SetMessage("INSERT COINS");
                        }
                    }

                    break;

                case DigitalDisplay.displayState.insertCoins:

                    break;

                case DigitalDisplay.displayState.displayPrice:

                    if (timeSpan.Seconds > 3){

                        CoinAccepter coinAccepter = digitalDisplay.GetVendingMachineController().GetCoinAccepter();
                        decimal depositAmount = coinAccepter.GetCurrentDeposit();


                        if (depositAmount > 0){

                            currentDisplayState = DigitalDisplay.displayState.displayDeposit;
                            digitalDisplay.SetMessage(depositAmount.ToString("C2"));
                        }
                        else{

                            currentDisplayState = DigitalDisplay.displayState.insertCoins;
                            digitalDisplay.SetMessage("INSERT COINS");
                        }
                    }

                    break;

                case DigitalDisplay.displayState.thankYou:


                    break;

                case DigitalDisplay.displayState.productSoldOut:

                    if (timeSpan.Seconds > 3){

                        CoinAccepter coinAccepter = digitalDisplay.GetVendingMachineController().GetCoinAccepter();
                        decimal depositAmount = coinAccepter.GetCurrentDeposit();

                        if (depositAmount > 0){

                            currentDisplayState = DigitalDisplay.displayState.displayDeposit;
                            digitalDisplay.SetMessage(depositAmount.ToString("C2"));
                        }
                        else{

                            currentDisplayState = DigitalDisplay.displayState.insertCoins;
                            digitalDisplay.SetMessage("INSERT COINS");
                        }
                    }

                    break;

                default:

                    break;
            }

            digitalDisplay.SetCurrentState(currentDisplayState);
        }

        public DigitalDisplay GetDigitalDisplay(){

            return m_digitalDisplay;
        }
    }
}
