using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTest
{
    [TestClass]
    public class SnackBoxTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestVendingMachineDisplayNotifiesUserWhenProductIsSoldOut(){

            testVendingMachineController = new VendingMachineController();

            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            snackBox.SetProductStock("Cola", 0);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "SOLD OUT";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());

        }

        [TestMethod]
        public void TestSoldOutMessageDoesntPersistWithoutDeposit(){

            testVendingMachineController = new VendingMachineController();

            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            snackBox.SetProductStock("Cola", 0);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            System.DateTime purchaseTime = System.DateTime.Now;
            System.DateTime currentTime;

            bool waiting = true;
            while (waiting)
            {

                currentTime = System.DateTime.Now;
                long elapsedTime = currentTime.Ticks - purchaseTime.Ticks;

                System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

                if (timeSpan.Seconds > 4)
                {

                    waiting = false;
                }
            }

            string testDisplayOutput = "INSERT COINS";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());

        }

        [TestMethod]
        public void TestSoldOutMessageDoesntPersistWithDeposit(){

            testVendingMachineController = new VendingMachineController();

            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            snackBox.SetProductStock("Cola", 0);

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            System.DateTime purchaseTime = System.DateTime.Now;
            System.DateTime currentTime;

            bool waiting = true;
            while (waiting)
            {

                currentTime = System.DateTime.Now;
                long elapsedTime = currentTime.Ticks - purchaseTime.Ticks;

                System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

                if (timeSpan.Seconds > 4)
                {

                    waiting = false;
                }
            }

            string testDisplayOutput = "$0.50";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }
    }
}
