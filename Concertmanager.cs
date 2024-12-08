namespace Concert_CRUD_app;

public static class ConcertManager
{
    private static List<Concert> concerts = new List<Concert>();

    public static void AddConcert()
    {
        Console.Write("Enter location: ");
        string location = Console.ReadLine();

        Console.Write("Enter performer/ensemble: ");
        string performer = Console.ReadLine();

        Console.Write("Enter date and time (yyyy-mm-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dateAndTime))
        {
            Console.WriteLine("Invalid date and time format.");
            return;
        }

        Console.Write("Enter capacity: ");
        if (!int.TryParse(Console.ReadLine(), out int capacity))
        {
            Console.WriteLine("Invalid capacity.");
            return;
        }

        Console.Write("Enter price: ");
        if (!double.TryParse(Console.ReadLine(), out double price))
        {
            Console.WriteLine("Invalid price.");
            return;
        }

        int newId = concerts.Any() ? concerts.Max(c => c.Id) + 1 : 1;
        concerts.Add(new Concert
        {
            Id = newId,
            Location = location,
            Performer = performer,
            DateAndTime = dateAndTime,
            Capacity = capacity,
            Price = price
        });

        Console.WriteLine("Concerts added successfully!");
    }

    public static void ListConcerts()
    {
        if (concerts.Count == 0)
        {
            Console.WriteLine("No concerts available.");
            return;
        }

        Console.WriteLine("\nConcerts list:");
        foreach (var concert in concerts)
        {
            Console.WriteLine(concert);
        }
    }

    public static void EditConcert()
    {
        Console.WriteLine("Enter the ID of the concert to edit.");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var concert = concerts.FirstOrDefault(c => c.Id == id);
        if (concert == null)
        {
            Console.WriteLine("concert not found.");
            return;
        }

        Console.Write($"Enter new Location (current: {concert.Location}): ");
        string location = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(location)) concert.Location = location;

        Console.Write($"Enter new performer (current: {concert.Performer}): ");
        string performer = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(performer)) concert.Performer = performer;

        Console.WriteLine($"Enter new date and time (current: {concert.DateAndTime:yyyy-mm-dd HH:mm}): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dateAndTime)) concert.DateAndTime = dateAndTime;

        Console.Write($"Enter new capacity:  (current: {concert.Capacity}): ");
        if (!int.TryParse(Console.ReadLine(), out int capacity)) concert.Capacity = capacity;

        Console.Write($"Enter new price: (current: {concert.Price:C}): ");
        if (!double.TryParse(Console.ReadLine(), out double price)) concert.Price = price;

        Console.Write("Concerts updated successfully!");
    }

    public static void DeleteConcert()
    {
        Console.Write("Enter the ID of the concert to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var concert = concerts.FirstOrDefault(c => c.Id == id);
        if (concert == null)
        {
            Console.WriteLine("concert not found.");
            return;
        }

        concerts.Remove(concert);
        Console.WriteLine("Concerts deleted successfully!");
    }

    public static void SaveToFile()
    {
        using (var writer = new StreamWriter("Concerts.txt"))
        {
            foreach (var concert in concerts)
            {
                writer.WriteLine(
                    $"{concert.Id}|{concert.Location}|{concert.Performer}|{concert.DateAndTime}|{concert.Capacity}|{concert.Price}");
            }
        }

        Console.WriteLine("Data saved!");
    }

    public static void LoadFromFile()
    {
        if (!File.Exists(@"Concerts.txt"))
        {
            Console.WriteLine("No saved concerts found.");
            return;
        }

        concerts.Clear();
        foreach (var line in File.ReadAllLines(@"Concerts.txt"))
        {
            var parts = line.Split('|');
            concerts.Add(new Concert
            {
                Id = int.Parse(parts[0]),
                Location = parts[1],
                Performer = parts[2],
                DateAndTime = DateTime.Parse(parts[3]),
                Capacity = int.Parse(parts[4]),
                Price = double.Parse(parts[5]),
            });
        }
    }
}

