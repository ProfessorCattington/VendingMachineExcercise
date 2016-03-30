using VendingMachineNS;
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

            DisplayExactChangeStrategy exactChangeStrategy = new DisplayExactChangeStrategy(digitalDisplay);

            string testExactChange = "$0.25";

            Assert.AreEqual(testExactChange, digitalDisplay.GetCurrentMessage());
        }
    }
}
