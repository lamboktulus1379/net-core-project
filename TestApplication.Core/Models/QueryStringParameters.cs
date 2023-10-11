namespace TestApplication.Core.Models;

public class QueryStringParameters
{
    private const int maxPageSize = 10;
    public int pageNumber { get; set; } = 1;
    public int _pageSize = 10;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
    
    public string OrderBy { get; set; }
    public string Fields { get; set; }
}