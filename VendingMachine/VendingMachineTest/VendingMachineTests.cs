using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineNS;

namespace VendingMachineTest
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void TestVendingMachineAcceptsCoins(){

            CoinAccepter coinAccepter = new CoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Dime);

            float testCurrentDeposit = .10f;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());

        }

       [TestMethod]
       public void TestVendingMachineDoesNotAcceptInvalidCoins(){

            CoinAccepter coinAccepter = new CoinAccepter();
            coinAccepter.AcceptCoin(CoinAccepter.Coin.Penny);

            float testCurrentDeposit = 0;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());

            coinAccepter.AcceptCoin(CoinAccepter.Coin.CanadianQuarter);

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());
        }

        [TestMethod]
        public void TestVendingMachineUpdatesDisplayWhenCoinIsAdded(){

            CoinAccepter coinAccepter = new CoinAccepter();
            DigitalDisplay digitalDisplay = new DigitalDisplay(coinAccepter);

            coinAccepter.AcceptCoin(CoinAccepter.Coin.Nickle);

            string testDisplayOutput = "$0.05";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.GetDisplayOutput());
        }

        [TestMethod]
        public void TestVendingMachineDisplayUpdatesDisplayWhenNoCoinsAreInserted(){

            CoinAccepter coinAccepter = new CoinAccepter();
            DigitalDisplay digitalDisplay = new DigitalDisplay(coinAccepter);

            string testDisplayOutput = "INSERT COIN";

            Assert.AreEqual(testDisplayOutput, digitalDisplay.GetDisplayOutput());
        }

        [TestMethod]
        public void TestVendingMachineControllerObjectConstructor(){
                        
            VendingMachineController vendingMachineController = new VendingMachineController();

            Assert.IsNotNull(vendingMachineController.GetDigitalDisplay());
            Assert.IsNotNull(vendingMachineController.GetCoinAccepter());
            Assert.IsNotNull(vendingMachineController.GetControlPanel());
        }

        [TestMethod]
        public void TestVendingMachineControlPanelUpdatesDisplayWithPrice(){

        
        }


    }
}
