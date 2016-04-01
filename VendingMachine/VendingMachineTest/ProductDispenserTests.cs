using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineNS;

namespace VendingMachineTestNS { 

    [TestClass]
    public class ProductDispenserTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestProductDispenserHasNotDispensedOnInitialization(){

            testVendingMachineController = new VendingMachineController();

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            string testLastDispensedProducted = "None";

            Assert.AreEqual(testLastDispensedProducted, productDispenser.GetLastProductDispensed());
        }

        [TestMethod]
        public void TestVendingMachineDispensesColaWhenEnoughMoneyIsDepositedAndColaButtonIsPressed(){

            testVendingMachineController = new VendingMachineController();

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            string testProductName = "Cola";

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            SnackBox snackbox = testVendingMachineController.GetSnackBox();
            Product testProduct = snackbox.GetProductByName(testProductName);
            controlPanel.UserPushedAButton(testProduct);

            Assert.AreEqual(testProduct, productDispenser.GetLastProductDispensed());
        }

        [TestMethod]
        public void TestVendingMachineReturnsChangeToUserButDoesntDispenseProduct(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            SnackBox snackbox = testVendingMachineController.GetSnackBox();
            Product testProduct = snackbox.GetProductByName("CoinReturn");
            controlPanel.UserPushedAButton(testProduct);

            decimal testChangeReturned = 1;

            Assert.AreEqual(testChangeReturned, coinAccepter.GetChangeOnLastPurchase());

            string testProductDispensed = "None";

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            Assert.AreEqual(testProductDispensed, productDispenser.GetLastProductDispensed());
        }
    }
}
