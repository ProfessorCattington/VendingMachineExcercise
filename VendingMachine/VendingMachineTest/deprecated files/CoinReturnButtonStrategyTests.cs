using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS {

    [TestClass]
    public class CoinReturnButtonStrategyTests{

        private VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestCoinReturnButtonStrategyReferenceToVendingMachineController(){

            testVendingMachineController = new VendingMachineController();

            CoinReturnButtonStrategy coinReturnButtonStrategy = new CoinReturnButtonStrategy(testVendingMachineController);

            Assert.AreEqual(testVendingMachineController, coinReturnButtonStrategy.GetVendingMachineController());
        }
        [TestMethod]
        public void TestCoinReturnButtonCausesCoinsToBeReturnedAndDisplayUpdateButNoProduct(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            CoinReturnButtonStrategy coinReturnButtonStrategy = new CoinReturnButtonStrategy(testVendingMachineController);

            decimal testMoneyReturned = .50m;

            Assert.AreEqual(testMoneyReturned, coinAccepter.GetChangeOnLastPurchase());

            string testDisplayOutput = "INSERT COIN";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());

            string testDispensedProduct = "None";

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            Assert.AreEqual(testDispensedProduct, productDispenser.GetLastProductDispensed());
        }
    }
}
