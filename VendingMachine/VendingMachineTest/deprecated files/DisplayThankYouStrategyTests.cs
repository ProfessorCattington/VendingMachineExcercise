using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS{

    [TestClass]
    public class DisplayThankYouStrategyTests{

        private VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestThankYouMessageDoesntImmediatelyDisapear(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserMadeAPurchase();

            DisplayThankYouStrategy thankYouStrategy = new DisplayThankYouStrategy(digitalDisplay);

            string testThanks = "THANK YOU";

            Assert.AreEqual(testThanks, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestThankYouMessageChangedAfterAShortWait(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserMadeAPurchase();

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
