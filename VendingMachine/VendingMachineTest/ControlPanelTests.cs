using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineNS;
using System.Collections.Generic;

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
            Product testProduct = snackBox.GetProducts(testProductName);

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(testProduct);

            Assert.AreEqual(testProductName, productDispenser.GetLastProductDispensed());

            testProductName = "Candy";
            testProduct = snackBox.GetProducts(testProductName);

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(testProduct);

            Assert.AreEqual(testProductName, productDispenser.GetLastProductDispensed());

            testProductName = "Chips";
            testProduct = snackBox.GetProducts(testProductName);

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(testProduct);

            Assert.AreEqual(testProductName, productDispenser.GetLastProductDispensed());
        }
    }
}
