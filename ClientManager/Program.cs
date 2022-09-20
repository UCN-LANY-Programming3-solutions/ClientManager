using DataAccess;
using Model;

CustomerDao repository = new();

while (true)
{
    Splash();
    PrintCustomers(repository.GetAll());

    Console.WriteLine("Select a command:");
    Console.WriteLine(" [0] - exit program");
    Console.WriteLine(" [1] - add customer");
    Console.WriteLine(" [2] - show customer");
    Console.WriteLine();

    Console.Write(">");
    var input = Console.ReadLine();

    switch (input)
    {
        case "0":
        case "exit":
            return;
        case "1":
            Console.Write("Firstname >");
            var firstname = Console.ReadLine();

            Console.Write("Lastname  >");
            var lastname = Console.ReadLine();

            Console.Write("Address   >");
            var address = Console.ReadLine();

            Console.Write("Zip       >");
            var zip = Console.ReadLine();

            Console.Write("City      >");
            var city = Console.ReadLine();

            Console.Write("Phone     >");
            var phone = Console.ReadLine();

            Console.Write("Email     >");
            var email = Console.ReadLine();

            var cust = new Customer
            {
                Firstname = firstname,
                Lastname = lastname,
                Address = address,
                Zip = zip,
                City = city,
                Phone = phone,
                Email = email,
            };

            repository.Insert(cust);

            break;
        case "2":
            Console.WriteLine();
            Console.Write("Write ID of customer >");
            Console.WriteLine();
            if (Int32.TryParse(Console.ReadLine(), out int customerId))
            {
                Console.WriteLine(repository.GetById(customerId));
            }
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            break;
        default:
            break;
    }

    Console.Clear();
}
    

static void PrintCustomers(IEnumerable<Customer> customers)
{
    foreach (var customer in customers)
    {
        Console.WriteLine("[{0}] - {1} {2}", customer.Id, customer.Firstname, customer.Lastname);
    }
    Console.WriteLine();
}

static void Splash()
{
    Console.WriteLine("----------------------------");
    Console.WriteLine("* Client Manager v1        *");
    Console.WriteLine("----------------------------");
    Console.WriteLine();
}


