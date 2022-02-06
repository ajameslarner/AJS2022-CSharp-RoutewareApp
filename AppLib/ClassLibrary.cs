namespace AppLib;
public class RoutewareFileHandle
{
    public string FilePath { get; set; }
    public RoutewareFileHandle(string filePath)
    {
        FilePath = filePath;
    }

    public ArrayList SearchRecord(string? term, int column)
    {
        ArrayList matches = new ArrayList();

        try
        {
            string[] lines = System.IO.File.ReadAllLines(this.FilePath);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] cells = lines[i].Split(',');
                if (DoesRecordMatch(term, cells, column))
                {
                    matches.Add(lines[i]);
                }
            }

            // var nearest = (from h in db.hotels
            //     let geo = new GeoCoordinate{ Latitude = h.gps.lat, Longitude = h.gps.lng}
            //     orderby geo.GetDistanceTo(coord)
            //     select h).Take(10);

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