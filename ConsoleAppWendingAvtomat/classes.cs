namespace ConsoleAppWendingAvtomat;

public class Product {
    public string Name { get; }
    public int Price { get; }
    public int Count { get; set; }
    
    public Product(string name, int price, int count) {
        Name = name;
        Price = price;
        Count = count;
    }

    public string ShowProductInfo() {
        return ($"Name: {Name}, price: {Price}, count: {Count}");
    }
}

public class VendingMachine {
    private List<Product> _products;
    public int Profit { get; private set; } = 0;
    public int CountOfCoins1 { get; private set; } = 0;
    public int CountOfCoins2 { get; private set; } = 0;
    public int CountOfCoins5 { get; private set; } = 0;
    public int CountOfCoins10 { get; private set; } = 0;

    public void ResetProfit() {
        Profit = 0;
    }

    public void SetProfit(int profit) {
        Profit = Profit + profit;
    }
    
    public void SetAllCoins(int c1, int c2, int c5, int c10) {
        CountOfCoins1 = c1;
        CountOfCoins2 = c2;
        CountOfCoins5 = c5;
        CountOfCoins10 = c10;
    }

    public VendingMachine() {
        _products = new List<Product>();
    }

    public void AddProduct(string name, int price, int count) {
        _products.Add(new Product(name, price, count));
    }

    public List<Product> GetAllProducts() {
        return _products;
    }

    public void ClearAllMoney() {
        CountOfCoins1 = 0;
        CountOfCoins2 = 0;
        CountOfCoins5 = 0;
        CountOfCoins10 = 0;
    }

    public int CheckHowManyCoins() {
        return (CountOfCoins1 + CountOfCoins2 * 2 + CountOfCoins5 * 5 + CountOfCoins10 * 10);
    }

    public void AddCoins(int nominal, int count) {
        if (nominal == 1) {
            CountOfCoins1 += count;
        } else if (nominal == 2) {
            CountOfCoins2 += count;
        } else if (nominal == 5) {
            CountOfCoins5 += count;
        } else if (nominal == 10) {
            CountOfCoins10 += count;
        }
    }
    
    public void HelpInfo() {
        Console.WriteLine("If you want to exit the machine, enter \"e\"");
        Console.WriteLine("If you want admin mode, enter \"admin\"");
        Console.WriteLine("View list of all products, enter \"1\"");
        Console.WriteLine("Insert coins, enter \"2\"");
        Console.WriteLine("Select a product and receive it, enter \"3\"");
        Console.WriteLine("Return money, enter \"4\"");
    }
    
    public void ReturnChange() {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Nominal 1: " + CountOfCoins1);
        Console.WriteLine("Nominal 2: " + CountOfCoins2);
        Console.WriteLine("Nominal 5: " + CountOfCoins5);
        Console.WriteLine("Nominal 10: " + CountOfCoins10);
        Console.WriteLine("Your change: " + CheckHowManyCoins() + "\n");
        Console.ResetColor();
        ClearAllMoney();
    }
    
    public void RecalculationBalance(Product p) {
        int ostatok = CheckHowManyCoins() - p.Price;
        int countCoins1 = 0;
        int countCoins2 = 0;
        int countCoins5 = 0;
        int countCoins10 = 0;
        while (ostatok > 0) {
            if (ostatok >= 10) {
                countCoins10 += 1;
                ostatok -= 10;
            } else if (ostatok >= 5) {
                countCoins5 += 1;
                ostatok -= 5;
            } else if (ostatok >= 2) {
                countCoins2 += 1;
                ostatok -= 2;
            } else if (ostatok >= 1) {
                countCoins1 += 1;
                ostatok -= 1;
            }
        }
        SetAllCoins(countCoins1, countCoins2, countCoins5, countCoins10);
    }
    
    public void ChooseAndBuy() {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Enter name product:");
        string nameProduct = Console.ReadLine();
        List<Product> products = GetAllProducts();
        if (products.Count == 0) {
            Console.WriteLine("There is no products in the device");
        }
        foreach (var p in products) {
            if (p.Name == nameProduct) {
                if (CheckHowManyCoins() >= p.Price && p.Count > 0) {
                    Console.WriteLine("Congratulations, you bought: " + p.Name);
                    RecalculationBalance(p);
                    p.Count--;
                    SetProfit(p.Price);
                } else {
                    Console.WriteLine("Not enough money");  
                    break;
                }
            } else {
                Console.WriteLine("Such a product does not exist");
            }
        }
        Console.ResetColor();
    }
    
    public void AddNewProductToMachine() {
        Console.WriteLine("To add a new product, write down: name, price, count (on each new line)");
        string name;
        int price;
        int count_product;
        Console.WriteLine("Enter product name: ");
        while (true) {
            name = Console.ReadLine();
            if (name.Length > 0) {
                break;
            }
            Console.WriteLine("That name is incorrect, please try again");
        }

        Console.WriteLine("Enter product price: ");
        while (true) {
            price = Convert.ToInt32(Console.ReadLine());
            if (price > 0) {
                break;
            }
            Console.WriteLine("That price is negative, please try again");
        }

        Console.WriteLine("Enter count: ");
        while (true) {
            count_product = Convert.ToInt32(Console.ReadLine());
            if (count_product > 0) {
                break;
            }
            Console.WriteLine("That price is negative, please try again");
        }
        AddProduct(name, price, count_product);
        Console.WriteLine("Product added");
    }
    
    public void GetAllProductsInfo() {
        Console.ForegroundColor = ConsoleColor.Green;
        List<Product> products =  GetAllProducts();
        if (products.Count == 0) {
            Console.WriteLine("There is no products in the device");
        }
        foreach (Product p in products) {
            Console.WriteLine(p.ShowProductInfo());
        }
        Console.ResetColor();
    }
    
    public void AddCoinsForMachine() {
        Console.WriteLine("Enter nominal (1, 2, 5, 10): ");
        int nominal;
        while (true) {
            nominal = Convert.ToInt32(Console.ReadLine());
            if (nominal == 1 || nominal == 2 || nominal == 5 || nominal == 10) {
                break;    
            }
            Console.WriteLine("Such a denomination does not exist, try again: ");
        }
    
        Console.WriteLine("Enter count: ");
        int count;
        while (true) {
            count = Convert.ToInt32(Console.ReadLine());
            if (count > 0) {
                break;
            }
            Console.WriteLine("The count cannot be less than 0, try again: ");
        }
    
        AddCoins(nominal, count);
    }
}