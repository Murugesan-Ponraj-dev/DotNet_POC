namespace Order.Domain.EntityResponse;
public class FilterQueryRequest
{
    public Dictionary<string, string> FilterQueries { get; set; }
    public int pageNo { get; set; }

}
