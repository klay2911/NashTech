namespace LibraryManagement.Domain.Configs;

public class AppSettings
{
    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int TokenExpiration { get; set; }
    public string DefaultConnection { get; set; } = null!;
}