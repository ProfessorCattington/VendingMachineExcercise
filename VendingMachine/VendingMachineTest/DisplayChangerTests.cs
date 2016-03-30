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
            digitalDisplay.UserSelectedExactChangeOnlyProduct();

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
            digitalDisplay.UserSelectedExactChangeOnlyProduct();

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

            digitalDisplay.DisplayMessage();

            Assert.AreEqual(testExactChange, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestDisplayChangerChangesMessageAfterAWaitWithoutDepositOnExactChangePurchase(){

            testVendingMachineController = new VendingMachineController();
            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedExactChangeOnlyProduct();

            bool waiting = true;
            long startTime = System.DateTime.Now.Ticks;

            while (waiting){

                long currentTime = System.DateTime.Now.Ticks;
                System.TimeSpan timeSpan = new System.TimeSpan(currentTime - startTime);
                if (timeSpan.Seconds > 4){

                    waiting = false;
                }
            }
            string testMessage = "INSERT COINS";

            digitalDisplay.DisplayMessage();

            Assert.AreEqual(testMessage, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestDisplayChangerDoesntImmediatelyChangeDisplayOnceMoneyBeenDeposited(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            decimal testDeposit = 0.75m;
            digitalDisplay.UserInsertCoins(testDeposit);

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            Assert.AreEqual(testDeposit.ToString("C2"), digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestDisplayChangerCausesAmountDepositedToBeDisplayedAfterAWaitIfMoneyIsDepositedAndButtonPressed(){

            testVendingMachineController = new VendingMachineController();

            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            decimal testDeposit = .10m;

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();

            string testPrice = "$0.33";
            digitalDisplay.UserHasntDepositedEnough(testPrice);

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

            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayDeposit;

            Assert.AreEqual(testDeposit.ToString("C2"), digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerChangesDisplayAfterAWaitIfNoMoneyIsDeposited(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            string testPrice = "$0.99";
            digitalDisplay.UserHasntDepositedEnough(testPrice);

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

            DigitalDisplay.displayState testState = DigitalDisplay.displayState.insertCoins;

            Assert.AreEqual("INSERT COINS", digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerDoesntImmediatelyChangeDisplayMessageWhenSoldOutProductIsSelected(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedASoldOutProduct();

            DisplayChanger displayChanger = new DisplayChanger(digitalDisplay);

            string testSoldOut = "SOLD OUT";

            Assert.AreEqual(testSoldOut, digitalDisplay.GetCurrentMessage());
        }

        [TestMethod]
        public void TestDisplayChangerChangesMessageAfterWaitWithDepositandSoldOutProduct(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            
            CoinAccepter coinAccepter = testVendingMachineController.GetCoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            digitalDisplay.UserSelectedASoldOutProduct();

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
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.displayDeposit;

            Assert.AreEqual(testDeposit, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }

        [TestMethod]
        public void TestDisplayChangerChangesMessageAfterAShortWaitWithoutDeposit(){

            testVendingMachineController = new VendingMachineController();

            DigitalDisplay digitalDisplay = testVendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserSelectedASoldOutProduct();

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
            DigitalDisplay.displayState testState = DigitalDisplay.displayState.insertCoins;

            string testMessage = "INSERT COINS";

            Assert.AreEqual(testMessage, digitalDisplay.GetCurrentMessage());
            Assert.AreEqual(testState, digitalDisplay.GetCurrentState());
        }
    }
}
