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

            float testBankValue = 1.0f;
            float testDepositValue = 0;
            string testLastChangeReturnedValue = "$0.00";

            Assert.AreEqual(testBankValue, coinAccepter.GetBankAmount());
            Assert.AreEqual(testDepositValue, coinAccepter.GetCurrentDeposit());
            Assert.AreEqual(testLastChangeReturnedValue, coinAccepter.GetChangeOnLastPurchase());
        }

        [TestMethod]
        public void TestVendingMachineAcceptsCoins(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            float testCurrentDeposit = .10f;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());

        }

        [TestMethod]
        public void TestVendingMachineCoinAccepterDoesNotAcceptInvalidCoins(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Penny);

            float testCurrentDeposit = 0;

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

            string testChangeReturned = "$0.50";

            Assert.AreEqual(testChangeReturned, coinAccepter.GetChangeOnLastPurchase());

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(ControlPanel.buttons.candy);

            testChangeReturned = "$0.35";

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

            string testChangeReturned = "$0.75";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testChangeReturned, coinAccepter.GetChangeOnLastPurchase());

            string testMessage = "INSERT COINS";

            Assert.AreEqual(testMessage, digitalDisplay.DisplayMessage());
        }
    }
}
