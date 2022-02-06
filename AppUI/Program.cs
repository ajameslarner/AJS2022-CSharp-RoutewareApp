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

            for (int i = 0; i < matchedRecords.Count; i++)
            {
                Console.WriteLine($"{i+1} || {matchedRecords[i]}");
                Console.WriteLine("-------------------");
            }

            Console.Write("Enter your chosen record: ");
            string? recordSelection = Console.ReadLine();
            int selectionNum = Convert.ToInt32(recordSelection);

            var closeLocations = DataFile.SimilarRecordsByLocation(matchedRecords[selectionNum]);

            foreach (var item in closeLocations)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
            Console.Clear();
        }
    }
}