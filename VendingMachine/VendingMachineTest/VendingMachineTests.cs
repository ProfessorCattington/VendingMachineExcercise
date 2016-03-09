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
    }
}
