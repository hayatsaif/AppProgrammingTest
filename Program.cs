using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        string birthdateInput;
        do
        {
            Console.Write("Enter your birthdate (MM/dd/yyyy): ");
            birthdateInput = Console.ReadLine().Trim();
        } while (!Regex.IsMatch(birthdateInput, @"^(0[1-9]|1[0-2])/(0[1-9]|[12][0-9]|3[01])/\d{4}$"));

       
        DateTime birthdate = DateTime.ParseExact(birthdateInput, "MM/dd/yyyy", null);

        int age = CalculateAge(birthdate);

     
        Console.WriteLine($"Hello, {name}! You are {age} years old.");

        string fileName = "user_info.txt";
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine($"Name: {name}");
            writer.WriteLine($"Birthdate: {birthdate.ToShortDateString()}");
            writer.WriteLine($"Age: {age}");
        }
        Console.WriteLine($"User information saved to {fileName}");

        Console.WriteLine("\nContents of user_info.txt:");
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        Console.Write("\nEnter a directory path to list files: ");
        string directoryPath = Console.ReadLine().Trim();

        if (Directory.Exists(directoryPath))
        {
            string[] files = Directory.GetFiles(directoryPath);
            Console.WriteLine($"\nFiles in directory '{directoryPath}':");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
        else
        {
            Console.WriteLine($"Directory '{directoryPath}' does not exist.");
        }

       
        Console.Write("\nEnter a string to format to title case: ");
        string inputString = Console.ReadLine().Trim();
        string titleCaseString = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputString.ToLower());
        Console.WriteLine($"Formatted string (title case): {titleCaseString}");

        GC.Collect();
        Console.WriteLine("\nGarbage collection triggered.");

        Console.WriteLine("\nPress any key to exit.");
        Console.ReadKey();
    }

    static int CalculateAge(DateTime birthdate)
    {
        DateTime now = DateTime.Today;
        int age = now.Year - birthdate.Year;
        if (birthdate > now.AddYears(-age))
            age--;
        return age;
    }
}