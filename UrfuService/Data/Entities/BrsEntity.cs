namespace UrfuService.Data.Entities;

public class BrsEntity
{
    public int Id { get; set; }
    public string RefreshToken { get; set; }
    public string BrsInJson { get; set; }
    public DateTime LastUpdate { get; set; }
}