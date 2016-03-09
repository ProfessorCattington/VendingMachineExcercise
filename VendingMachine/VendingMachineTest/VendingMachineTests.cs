using System;
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
            coinAccepter.AcceptCoint(CoinAccepter.Coin.Dime);

            float testCurrentDeposit = .10f;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());

        }

       [TestMethod]
       public void TestVendingMachineDoesNotAcceptPennies(){

            CoinAccepter coinAccepter = new CoinAccepter();
            coinAccepter.AcceptCoint(CoinAccepter.Coin.Penny);

            float testCurrentDeposit = 0;

            Assert.AreEqual(testCurrentDeposit, coinAccepter.GetCurrentDeposit());
        }
    }
}
