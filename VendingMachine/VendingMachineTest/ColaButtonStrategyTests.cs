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
        public void TestStrategyHasProperOutcomeIfExactChangeIsRequired(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.SetBankAmount(0);

            decimal testProductPrice = 2;
            ColaButtonStrategy colaButtonStrategy = new ColaButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "EXACT CHANGE ONLY";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }
        [TestMethod]
        public void TestStrategyHasProperOutcomeIfNoMoneyIsDeposited(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.SetBankAmount(2);

            decimal testProductPrice = 1.5m;
            ChipButtonStrategy chipButtonStrategy = new ChipButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "PRICE $1.50";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }
        [TestMethod]
        public void TestStrategyHasProperOutcomeIfCandyIsPuchased(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            decimal testProductPrice = .45m;
            ColaButtonStrategy colaButtonStrategy = new ColaButtonStrategy(testVendingMachineController, testProductPrice);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "THANK YOU";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            string testProductDispensed = "Cola";

            Assert.AreEqual(testProductDispensed, productDispenser.GetLastProductDispensed());

            decimal testChangeReturned = .05m;

            Assert.AreEqual(testChangeReturned, coinAccepter.GetChangeOnLastPurchase());

        }
    }
}
