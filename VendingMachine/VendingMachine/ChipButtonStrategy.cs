using VendingMachineNS;

namespace VendingMachineNS{

    public class ChipButtonStrategy{

        private VendingMachineController m_vendingMachineController;

        public ChipButtonStrategy(VendingMachineController vendingMachineController, decimal productPrice){

            m_vendingMachineController = vendingMachineController;

            m_vendingMachineController = vendingMachineController;

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
            decimal currentDeposit = coinAccepter.GetCurrentDeposit();

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();
            SnackBox snackBox = m_vendingMachineController.GetSnackBox();
            int productStock = snackBox.GetProductStock("Chips");

            if (productStock == 0){

                digitalDisplay.UserSelectedASoldOutProduct();
            }
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
