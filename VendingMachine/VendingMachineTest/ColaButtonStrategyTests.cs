using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS { 

    [TestClass]
    public class ColaButtonStrategyTests {

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestColaButtonStrategyReferenceToVendingMachineControllerIsCorrect() {

            testVendingMachineController = new VendingMachineController();
            decimal testProductPrice = 4;
            ColaButtonStrategy colaButtonStrategy = new ColaButtonStrategy(testVendingMachineController, testProductPrice);

            Assert.AreEqual(testVendingMachineController, colaButtonStrategy.GetVendingMachineController());
        }

        [TestMethod]
        public void TestStrategyOutputIfColaIsOutOfStock(){

            testVendingMachineController = new VendingMachineController();
            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            snackBox.SetProductStock("Cola", 0);

            decimal testProductPrice = 2;
            ColaButtonStrategy ccolaButtonStrategy = new ColaButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "SOLD OUT";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }
        [TestMethod]
        public void TestStrategyHasProperOutcomeIfExactChangeIsRequired()
        {

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.SetBankAmount(0);

            decimal testProductPrice = 2;
            ColaButtonStrategy colaButtonStrategy = new ColaButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "EXACT CHANGE ONLY";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }
    }
}
