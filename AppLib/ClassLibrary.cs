namespace AppLib;
public class RoutewareFileHandle
{
    public string FilePath { get; set; }
    public string[] FileLinesData { get; set; }
    public RoutewareFileHandle(string filePath)
    {
        FilePath = filePath;
        FileLinesData = System.IO.File.ReadAllLines(this.FilePath);
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

        string? record = recordSet.ToString();
        string[] recordData = record.Split(',');
        double lat = Convert.ToDouble(recordData[4]);
        double lon = Convert.ToDouble(recordData[5]);
        var coord = new GeoCoordinate(lat, lon);

        try
        {
            for (int i = 0; i < FileLinesData.Length; i++)
            {
                string[] cells = FileLinesData[i].Split(',');

                var nearest = cells.Select(x => new GeoCoordinate(Convert.ToDouble(cells[4]), Convert.ToDouble(cells[5])))
                        .OrderBy(x => x.GetDistanceTo(coord))
                        .Take(10);

                //var nearest = (from h in FileLinesData
                 //       let geo = new GeoCoordinate( Convert.ToDouble(cells[4]), Convert.ToDouble(cells[5]))
                 //       orderby geo.GetDistanceTo(coord)
                 //       select h).Take(10);

                foreach (var item in nearest)
                {
                    matches.Add(item.ToString());
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

    public bool DoesRecordMatch(string? term, string[] cells, int column)
    {
        return (cells[column].ToLower().Contains(term.ToLower()));
    }
}