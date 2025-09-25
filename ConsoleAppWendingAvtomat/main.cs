using ConsoleAppWendingAvtomat;
VendingMachine machine = new VendingMachine();

void AdminPanel() {
    Console.WriteLine("Enter password:");
    string password = Console.ReadLine();
    if (password == "admin") {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("You are approved as an administrator, you can replenish the assortment or collect money");
        Console.ResetColor();
    } else {
        Console.WriteLine("The password is incorrect");
        return;
    }
    
    string line;
    while (true) {
        Console.WriteLine("If you want to change status, enter \"p\"");
        Console.WriteLine("Collect money, enter \"1\"");
        Console.WriteLine("Add new product \"2\"");
        line = Console.ReadLine();
        
        if (line == "1") {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You collected: " + machine.Profit);
            machine.ResetProfit();
            Console.ResetColor();
        } else if (line == "2") {
            machine.AddNewProductToMachine();
        } else if (line == "p") {
            break;
        }
    }
}

void GetLineForCommand() {
    string line;
    while (true) {
        machine.HelpInfo();
        line = Console.ReadLine();
        if (line == "1") {
            machine.GetAllProductsInfo();
        } else if (line == "2") {
            machine.AddCoinsForMachine();
        } else if (line == "3") {
            machine.ChooseAndBuy();
        } else if (line == "4") {
            machine.ReturnChange();
        } else if (line == "e") {
            return;
        } else if (line == "admin") {
            AdminPanel();
        }
    }
}

GetLineForCommand();