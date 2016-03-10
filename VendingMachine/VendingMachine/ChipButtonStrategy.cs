using VendingMachineNS;

namespace VendingMachineNS{

    public class ChipButtonStrategy{

        private VendingMachineController m_vendingMachineController;

        public ChipButtonStrategy(VendingMachineController vendingMachineController, decimal productPrice){

            m_vendingMachineController = vendingMachineController;

        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
