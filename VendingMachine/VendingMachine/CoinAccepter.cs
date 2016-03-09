namespace VendingMachineNS
{
    public class CoinAccepter{

        private float m_currentDeposit = 0;

        public enum Coin{

            Penny = 1,
            Nickle = 5,
            Dime = 10,
            Quarter = 25
        }

        public CoinAccepter(){


        }

        public void AcceptCoint(CoinAccepter.Coin coin){

            if(coin != CoinAccepter.Coin.Penny){

                float depositedCoinValue = (float)coin / 100;
       
                m_currentDeposit += depositedCoinValue;
            }
        }

        public float GetCurrentDeposit(){

            return m_currentDeposit;
        }
    }
}
