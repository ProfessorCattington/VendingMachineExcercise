
namespace VendingMachineNS{

    public class ProductDispenser{

        private VendingMachineController m_vendingMachineController;
        private string m_lastProductDispensed = "None";

        public ProductDispenser(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;
        }

        public string GetLastProductDispensed(){

            return m_lastProductDispensed;
        }
    }
}
