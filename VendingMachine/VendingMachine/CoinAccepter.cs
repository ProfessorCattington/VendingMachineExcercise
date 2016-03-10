namespace VendingMachineNS
{
    public class CoinAccepter{

        private VendingMachineController m_vendingMachineController;

        private float m_currentDeposit = 0;
        private float m_changeOnLastPurchase = 0;

        public enum Coin{

            Penny,
            Nickle,
            Dime,
            Quarter,
            CanadianQuarter
        }

        public CoinAccepter(VendingMachineController vendingMachineController){

            m_vendingMachineController = vendingMachineController;
        }

        public void AcceptCoin(CoinAccepter.Coin coin){

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();

            switch (coin){

                case Coin.Nickle:

                    m_currentDeposit += .05f;
                    digitalDisplay.UserInsertCoins(m_currentDeposit);
                    break;

                case Coin.Dime:
                    m_currentDeposit += .1f;
                    digitalDisplay.UserInsertCoins(m_currentDeposit);
                    break;

                case Coin.Quarter:
                    m_currentDeposit += .25f;
                    digitalDisplay.UserInsertCoins(m_currentDeposit);
                    break;

                default:

                    break;
            }
        }

        public float GetCurrentDeposit(){

            return m_currentDeposit;
        }

        public void CheckIfWeOweTheUserChange(float productCost){

            if(productCost < m_currentDeposit){

                m_changeOnLastPurchase = m_currentDeposit - productCost;
                m_currentDeposit = 0;

                DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
                digitalDisplay.SetMessage("INSERT COINS");
            }
        }

        public string GetChangeOnLastPurchase(){

            return m_changeOnLastPurchase.ToString("C2");
        }

        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
