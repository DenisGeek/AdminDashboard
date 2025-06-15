namespace Domain;

public class Payment : IBaseEntity
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public PaymentStatus Status { get; set; }
}
