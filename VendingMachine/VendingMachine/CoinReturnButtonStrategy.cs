using VendingMachineNS;

namespace VendingMachineNS { 

    public class CoinReturnButtonStrategy{

        private VendingMachineController m_vendingMachineController;

        public CoinReturnButtonStrategy(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
