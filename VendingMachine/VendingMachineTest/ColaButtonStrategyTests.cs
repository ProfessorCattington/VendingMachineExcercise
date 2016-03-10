using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS { 

    [TestClass]
    public class ColaButtonStrategyTests {

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestColaButtonStrategyReferenceToVendingMachineControllerIsCorrect() {

            testVendingMachineController = new VendingMachineController();
            decimal testProductPrice = 4;
            ColaButtonStrategy colaButtonStrategy = new ColaButtonStrategy(testVendingMachineController, testProductPrice);

            Assert.AreEqual(testVendingMachineController, colaButtonStrategy.GetVendingMachineController());
        }
    }
}
