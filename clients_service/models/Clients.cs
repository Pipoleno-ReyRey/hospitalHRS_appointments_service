using System.ComponentModel.DataAnnotations;

public class Users{
    public int id { get; set; }
    [StringLength(maximumLength:250)]
    public string? name { get; set; }
    [StringLength(maximumLength:250)]
    public string? lastName { get; set; }
    [StringLength(maximumLength:250)]
    public string? email { get; set; }
    [StringLength(maximumLength:150)]
    public string? phone { get; set; }
    [StringLength(maximumLength:300)]
    public string? insurance_num { get; set; }
    [StringLength(maximumLength:100)]
    public string? role { get; set; }
}