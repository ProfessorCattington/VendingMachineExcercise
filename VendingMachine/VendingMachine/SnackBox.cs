using System.Collections.Generic;

namespace VendingMachineNS {

   public class SnackBox{

        private VendingMachineController m_vendingMachineController;

        private List<Product> m_products;

        public SnackBox(VendingMachineController vendingMachineController) {

            m_vendingMachineController = vendingMachineController;

            m_products = new List<Product>();
            m_products.Add(new Product("Cola", 1, 1));
            m_products.Add(new Product("Chips", .5m, 1));
            m_products.Add(new Product("Candy", .65m, 1));

        }

        public List<Product> GetProducts(){

            return m_products;
        }

        public Product GetProductByName(string name) {

            Product productToReturn = null;

            foreach(Product product in m_products){

                if(product.GetName() == name){
                    productToReturn = product;
                }
            }
            
            return productToReturn;
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
