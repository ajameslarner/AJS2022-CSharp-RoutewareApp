namespace AppLib;
public class RoutewareFileHandle
{
    public string FilePath { get; set; }
    public string[] FileLinesData { get; set; }
    public string[][] FileDataSplit { get; set; }
    public RoutewareFileHandle(string filePath)
    {
        FilePath = filePath;
        FileLinesData = System.IO.File.ReadAllLines(this.FilePath);
        FileDataSplit = FileLinesData.Select(x => x.Split(",")).ToArray().ToArray();
    }

    public ArrayList SearchRecord(string? term, int column)
    {
        ArrayList matches = new ArrayList();

        try
        {
            for (int i = 1; i < FileLinesData.Length; i++)
            {
                string[] cells = FileLinesData[i].Split(',');
                if (DoesRecordMatch(term, cells, column))
                {
                    matches.Add(FileLinesData[i]);
                }
            }

            return matches;
            
        }
        catch (Exception e)
        {
            matches.Add("No Records Found");
            return matches;
            throw new ApplicationException("Exception Caught: ", e);
        }
    }

    public ArrayList SimilarRecordsByLocation(Object recordSet)
    {
        ArrayList matches = new ArrayList();

        ArrayList similar = new ArrayList();

        string? record = recordSet.ToString();
        string[] recordData = record.Split(',');
        double lat = Convert.ToDouble(recordData[4]);
        double lon = Convert.ToDouble(recordData[5]);
        var coord = new GeoCoordinate(lat, lon);
        
        try
        {
            for (int i = 1; i < FileLinesData.Length; i++)
            {
                string[] cells = FileLinesData[i].Split(",");

                matches.Add(new GeoCoordinate(Convert.ToDouble(cells[4]), Convert.ToDouble(cells[5])).ToString());
            }

            GeoCoordinate[] closeLocations = SortSelection(coord, matches);

            foreach (var item in closeLocations)
            {
                similar.Add(item.ToString());
            }

            return similar;
            
        }
        catch (Exception e)
        {
            throw new ApplicationException("Exception Caught: ", e);
        }
    }

    public bool DoesRecordMatch(string? term, string[] cells, int column)
    {
        return (cells[column].ToLower().Contains(term.ToLower()));
    }

    public GeoCoordinate[] SortSelection(GeoCoordinate pos, ArrayList locations)
    {
        string[] loc = (string[])locations.ToArray( typeof( string ) );

        var nearest = loc.Select(x => new GeoCoordinate(Convert.ToDouble(x.Split(",")[0]), Convert.ToDouble(x.Split(",")[1])))
                    .OrderBy(x => x.GetDistanceTo(pos))
                    .Take(10)
                    .ToArray();

        return nearest;
    }
}