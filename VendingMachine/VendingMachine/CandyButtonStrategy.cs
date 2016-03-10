using VendingMachineNS;

namespace VendingMachineNS { }

    public class CandyButtonStrategy{

    private VendingMachineController m_vendingMachineController; 

    public CandyButtonStrategy(VendingMachineController vendingMachineController, decimal candyPrice){

        m_vendingMachineController = vendingMachineController;

        CoinAccepter coinAccepter = m_vendingMachineController.GetCoinAccepter();
        decimal currentDeposit = coinAccepter.GetCurrentDeposit();

        DigitalDisplay digitalDisplay = m_vendingMachineController.GetDigitalDisplay();
        ProductDispenser productDispenser = m_vendingMachineController.GetProductDispenser();
        SnackBox snackBox = m_vendingMachineController.GetSnackBox();
        int productStock = snackBox.GetProductStock("Candy");

        if (productStock == 0){

            digitalDisplay.UserSelectedASoldOutProduct();
        }
        else if (coinAccepter.WeHaveEnoughForChange(candyPrice)){

            digitalDisplay.UserSelectedExactChangeOnlyProduct();
        }
        else if (currentDeposit < candyPrice){

            digitalDisplay.UserHasntDepositedEnough(candyPrice.ToString("C2"));
        }
        else{

            productDispenser.SetLastProductDispensed("Candy");
            digitalDisplay.UserMadeAPurchase();
            coinAccepter.CheckIfWeOweTheUserChange(candyPrice);
        }
    }

    public VendingMachineController GetVendingMachineController(){

        return m_vendingMachineController;
    }
}
