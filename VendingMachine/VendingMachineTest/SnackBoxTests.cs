﻿using VendingMachineNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTest
{
    [TestClass]
    public class SnackBoxTests{

        VendingMachineController testVendingMachineController;

        [TestMethod]
        void TestSnackBoxConstructorsProperly(){

            testVendingMachineController = new VendingMachineController();
            SnackBox snackBox = testVendingMachineController.GetSnackBox();

            Assert.IsNotNull(snackBox);
            Assert.IsNotNull(snackBox.GetProducts());
        }

        [TestMethod]
        void TestProductListSetterSetsTheCorrectProduct()
        {

        }
    }
}
