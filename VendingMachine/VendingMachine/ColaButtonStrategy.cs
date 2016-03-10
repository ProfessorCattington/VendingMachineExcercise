using VendingMachineNS;

namespace VendingMachineNS {

    public class ColaButtonStrategy {

        private VendingMachineController m_vendingMachineController;

        public ColaButtonStrategy(VendingMachineController vendingMachineController, decimal colaPrice) {

            m_vendingMachineController = vendingMachineController;
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
