using VendingMachineNS;

namespace VendingMachineNS { }

    public class CandyButtonStrategy{

    private VendingMachineController m_vendingMachineController; 

    public CandyButtonStrategy(VendingMachineController vendingMachineController){

        m_vendingMachineController = vendingMachineController;
    }

    public VendingMachineController GetVendingMachineController(){

        return m_vendingMachineController;
    }
}
