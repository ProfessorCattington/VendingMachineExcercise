using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS { 

    [TestClass]
    public class DisplaySoldOutStrategyTests{

        private VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestSoldOutStrategyDoesntImmediatelyChangeDisplayMessage(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedASoldOutProduct();

            DisplaySoldOutStrategy soldOutStrategy = new DisplaySoldOutStrategy(digitalDisplay);

            string testSoldOut = "SOLD OUT";

            Assert.AreEqual(testSoldOut, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestSoldOutStrategyChangesMessageAfterAShortWait(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedASoldOutProduct();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            DisplaySoldOutStrategy soldOutStrategy = new DisplaySoldOutStrategy(digitalDisplay);

            string testDeposit = "$0.10";

            Assert.AreEqual(testDeposit, digitalDisplay.GetCurrentMessage());
        }
    }
}
