using System.Collections.Generic;

namespace VendingMachineNS {

   public class SnackBox{

        private VendingMachineController m_vendingMachineController;

        private Dictionary<string, int> m_vendingMachineProducts;

        public SnackBox(VendingMachineController vendingMachineController) {

            m_vendingMachineController = vendingMachineController;
            m_vendingMachineProducts = new Dictionary<string, int>();
            m_vendingMachineProducts.Add("Cola", 1);
        }

        public void SetProductStock(string product, int stock){

            m_vendingMachineProducts[product] = stock;
        }

        public int GetProductStock(string product){

            return m_vendingMachineProducts[product];
        }

        public Dictionary<string, int> GetProductDictionary(){

            return m_vendingMachineProducts;
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
