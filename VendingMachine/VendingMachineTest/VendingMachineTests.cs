using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineNS;

namespace VendingMachineTestNS {

    [TestClass]
    public class VendingMachineTests
    {
        private VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestVendingMachineControllerObjectConstructor(){

            testVendingMachineController = new VendingMachineController();

            Assert.IsNotNull(testVendingMachineController.GetDigitalDisplay());
            Assert.IsNotNull(testVendingMachineController.GetCoinAccepter());
            Assert.IsNotNull(testVendingMachineController.GetControlPanel());
        }

        [TestMethod]
        public void TestIfVendingMachineComponentsHaveAReferenceToTheSameVendingMachineController() {

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();
            SnackBox snackBox = testVendingMachineController.GetSnackBox();

            Assert.AreEqual(testVendingMachineController, digitalDisplay.GetVendingMachineController());
            Assert.AreEqual(testVendingMachineController, coinAccepter.GetVendingMachineController());
            Assert.AreEqual(testVendingMachineController, controlPanel.GetVendingMachineController());
            Assert.AreEqual(testVendingMachineController, productDispenser.GetVendingMachineController());
            Assert.AreEqual(testVendingMachineController, snackBox.GetVendingMachineController());
        }
    }
}
