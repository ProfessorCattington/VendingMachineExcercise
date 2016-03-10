using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS{

    [TestClass]
    public class CandyButtonStrategyTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestButtonPressStrategyReferenceToVendingMachineController(){

            testVendingMachineController = new VendingMachineController();
            decimal testProductPrice = .5m;
            CandyButtonStrategy candyButtonStrategy = new CandyButtonStrategy(testVendingMachineController, testProductPrice);

            Assert.AreEqual(testVendingMachineController, candyButtonStrategy.GetVendingMachineController());
        }

        [TestMethod]
        public void TestStrategyHasProperOutcomeIfNoMoneyIsDeposited(){

            testVendingMachineController = new VendingMachineController();
            decimal testProductPrice = 1;
            CandyButtonStrategy candyButtonStrategy = new CandyButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "PRICE $1.00";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }
        [TestMethod]
        public void TestStrategyHasProperOutcomeIfProductIsOutOfStock(){

            testVendingMachineController = new VendingMachineController();
            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            snackBox.SetProductStock("Candy", 0);

            decimal testProductPrice = 1;
            CandyButtonStrategy candyButtonStrategy = new CandyButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "SOLD OUT";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());

        }
    }
}
