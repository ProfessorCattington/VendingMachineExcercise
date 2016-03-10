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
                    digitalDisplay.UserInsertCoins(m_currentDeposit);
                    break;

                case Coin.Dime:
                    m_currentDeposit += .1m;
                    digitalDisplay.UserInsertCoins(m_currentDeposit);
                    break;

                case Coin.Quarter:
                    m_currentDeposit += .25m;
                    digitalDisplay.UserInsertCoins(m_currentDeposit);
                    break;

                default:

                    break;
            }
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

                //DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
                //digitalDisplay.SetMessage("INSERT COINS");
            }
        }

        public void UserPressedCoinReturn(){

            m_changeOnLastPurchase = m_currentDeposit;
            m_currentDeposit = 0;

            SpitOutChange();

            //DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            //digitalDisplay.SetMessage("INSERT COINS");

        }

        public string GetChangeOnLastPurchase(){

            return m_changeOnLastPurchase.ToString("C2");
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }

        public void SpitOutChange() { }
    }
}
