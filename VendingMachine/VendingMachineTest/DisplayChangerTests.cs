﻿using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS{

    [TestClass]
    public class DisplayChangerTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestDisplayChangerHasHandleOnCorrectDisplayObject(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            Assert.AreSame(digitalDisplay, displayChanger.GetDigitalDisplay());

        }

        [TestMethod]
        public void TestDisplayChangerDoesntImmediatelyRemoveExactChangeNotification(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "EXACT CHANGE ONLY";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayPrice;
            digitalDisplay.SetMessageAndState(testMessage, testState);

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            string testExactChange = "EXACT CHANGE ONLY";

            Assert.AreEqual(testExactChange, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestDisplayChangerChangesMessageAfterAWaitWithDepositOnExactChangerPurchase(){

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

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            Assert.AreEqual(testExactChange, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerChangesMessageAfterAWaitWithoutDepositOnExactChangePurchase(){

            testVendingMachineController = new VendingMachineController();
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

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            Assert.AreEqual(testMessage, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerDoesntImmediatelyChangeDisplayOnceMoneyBeenDeposited(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            decimal testDeposit = 0.75m;
            string testString = testDeposit.ToString("C2");
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayPrice;

            digitalDisplay.SetMessageAndState(testString, testState);

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            Assert.AreEqual(testDeposit.ToString("C2"), digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerCausesAmountDepositedToBeDisplayedAfterAWaitIfMoneyIsDepositedAndButtonPressed(){

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

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            testState = DigitalDisplay.displayState.displayDeposit;

            Assert.AreEqual(testDeposit.ToString("C2"), digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerChangesDisplayAfterAWaitIfNoMoneyIsDeposited(){

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

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            testState = DigitalDisplay.displayState.insertCoins;

            Assert.AreEqual("INSERT COINS", digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerDoesntImmediatelyChangeDisplayMessageWhenSoldOutProductIsSelected(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "SOLD OUT";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.productSoldOut;
            digitalDisplay.SetMessageAndState(testMessage, testState);

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            Assert.AreEqual(testMessage, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestDisplayChangerChangesMessageAfterWaitWithDepositandSoldOutProduct(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            string testMessage = "SOLD OUT";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.productSoldOut;
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

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);
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
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.productSoldOut;
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

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);
            testState = DigitalDisplay.displayState.insertCoins;

            testMessage = "INSERT COINS";

            Assert.AreEqual(testMessage, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerDoesntImmediatelyReplaceThankYouMessageOnPurchase(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "THANK YOU";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.thankYou;

            digitalDisplay.SetMessageAndState(testMessage, testState);

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            string testThanks = "THANK YOU";

            Assert.AreEqual(testThanks, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestDisplayChangerChangesThankYouMessageChangedAfterAShortWait(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testMessage = "THANK YOU";
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.thankYou;

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

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);
            testState = DigitalDisplay.displayState.insertCoins;

            Assert.AreEqual("INSERT COINS", digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }
    }
}
