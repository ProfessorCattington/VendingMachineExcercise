using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineNS
{



    public class CoinAccepter{

        private float m_currentDeposit = 0;

        public CoinAccepter(){


        }

        public void AcceptCoint(){

            m_currentDeposit += .10f;
        }

        public float GetCurrentDeposit(){

            return m_currentDeposit;
        }
    }
}
