namespace CRM.Domain.Common;

public class SortParams
{
    public string? OrderBy { get; set; }
    public bool Descending { get; set; } = false;
}
