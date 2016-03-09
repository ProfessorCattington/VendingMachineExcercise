namespace VendingMachineNS
{
    public class CoinAccepter{

        private float m_currentDeposit = 0;
        private float m_changeOnLastPurchase = 0;

        public enum Coin{

            Penny,
            Nickle,
            Dime,
            Quarter,
            CanadianQuarter
        }

        public CoinAccepter(){


        }

        public void AcceptCoin(CoinAccepter.Coin coin){

            switch (coin){

                case Coin.Nickle:

                    m_currentDeposit += .05f;
                    break;

                case Coin.Dime:
                    m_currentDeposit += .1f;
                    break;

                case Coin.Quarter:
                    m_currentDeposit += .25f;
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
            }

        }

        public string GetChangeOnLastPurchase(){

            return m_changeOnLastPurchase.ToString("C2");
        }
    }
}
