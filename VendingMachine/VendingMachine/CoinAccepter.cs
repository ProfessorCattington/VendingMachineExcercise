namespace VendingMachineNS
{
    public class CoinAccepter{

        private VendingMachineController m_vendingMachineController;

        private decimal m_currentDeposit = 0;
        private decimal m_bank;
        private decimal m_changeOnLastPurchase = 0;

        public enum Coin{

            Penny,
            Nickle,
            Dime,
            Quarter,
            CanadianQuarter
        }

        public CoinAccepter(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;
            m_bank = 1.00m;
        }

        public void AcceptCoin(CoinAccepter.Coin coin){

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();

            switch (coin){

                case Coin.Nickle:

                    m_currentDeposit += .05m;
                    break;

                case Coin.Dime:
                    m_currentDeposit += .1m;
                    break;

                case Coin.Quarter:
                    m_currentDeposit += .25m;
                    break;

                default:

                    break;
            }
            string formattedDepositString = m_currentDeposit.ToString("C2");
            DigitalDisplay.displayState coinInsertedState = DigitalDisplay.displayState.displayDeposit;
            digitalDisplay.SetMessageAndState(formattedDepositString, coinInsertedState);
        }

        public decimal GetCurrentDeposit(){

            return m_currentDeposit;
        }
        public void SetBankAmount(decimal bank){

            m_bank = bank;
        }

        public decimal GetBankAmount(){

            return m_bank;
        }

        public bool WeHaveEnoughForChange(decimal productCost){

            return (m_bank - productCost < 0) ? true : false;
        }

        public void CheckIfWeOweTheUserChange(decimal productCost){

            if(productCost < m_currentDeposit){

                m_changeOnLastPurchase = m_currentDeposit - productCost;
                m_currentDeposit = 0;

                SpitOutChange();
            }
        }

        public void UserPressedCoinReturn(){

            m_changeOnLastPurchase = m_currentDeposit;
            m_currentDeposit = 0;

            SpitOutChange();
        }

        public decimal GetChangeOnLastPurchase(){

            return m_changeOnLastPurchase;
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }

        public void SpitOutChange() {

            //dummy routine. 
        }
    }
}
