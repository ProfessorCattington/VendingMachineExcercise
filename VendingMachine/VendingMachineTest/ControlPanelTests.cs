using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineNS;

namespace VendingMachineTestNS { 

    [TestClass]
    public class ControlPanelTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestAllProductsCanBePurchased(){

            testVendingMachineController = new VendingMachineController();

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            SnackBox snackBox = testVendingMachineController.GetSnackBox();

            string testProductName = "Cola";

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(testProductName);

            Assert.AreEqual(testProductName, productDispenser.GetLastProductDispensed());

            testProductName = "Candy";

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(testProductName);

            Assert.AreEqual(testProductName, productDispenser.GetLastProductDispensed());

            testProductName = "Chips";

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(testProductName);

            Assert.AreEqual(testProductName, productDispenser.GetLastProductDispensed());
        }

        [TestMethod]
        public void TestCoinReturnButtonReturnsCoins(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedCoinReturnButton();

            decimal testDeposit = 0;
            string testProduct = "None";

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            Assert.AreEqual(testDeposit, coinAccepter.GetCurrentDeposit());
            Assert.AreEqual(testProduct, productDispenser.GetLastProductDispensed());
        }
    }
}
