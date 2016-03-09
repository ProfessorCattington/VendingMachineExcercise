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
       public void TestVendingMachineDoesNotAcceptInvalidCoins(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Penny);

            float testCurrentDeposit = 0;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());

            coinAccepter.AcceptCoin(CoinAccepter.Coin.CanadianQuarter);

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());
        }

        [TestMethod]
        public void TestVendingMachineUpdatesDisplayWhenCoinIsAdded(){

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
        public void TestVendingMachineControlPanelUpdatesDisplayWithPrice(){

            testVendingMachineController = new VendingMachineController();

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedCandyButton();

            string testDisplayOutput = "$0.65";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            Assert.AreEqual(testDisplayOutput, digitalDisplay.GetPrice());
        }


    }
}
