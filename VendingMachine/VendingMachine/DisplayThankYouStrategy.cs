using VendingMachineNS;

namespace VendingMachineNS {

    public class DisplayThankYouStrategy {

        public DisplayThankYouStrategy(DigitalDisplay digitalDisplay) {

            DigitalDisplay.displayState currentDisplayState = digitalDisplay.GetCurrentState();

            long currentTime = System.DateTime.Now.Ticks;
            long lastDisplayMessageTime = digitalDisplay.GetLastMessageTime().Ticks;
            long elapsedTime = currentTime - lastDisplayMessageTime;

            System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

            if (timeSpan.Seconds > 3){
           
                currentDisplayState = DigitalDisplay.displayState.insertCoins;
                digitalDisplay.SetMessage("INSERT COINS");
            }
        }
    }
}
