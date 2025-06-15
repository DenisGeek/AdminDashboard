namespace Domain;

public class Rate : IBaseEntity
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public DateTime LastUpdated { get; set; }
    public Currency BaseCurrency { get; set; }
    public Currency TargetCurrency { get; set; }
}
