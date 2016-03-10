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
    }
}
