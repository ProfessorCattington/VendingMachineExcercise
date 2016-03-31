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

            string message = "SOLD OUT";
            DigitalDisplay.displayState displayState = DigitalDisplay.displayState.productSoldOut;
            digitalDisplay.SetMessageAndState(message, displayState);
        }
        else if (coinAccepter.WeHaveEnoughForChange(candyPrice)){

            digitalDisplay.UserSelectedExactChangeOnlyProduct();
        }
        else if (currentDeposit < candyPrice){

            string message = "PRICE " + candyPrice.ToString("C2");
            DigitalDisplay.displayState displayState = DigitalDisplay.displayState.displayPrice;
            digitalDisplay.SetMessageAndState(message, displayState);
        }
        else{

            productDispenser.SetLastProductDispensed("Candy");
            string displayMessage = "THANK YOU";
            DigitalDisplay.displayState displayState = DigitalDisplay.displayState.thankYou;
            digitalDisplay.SetMessageAndState(displayMessage, displayState);
            coinAccepter.CheckIfWeOweTheUserChange(candyPrice);
        }
    }

    public VendingMachineController GetVendingMachineController(){

        return m_vendingMachineController;
    }
}
