using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineNS;

namespace VendingMachineTest
{
    [TestClass]
    public class VendingMachineTests
    {
        private VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestVendingMachineControllerObjectConstructor()
        {

            testVendingMachineController = new VendingMachineController();

            Assert.IsNotNull(testVendingMachineController.GetDigitalDisplay());
            Assert.IsNotNull(testVendingMachineController.GetCoinAccepter());
            Assert.IsNotNull(testVendingMachineController.GetControlPanel());
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
        public void TestVendingMachineDisplayUpdatesWhenCoinIsAdded(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Nickle);

            string testDisplayOutput = "$0.05";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayCurrentDeposit());
        }

        [TestMethod]
        public void TestVendingMachineDisplayUpdatesDisplayWhenNoCoinsAreInserted(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            string testDisplayOutput = "INSERT COIN";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayCurrentDeposit());
        }

        [TestMethod]
        public void TestVendingMachineControlPanelUpdatesDisplayWithPriceWhenNoMoneyIsDeposited(){

            testVendingMachineController = new VendingMachineController();

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedCandyButton();

            string testDisplayOutput = "$0.65";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.GetPrice());

            testDisplayOutput = "$0.50";

            controlPanel.UserPushedChipsButton();
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.GetPrice());

            testDisplayOutput = "$1.00";

            controlPanel.UserPushedColaButton();
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.GetPrice());
        }

    }
}
