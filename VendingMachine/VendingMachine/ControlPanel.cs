﻿
namespace VendingMachineNS { 

    public class ControlPanel{

        private const decimal m_candyPrice = 0.65m;
        private const decimal m_colaPrice = 1.0m;
        private const decimal m_chipsPrice = 0.5m;

        public enum buttons{

            candy,
            chip,
            cola,
            coinReturn
        }

        private VendingMachineController m_vendingMachineController;

        public ControlPanel(VendingMachineController vendingMachineController) {

            m_vendingMachineController = vendingMachineController;
        }

        public void UserPushedAButton(buttons button){

            CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
            decimal currentDeposit = coinAccepter.GetCurrentDeposit();

            DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
            ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();
            SnackBox snackBox = m_vendingMachineController.GetSnackBox();
            int productStock;

            switch (button){

                case buttons.candy:

                    new CandyButtonStrategy(m_vendingMachineController, m_candyPrice);

                    break;

                case buttons.chip:

                    new ChipButtonStrategy(m_vendingMachineController, m_chipsPrice);

                    break;

                case buttons.cola:

                    productStock = snackBox.GetProductStock("Cola");

                    if(productStock == 0){

                        digitalDisplay.UserSelectedASoldOutProduct();
                    }
                    else if (coinAccepter.WeHaveEnoughForChange(m_colaPrice)){

                        digitalDisplay.UserSelectedExactChangeOnlyProduct();
                    }
                    else if (currentDeposit < m_colaPrice){

                        digitalDisplay.UserHasntDepositedEnough(m_colaPrice.ToString("C2"));
                    }

                    else if (productStock > 0) { 

                        productDispenser.SetLastProductDispensed("Cola");
                        digitalDisplay.UserMadeAPurchase();
                        coinAccepter.CheckIfWeOweTheUserChange(m_colaPrice);
                    }
       
                    break;

                case buttons.coinReturn:

                    coinAccepter.UserPressedCoinReturn();
                    digitalDisplay.UserPressedCoinReturn();
                    productDispenser.SetLastProductDispensed("None");
                    break;

                default:
                    break;

            }
        }
        public VendingMachineController GetVendingMachineController(){

            return m_vendingMachineController;
        }
    }
}
