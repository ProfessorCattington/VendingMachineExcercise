namespace VendingMachineNS {

    public class Product {

        private string m_name;
        private decimal m_price;
        private int m_stock;

        public Product(string name, decimal price, int stock) {

            m_name = name;
            m_price = price;
            m_stock = stock;
        }

        public string GetName(){ return m_name; }
        public decimal GetPrice() { return m_price; }
        public int GetStock() { return m_stock; }

        public void SetStock(int amount){
            m_stock = amount;
        }
    }
}
