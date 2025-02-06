namespace SoftwareCatalog.Api.Techs;
//storing in DB
public class TechEntity
{
    public Guid Id { get; set; }
    public string Sub { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;

    public string? Phone { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;

}