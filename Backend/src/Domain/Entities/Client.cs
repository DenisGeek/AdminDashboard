namespace Domain;

public class Client : IBaseEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public decimal BalanceInTokens { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();

    private Client() { }
}
