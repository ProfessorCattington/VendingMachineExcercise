using VendingMachineNS;

namespace VendingMachineNS { 

    public class CoinReturnButtonStrategy{

        private VendingMachineController m_vendingMachineController;

        public CoinReturnButtonStrategy(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
            coinAccepter.UserPressedCoinReturn();

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            digitalDisplay.UserPressedCoinReturn();

            ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();
            productDispenser.SetLastProductDispensed("None");
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
