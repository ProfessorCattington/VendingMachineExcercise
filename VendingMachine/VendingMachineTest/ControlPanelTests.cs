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
            Product testProduct = snackBox.GetProductByName(testProductName);

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(testProduct);

            Assert.AreEqual(testProductName, productDispenser.GetLastProductDispensed());

            testProductName = "Candy";
            testProduct = snackBox.GetProductByName(testProductName);

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(testProduct);

            Assert.AreEqual(testProductName, productDispenser.GetLastProductDispensed());

            testProductName = "Chips";
            testProduct = snackBox.GetProductByName(testProductName);

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(testProduct);

            Assert.AreEqual(testProductName, productDispenser.GetLastProductDispensed());
        }
    }
}
