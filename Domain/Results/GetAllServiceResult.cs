namespace hello_asp_identity.Domain.Results;

public class GetAllServiceResult<T> where T : class
{
    public long TotalNumber { get; set; }
    public IList<T> Data { get; set; } = new List<T>();
}