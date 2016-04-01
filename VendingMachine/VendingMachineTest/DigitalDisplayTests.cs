using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineNS;

namespace VendingMachineTestNS { 

    [TestClass]
    public class DigitalDisplayTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestVendingMachineDisplayUpdatesDisplayWhenNoCoinsAreInserted(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            string testDisplayOutput = "INSERT COINS";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestMessageAndStateSetter(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testString = "SOLD OUT";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayPrice;
            System.DateTime testTime = System.DateTime.Now;

            digitalDisplay.SetMessageAndState(testString, testState);

            Assert.AreEqual(testString, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
            Assert.AreEqual(testTime, digitalDisplay.GetLastMessageTime());

            System.DateTime currentTime;

            bool waiting = true;
            while (waiting){

                currentTime = System.DateTime.Now;
                long elapsedTime = currentTime.Ticks - testTime.Ticks;

                System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

                if (timeSpan.Seconds > 4){

                    waiting = false;
                    digitalDisplay.DisplayMessage();
                }
            }

            testString = "INSERT COINS";
            testState = DigitalDisplay.displayState.insertCoins;

            Assert.AreEqual(testString, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestVendingMachineDisplayUpdatesWhenCoinIsAdded(){

            testVendingMachineController = new VendingMachineController();
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Nickle);

            string testDisplayOutput = "$0.05";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestVendingMachineControlPanelUpdatesDisplayWithPriceWhenNoMoneyIsDeposited(){

            testVendingMachineController = new VendingMachineController();

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            SnackBox snackbox = testVendingMachineController.GetSnackBox();
            Product testProduct = snackbox.GetProductByName("Candy");
            controlPanel.UserPushedAButton(testProduct);

            string testDisplayOutput = "$0.65";

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.DisplayMessage());

            testDisplayOutput = "$0.50";

            testProduct = snackbox.GetProductByName("Chips");
            controlPanel.UserPushedAButton(testProduct);
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.DisplayMessage());

            testDisplayOutput = "$1.00";

            testProduct = snackbox.GetProductByName("Cola");
            controlPanel.UserPushedAButton(testProduct);
            Assert.AreEqual("PRICE " + testDisplayOutput, digitalDisplay.DisplayMessage());
        }

        [TestMethod]
        public void TestVendingMachineDisplayDisplaysPriceWhenNotEnoughMoneyIsDeposited(){

            testVendingMachineController = new VendingMachineController();

            ProductDispenser productDispenser = testVendingMachineController.GetProductDispenser();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            ControlPanel controlPanel = testVendingMachineController.GetControlPanel();
            SnackBox snackbox = testVendingMachineController.GetSnackBox();
            Product testProduct = snackbox.GetProductByName("Cola");
            controlPanel.UserPushedAButton(testProduct);

            controlPanel.UserPushedAButton(testProduct);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testDisplayOutput = "PRICE $1.00";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.DisplayMessage());
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

            Assert.AreEqual(testMessage, digitalDisplay.DisplayMessage());
        }


        [TestMethod]
        public void TestThankYouMessageDoesntPersist(){

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
            while (waiting){

                currentTime = System.DateTime.Now;
                long elapsedTime = currentTime.Ticks - purchaseTime.Ticks;

                System.TimeSpan timeSpan = new System.TimeSpan(elapsedTime);

                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            testMessage = "INSERT COINS";
            Assert.AreEqual(testMessage, digitalDisplay.DisplayMessage());
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

        [TestMethod]
        public void TestDisplayChangesMessageAfterAWaitWithDepositOnExactChangePurchase(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.SetBankAmount(0);
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Quarter);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "EXACT CHANGE ONLY";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayPrice;
            digitalDisplay.SetMessageAndState(testMessage, testState);

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }
            string testExactChange = "$0.25";
            testState = DigitalDisplay.displayState.displayDeposit;

            digitalDisplay.DisplayMessage();

            Assert.AreEqual(testExactChange, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangesMessageAfterAWaitWithoutDepositOnExactChangePurchase(){ 

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.SetBankAmount(0);

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "EXACT CHANGE ONLY";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayPrice;
            digitalDisplay.SetMessageAndState(testMessage, testState);

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }
            testMessage = "INSERT COINS";
            testState = DigitalDisplay.displayState.insertCoins;

            digitalDisplay.DisplayMessage();

            Assert.AreEqual(testMessage, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayCausesAmountDepositedToBeDisplayedAfterAWaitIfMoneyIsDepositedAndButtonPressed(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            decimal testDeposit = .10m;

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "PRICE $0.33";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayPrice;
            digitalDisplay.SetMessageAndState(testMessage, testState);

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            digitalDisplay.DisplayMessage();

            testState = DigitalDisplay.displayState.displayDeposit;

            Assert.AreEqual(testDeposit.ToString("C2"), digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangesMessageFromPriceToInsertCoinsAfterAWaitIfNoMoneyIsDeposited(){ 

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "PRICE $0.99";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayPrice;
            digitalDisplay.SetMessageAndState(testMessage, testState);

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            digitalDisplay.DisplayMessage();

            testState = DigitalDisplay.displayState.insertCoins;

            Assert.AreEqual("INSERT COINS", digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayDoesntImmediatelyChangeDisplayMessageWhenSoldOutProductIsSelected(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "SOLD OUT";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.productSoldOut;
            digitalDisplay.SetMessageAndState(testMessage, testState);

           digitalDisplay.DisplayMessage();

            Assert.AreEqual(testMessage, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestDisplayChangerChangesMessageAfterWaitWithDepositandSoldOutProduct(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            string testMessage = "SOLD OUT";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayPrice;
            digitalDisplay.SetMessageAndState(testMessage, testState);

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            digitalDisplay.DisplayMessage();

            string testDeposit = "$0.10";
            testState = DigitalDisplay.displayState.displayDeposit;

            Assert.AreEqual(testDeposit, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerChangesMessageAfterAShortWaitWithoutDeposit(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            string testMessage = "SOLD OUT";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayPrice;
            digitalDisplay.SetMessageAndState(testMessage, testState);

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }

            digitalDisplay.DisplayMessage();

            testState = DigitalDisplay.displayState.insertCoins;

            testMessage = "INSERT COINS";

            Assert.AreEqual(testMessage, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }
    }
}
