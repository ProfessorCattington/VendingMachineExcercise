using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS{

    [TestClass]
    public class DisplayChangerTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestDisplayChangerHasHandleOnCorrectDisplayObject(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            Assert.AreSame(digitalDisplay, displayChanger.GetDigitalDisplay());

        }

        [TestMethod]
        public void TestDisplayChangerDoesntImmediatelyRemoveExactChangeNotification(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedExactChangeOnlyProduct();

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            string testExactChange = "EXACT CHANGE ONLY";

            Assert.AreEqual(testExactChange, digitalDisplay.GetCurrentMessage());
        }
    }
}
