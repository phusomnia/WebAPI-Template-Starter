namespace WebAPI_Template_Starter.Domain.entities.@base;

public class Pageable
{
    public int page;
    public int limit;
    public int total;
    
    public Pageable() { }
    
    public Pageable(int page, int limit) : this(page, limit, 0)
    {
    }

    public Pageable(int page, int limit, int total)
    {
        this.page = page;
        this.limit = limit;
        this.total = total;
    }
}