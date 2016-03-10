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
        [TestMethod]
        public void TestStrategyHasProperOutcomeIfExactChangeIsRequired(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.SetBankAmount(0);

            decimal testProductPrice = 2;
            ChipButtonStrategy candyButtonStrategy = new ChipButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "EXACT CHANGE ONLY";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }
        [TestMethod]
        public void TestStrategyHasProperOutcomeIfNoMoneyIsDeposited(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.SetBankAmount(2);

            decimal testProductPrice = 2;
            ChipButtonStrategy chipButtonStrategy = new ChipButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "PRICE $2.00";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }

    }
}
