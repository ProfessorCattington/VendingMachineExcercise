using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS{

    [TestClass]
    public class DisplayChangerTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestDisplayStrategyDoesntImmediatelyRemoveExactChangeNotification(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedExactChangeOnlyProduct();

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            string testExactChange = "EXACT CHANGE ONLY";

            Assert.AreEqual(testExactChange, digitalDisplay.GetCurrentMessage());
        }
    }
}
