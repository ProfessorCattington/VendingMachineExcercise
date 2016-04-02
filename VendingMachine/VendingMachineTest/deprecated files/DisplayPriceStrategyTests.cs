using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS {

    [TestClass]
    public class DisplayPriceStrategyTests{

        private VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestDisplayStrategyDoesntImmediatelyChangeDisplay(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            string testPrice = "$0.75";
            digitalDisplay.UserHasntDepositedEnough(testPrice);

            DisplayPriceStrategy displayPriceStrategy = new DisplayPriceStrategy(digitalDisplay);

            Assert.AreEqual("PRICE " + testPrice, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestDisplayStrategyChangesDisplayAfterAShortWait(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            string testPrice = "$0.99";
            digitalDisplay.UserHasntDepositedEnough(testPrice);

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            DisplayPriceStrategy displayPriceStrategy = new DisplayPriceStrategy(digitalDisplay);

            Assert.AreEqual("INSERT COINS", digitalDisplay.GetCurrentMessage());
        }
    }
}
