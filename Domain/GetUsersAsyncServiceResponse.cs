namespace hello_asp_identity.Domain;

public class GetAllAsyncServiceResponse<T>
{
    public long TotalNumber { get; set; }
    public List<T> Data { get; set; }
}