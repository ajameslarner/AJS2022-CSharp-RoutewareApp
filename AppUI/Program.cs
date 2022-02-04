using IronXL;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel; //Add a library reference to the excel assembly

//Requires a license key to use
WorkBook workbook = WorkBook.Load("data/Data.xls");
WorkSheet sheet = workbook.WorkSheets.First();

RangeColumn column = sheet.GetColumn(0);

string? searchTerm = Console.ReadLine();

foreach (var item in column)
{
    if (item.ToString() == searchTerm)
    {
        Console.WriteLine("Success. Match Found: " + item.ToString());
    }
}