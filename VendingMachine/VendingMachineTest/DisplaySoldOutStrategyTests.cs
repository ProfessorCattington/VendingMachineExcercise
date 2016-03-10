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
    }
}
