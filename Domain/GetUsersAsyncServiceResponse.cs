namespace hello_asp_identity.Domain;

public class GetAllServiceResponse<T> where T : class
{
    public long TotalNumber { get; set; }
    public IList<T> Data { get; set; } = new List<T>();
}