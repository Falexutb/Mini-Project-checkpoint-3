using System;
using System.Collections.Generic;

// Base class representing an asset
class Asset
{
    // Properties common to all assets
    public string Brand { get; set; }
    public string ModelName { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal Price { get; set; }
    public int EndOfLifeYears { get; } = 3;

    // Constructor for initializing common properties
    public Asset(string brand, string modelName, DateTime purchaseDate, decimal price)
    {
        Brand = brand;
        ModelName = modelName;
        PurchaseDate = purchaseDate;
        Price = price;
    }

    // Virtual method for displaying asset details
    public virtual void Display()
    {
        Console.WriteLine($"Type: {GetType().Name}\tBrand: {Brand}\tModel: {ModelName}\tPurchase Date: {PurchaseDate.ToShortDateString()}\tPrice: {Price:C}\tEnd of Life: {PurchaseDate.AddYears(EndOfLifeYears).ToShortDateString()}");
    }

    // Method to get color code based on end-of-life duration
    public ConsoleColor GetColorCode()
    {
        TimeSpan timeUntilEndOfLife = PurchaseDate.AddYears(EndOfLifeYears) - DateTime.Now;

        if (timeUntilEndOfLife < TimeSpan.FromDays(90))
        {
            return ConsoleColor.Red;
        }
        else if (timeUntilEndOfLife < TimeSpan.FromDays(180))
        {
            return ConsoleColor.Yellow;
        }
        else
        {
            return ConsoleColor.White;
        }
    }
}

// Derived class representing a GPU asset
class Gpu : Asset
{
    // Constructor for initializing GPU-specific properties
    public Gpu(string brand, string modelName, DateTime purchaseDate, decimal price)
        : base(brand, modelName, purchaseDate, price)
    {
    }

    // Override method to customize display for GPU
    public override void Display()
    {
        Console.ForegroundColor = GetColorCode();
        base.Display();
        Console.ResetColor();
    }
}

// Derived class representing a RAM asset
class Ram : Asset
{
    // Constructor for initializing RAM-specific properties
    public Ram(string brand, string modelName, DateTime purchaseDate, decimal price)
        : base(brand, modelName, purchaseDate, price)
    {
    }

    // Override method to customize display for RAM
    public override void Display()
    {
        Console.ForegroundColor = GetColorCode();
        base.Display();
        Console.ResetColor();
    }
}

// Main program class
class Program
{
    // List to store assets
    static List<Asset> assets = new List<Asset>();

    // Main entry point of the program
    static void Main()
    {
        // Main program loop
        while (true)
        {
            Console.WriteLine("\nAsset Tracking Console App Menu:");
            Console.WriteLine("1. Add GPU");
            Console.WriteLine("2. Add RAM");
            Console.WriteLine("3. Display Assets");
            Console.WriteLine("4. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddGpu();
                    break;
                case "2":
                    AddRam();
                    break;
                case "3":
                    DisplayAssets();
                    break;
                case "4":
                    Console.WriteLine("Exiting the program.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    // Method to add GPU to the list
    static void AddGpu()
    {
        Console.WriteLine("Enter GPU details:");
        string brand = GetStringInput("Brand: ");
        string modelName = GetStringInput("Model Name: ");
        DateTime purchaseDate = GetDateInput("Purchase Date (MM/DD/YYYY): ");
        decimal price = GetDecimalInput("Price: ");

        Gpu gpu = new Gpu(brand, modelName, purchaseDate, price);
        assets.Add(gpu);

        Console.WriteLine("GPU added successfully!");
    }

    // Method to add RAM to the list
    static void AddRam()
    {
        Console.WriteLine("Enter RAM details:");
        string brand = GetStringInput("Brand: ");
        string modelName = GetStringInput("Model Name: ");
        DateTime purchaseDate = GetDateInput("Purchase Date (MM/DD/YYYY): ");
        decimal price = GetDecimalInput("Price: ");

        Ram ram = new Ram(brand, modelName, purchaseDate, price);
        assets.Add(ram);

        Console.WriteLine("RAM added successfully!");
    }

    // Method to display all assets
    static void DisplayAssets()
    {
        Console.WriteLine("Assets:");
        foreach (var asset in assets)
        {
            Console.ForegroundColor = asset.GetColorCode();
            asset.Display();
            Console.ResetColor();
        }
    }

    // Helper method to get string input from the user
    static string GetStringInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    // Helper method to get date input from the user
    static DateTime GetDateInput(string prompt)
    {
        Console.Write(prompt);
        return DateTime.Parse(Console.ReadLine());
    }

    // Helper method to get decimal input from the user
    static decimal GetDecimalInput(string prompt)
    {
        Console.Write(prompt);
        return decimal.Parse(Console.ReadLine());
    }
}
