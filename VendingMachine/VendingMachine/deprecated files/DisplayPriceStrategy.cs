﻿using VendingMachineNS;

namespace VendingMachineNS {

    public class DisplayPriceStrategy{

        public DisplayPriceStrategy(DigitalDisplay digitalDisplay){

            DigitalDisplay.displayState currentDisplayState = digitalDisplay.GetCurrentState();

            long currentTime = System.DateTime.Now.Ticks;
            long lastDisplayMessageTime = digitalDisplay.GetLastMessageTime().Ticks;
            long elapsedTime = currentTime - lastDisplayMessageTime;

            System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

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
        }
    }
}
