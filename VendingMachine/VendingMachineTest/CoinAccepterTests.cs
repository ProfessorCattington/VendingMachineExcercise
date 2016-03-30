using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineNS;

namespace VendingMachineTestNS { 

    [TestClass]
    public class CoinAccepterTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void CoinAccepterIsConstructedProperly(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            decimal testBankValue = 1.0m;
            decimal testDepositValue = 0;
            decimal testLastChangeReturnedValue = 0;

            Assert.AreEqual(testBankValue, coinAccepter.GetBankAmount());
            Assert.AreEqual(testDepositValue, coinAccepter.GetCurrentDeposit());
            Assert.AreEqual(testLastChangeReturnedValue, coinAccepter.GetChangeOnLastPurchase());
        }

        [TestMethod]
        public void TestVendingMachineAcceptsCoins(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            decimal testCurrentDeposit = .10m;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());

        }

        [TestMethod]
        public void TestVendingMachineCoinAccepterDoesNotAcceptInvalidCoins(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Penny);

            decimal testCurrentDeposit = 0;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());

            coinAccepter.AcceptCoin(CoinAccepter.Coin.CanadianQuarter);

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());
        }

        [TestMethod]
        public void TestVendingMachineReturnsChangeToUser(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.chip);

            decimal testChangeReturned = .50m;

            Assert.AreEqual(testChangeReturned, coinAccepter.GetChangeOnLastPurchase());

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(ControlPanel.buttons.candy);

            testChangeReturned = .35m;

            Assert.AreEqual(testChangeReturned, coinAccepter.GetChangeOnLastPurchase());
        }

        [TestMethod]
        public void TestVendingMachineReturnsChangeIfNoPurchaseIsMade(){

            testVendingMachineController = new VendingMachineController();

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.coinReturn);

            decimal testChangeReturned = .75m;

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testChangeReturned, coinAccepter.GetChangeOnLastPurchase());

            string testMessage = "INSERT COIN";

            Assert.AreEqual(testMessage, digitalDisplay.DisplayMessage());
        }
    }
}
