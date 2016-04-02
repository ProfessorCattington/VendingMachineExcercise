using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTestNS{
    [TestClass]
    public class SnackBoxTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        public void TestSnackBoxConstructorsProperly(){

            testVendingMachineController = new VendingMachineController();
            SnackBox snackBox = testVendingMachineController.GetSnackBox();

            Assert.IsNotNull(snackBox);
            Assert.IsNotNull(snackBox.GetProducts());
        }

        [TestMethod]
        public void TestGetProductByNameReturnsCorrectProduct(){

            testVendingMachineController = new VendingMachineController();
            string testProductName = "Cola";

            SnackBox snackBox = testVendingMachineController.GetSnackBox();
            Product testProduct = snackBox.GetProductByName(testProductName);
            Assert.AreEqual(testProductName, testProduct.GetName());
        }
    }
}
