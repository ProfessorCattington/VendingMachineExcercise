using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS { 

    [TestClass]
    public class ChipButtonStrategyTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestChipButtonStrategyReferenceIsCorrect(){

            testVendingMachineController = new VendingMachineController();
            decimal testProductPrice = .5m;
            ChipButtonStrategy chipButtonStrategy = new ChipButtonStrategy(testVendingMachineController, testProductPrice);

            Assert.AreEqual(testVendingMachineController,chipButtonStrategy.GetVendingMachineController());

        }
    }
}
