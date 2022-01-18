namespace hello_asp_identity.Domain;

public class GetAllAsyncServiceResponse<T> where T : class
{
    public long TotalNumber { get; set; }
    public List<T> Data { get; set; }
}