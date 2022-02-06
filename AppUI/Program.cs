using AppLib;

namespace AppUI;

class MainProgram
{
    static void Main(string[] args)
    {
        RoutewareFileHandle DataFile = new RoutewareFileHandle("Data.csv");

        while (true)
        {
            Console.Write("Enter your search term: ");
            string? userInput = Console.ReadLine();

            var matchedRecords = DataFile.SearchRecord(userInput, 0);

            foreach (var item in matchedRecords)
            {
                Console.WriteLine(item);
                Console.WriteLine("-------------------");
            }

            Console.ReadLine();
            Console.Clear();
        }
    }
}