using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS { 

    [TestClass]
    public class DisplayExactChangeStrategyTests{

        private VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestDisplayStrategyDoesntImmediatelyRemoveExactChangeNotification(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedExactChangeOnlyProduct();

            DisplayExactChangeStrategy exactChangeStrategy = new DisplayExactChangeStrategy(digitalDisplay);

            string testExactChange = "EXACT CHANGE ONLY";

            Assert.AreEqual(testExactChange, digitalDisplay.GetCurrentMessage());
        }
        [TestMethod]
        public void TestExactChangeOnlyStrategyChangesMessageAfterAWaitWithDeposit(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedExactChangeOnlyProduct();

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            DisplayExactChangeStrategy exactChangeStrategy = new DisplayExactChangeStrategy(digitalDisplay);

            string testExactChange = "$0.25";

            Assert.AreEqual(testExactChange, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestExactChangeOnlyStrategyChangesMessageAfterAWaitWithoutDeposit(){

            testVendingMachineController = new VendingMachineController();
            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedExactChangeOnlyProduct();

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            DisplayExactChangeStrategy exactChangeStrategy = new DisplayExactChangeStrategy(digitalDisplay);

            string testMessage = "INSERT COINS";

            Assert.AreEqual(testMessage, digitalDisplay.GetCurrentMessage());
        }
    }
}
