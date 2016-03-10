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
        public void TestIfVendingMachineComponentsHaveAReferenceToTheSameVendingMachineController() {

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();
            SnackBox snackBox = testVendingMachineController.GetSnackBox();

            Assert.AreEqual(testVendingMachineController, digitalDisplay.GetVendingMachineController());
            Assert.AreEqual(testVendingMachineController, coinAccepter.GetVendingMachineController());
            Assert.AreEqual(testVendingMachineController, controlPanel.GetVendingMachineController());
            Assert.AreEqual(testVendingMachineController, productDispenser.GetVendingMachineController());
            Assert.AreEqual(testVendingMachineController, snackBox.GetVendingMachineController());
        }

        [TestMethod]
        public void TestProductDispenserHasNotDispensedOnInitialization() {

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

            string testDisplayOutput = "INSERT COINS";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestVendingMachineAcceptsCoins() {

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            float testCurrentDeposit = .10f;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());

        }

        [TestMethod]
        public void TestVendingMachineCoinAccepterDoesNotAcceptInvalidCoins() {

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Penny);

            float testCurrentDeposit = 0;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());

            coinAccepter.AcceptCoin(CoinAccepter.Coin.CanadianQuarter);

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());
        }

        [TestMethod]
        public void TestVendingMachineDisplayUpdatesWhenCoinIsAdded() {

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Nickle);

            string testDisplayOutput = "$0.05";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestVendingMachineControlPanelUpdatesDisplayWithPriceWhenNoMoneyIsDeposited() {

            testVendingMachineController = new VendingMachineController();

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.candy);

            string testDisplayOutput = "$0.65";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.DisplayMessage());

            testDisplayOutput = "$0.50";

            controlPanel.UserPushedAButton(ControlPanel.buttons.chip);
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.DisplayMessage());

            testDisplayOutput = "$1.00";

            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestVendingMachineDisplayDisplaysPriceWhenNotEnoughMoneyIsDeposited() {

            testVendingMachineController = new VendingMachineController();

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();

            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "PRICE $1.00";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestVendingMachineDispensesColaWhenEnoughMoneyIsDepositedAndColaButtonIsPressed() {

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
        public void TestVendingMachineThanksCustomerAfterAPurchase() {

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

            Assert.AreEqual(testMessage, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestAllProductsCanBePurchased() {

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

        [TestMethod]
        public void TestThankYouMessageDoesntPersist() {

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "THANK YOU";

            Assert.AreEqual(testMessage, digitalDisplay.DisplayMessage());

            System.DateTime purchaseTime = System.DateTime.Now;
            System.DateTime currentTime;

            bool waiting = true;
            while (waiting) {

                currentTime = System.DateTime.Now;
                long elapsedTime = currentTime.Ticks - purchaseTime.Ticks;

                System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

                if (timeSpan.Seconds > 4) {

                    waiting = false;
                }
            }

            testMessage = "INSERT COINS";
            Assert.AreEqual(testMessage, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestVendingMachineReturnsChangeToUser() {

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
        public void TestVendingMachineReturnsChangeToUserButDoesntDispenseProduct() { 

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.coinReturn);

            string testChangeReturned = "$1.00";

            Assert.AreEqual(testChangeReturned, coinAccepter.GetChangeOnLastPurchase());

            string testProductDispensed = "None";

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            Assert.AreEqual(testProductDispensed, productDispenser.GetLastProductDispensed());
        }


        [TestMethod]
        public void TestVendingMachineReturnsChangeIfNoPurchaseIsMade() {

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

        [TestMethod]
        public void TestVendingMachineDisplayNotifiesUserWhenProductIsSoldOut() {

            testVendingMachineController = new VendingMachineController();

            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            snackBox.SetProductStock("Cola", 0);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "SOLD OUT";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());

        }

        [TestMethod]
        public void TestSoldOutMessageDoesntPersistWithoutDeposit() {

            testVendingMachineController = new VendingMachineController();

            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            snackBox.SetProductStock("Cola", 0);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            System.DateTime purchaseTime = System.DateTime.Now;
            System.DateTime currentTime;

            bool waiting = true;
            while (waiting){

                currentTime = System.DateTime.Now;
                long elapsedTime = currentTime.Ticks - purchaseTime.Ticks;

                System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            string testDisplayOutput = "INSERT COINS";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());

        }

        [TestMethod]
        public void TestSoldOutMessageDoesntPersistWithDeposit(){

            testVendingMachineController = new VendingMachineController();

            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            snackBox.SetProductStock("Cola", 0);

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            System.DateTime purchaseTime = System.DateTime.Now;
            System.DateTime currentTime;

            bool waiting = true;
            while (waiting){

                currentTime = System.DateTime.Now;
                long elapsedTime = currentTime.Ticks - purchaseTime.Ticks;

                System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            string testDisplayOutput = "$0.50";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestVendingMachineNotifiesExactChangeOnly(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.SetBankAmount(0);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);
            
            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.chip);

            string testDisplayOutput = "EXACT CHANGE ONLY";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());


        }

        [TestMethod]
        public void TestVendingMachineNotifiesExactChangeOnlyWithoutDeposit(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.SetBankAmount(0);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            controlPanel.UserPushedAButton(ControlPanel.buttons.cola);

            string testDisplayOutput = "EXACT CHANGE ONLY";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());


        }
    }
}
