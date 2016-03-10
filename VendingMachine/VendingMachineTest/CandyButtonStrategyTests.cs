using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS{

    [TestClass]
    public class CandyButtonStrategyTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestButtonPressStrategyReferenceToVendingMachineController(){

            testVendingMachineController = new VendingMachineController();

            CandyButtonStrategy candyButtonStrategy = new CandyButtonStrategy(testVendingMachineController);

            Assert.AreEqual(testVendingMachineController, candyButtonStrategy.GetVendingMachineController());
        }
    }
}
