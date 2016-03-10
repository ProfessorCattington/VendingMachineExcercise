using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS { 

    [TestClass]
    public class ChipButtonStrategyTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestChipButtonStrategyReferenceToVendingMachineControllerIsCorrect(){

            testVendingMachineController = new VendingMachineController();
            decimal testProductPrice = .5m;
            ChipButtonStrategy chipButtonStrategy = new ChipButtonStrategy(testVendingMachineController, testProductPrice);

            Assert.AreEqual(testVendingMachineController, chipButtonStrategy.GetVendingMachineController());

        }

        [TestMethod]
        public void TestStrategyHasProperOutcomeIfProductIsOutOfStock(){

            testVendingMachineController = new VendingMachineController();
            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            snackBox.SetProductStock("Chips", 0);

            decimal testProductPrice = 2;
            ChipButtonStrategy chipButtonStrategy = new ChipButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "SOLD OUT";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());

        }
    }
}
