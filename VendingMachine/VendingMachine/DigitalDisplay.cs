namespace VendingMachineNS{

   public class DigitalDisplay {

        private VendingMachineController m_vendingMachineController;
        private string m_displayDepositAmount;
        private string m_displayPriceAmount;

        public DigitalDisplay(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;
        }

        public string DisplayCurrentDeposit(){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();

            m_displayDepositAmount = coinAccepter.GetCurrentDeposit().ToString("C2");

            if(m_displayDepositAmount == "$0.00"){

                m_displayDepositAmount = "INSERT COIN";
            }

            return m_displayDepositAmount;
        }

        public void SetPrice(float price){

            m_displayPriceAmount = "PRICE " + price.ToString("C2");
        }

        public string GetPrice(){

            return m_displayPriceAmount;
        }
    }
}
