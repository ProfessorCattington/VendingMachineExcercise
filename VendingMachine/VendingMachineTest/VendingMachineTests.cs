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
        public void TestProductDispenserHasNotDispensedOnInitialization(){

            testVendingMachineController = new VendingMachineController();

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            string testLastDispensedProducted = "None";

            Assert.AreEqual(testLastDispensedProducted, productDispenser.GetLastProductDispensed());
        }

        [TestMethod]
        public void TestVendingMachineDisplayUpdatesDisplayWhenNoCoinsAreInserted()
        {

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            string testDisplayOutput = "INSERT COIN";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayCurrentDeposit());
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
        public void TestVendingMachineControlPanelUpdatesDisplayWithPriceWhenNoMoneyIsDeposited(){

            testVendingMachineController = new VendingMachineController();

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.candy);

            string testDisplayOutput = "$0.65";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.GetMessage());

            testDisplayOutput = "$0.50";

            controlPanel.UserPushedAButton(ControlPanel.buttons.chip);
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.GetMessage());

            testDisplayOutput = "$1.00";

            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.GetMessage());
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

            string testProduct = "Cola";

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            Assert.AreEqual(testProduct, productDispenser.GetLastProductDispensed());
        }

        [TestMethod]
        public void TestVendingMachineThanksCustomerAfterAPurchase(){

            testVendingMachineController = new VendingMachineController();

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            string testMessage = "THANK YOU";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testMessage, digitalDisplay.GetMessage());
        }

        [TestMethod]
        public void TestAllProductsCanBePurchased(){

            testVendingMachineController = new VendingMachineController();

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();
  
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            string testProduct = "Cola";
            Assert.AreEqual(testProduct, productDispenser.GetLastProductDispensed());

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(ControlPanel.buttons.candy);

            testProduct = "Candy";
            Assert.AreEqual(testProduct, productDispenser.GetLastProductDispensed());

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            controlPanel.UserPushedAButton(ControlPanel.buttons.chip);

            testProduct = "Chips";
            Assert.AreEqual(testProduct, productDispenser.GetLastProductDispensed());
        }
    }
}
