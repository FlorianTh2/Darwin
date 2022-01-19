namespace hello_asp_identity.Contracts.V1.Requests;

public record PaginationQuery
{
    public int PageSize { get; init; }
    public int PageNumber { get; init; }

    public PaginationQuery()
    {
        PageNumber = 1;
        PageSize = 100;
    }

    public PaginationQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize > 100 ? 100 : pageSize;
    }

}