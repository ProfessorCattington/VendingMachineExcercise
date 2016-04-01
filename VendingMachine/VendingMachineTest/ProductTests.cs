using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachineNS;

namespace VendingMachineTestNS{

    [TestClass]
    public class ProductTests{

        [TestMethod]
        public void TestProductConstructsProperly(){

            string testName = "CandyBar";
            decimal testPrice = .50m;
            int testStock = 10;

            Product candyBar = new Product(testName, testPrice,testStock);

            Assert.AreEqual(testName, candyBar.GetName());
            Assert.AreEqual(testPrice, candyBar.GetPrice());
            Assert.AreEqual(testStock, candyBar.GetStock());
        }
       
    }
}
