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

        [TestMethod]
        public void TestInvalidProductNameThrowsException() {

            testVendingMachineController = new VendingMachineController();
            string testProductName = "Mountain Dew";

            SnackBox snackBox = testVendingMachineController.GetSnackBox();

            System.Exception testException = null;

            try{

                Product testProduct = snackBox.GetProductByName(testProductName);
            }
            catch (System.Exception e){

                testException = e;
            }

            Assert.IsNotNull(testException);
        }
    }
}
