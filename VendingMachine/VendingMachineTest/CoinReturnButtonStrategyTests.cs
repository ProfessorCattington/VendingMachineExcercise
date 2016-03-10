using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS {

    [TestClass]
    public class CoinReturnButtonStrategyTests{

        private VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestCoinReturnButtonStrategyReferenceToVendingMachineController(){

            testVendingMachineController = new VendingMachineController();

            CoinReturnButtonStrategy coinReturnButtonStrategy = new CoinReturnButtonStrategy(testVendingMachineController);

            Assert.AreEqual(testVendingMachineController, coinReturnButtonStrategy.GetVendingMachineController());
        }
    }
}
