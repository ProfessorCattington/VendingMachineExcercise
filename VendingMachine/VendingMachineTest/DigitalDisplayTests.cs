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
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.productSoldOut;
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
        public void TestVendingMachineDisplayDisplaysPriceWhenNotEnoughMoneyIsDeposited(){

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
    }
}
