namespace Api;

public record UpdateRateRequest(
    decimal NewRate,
    string? BaseCurrency,
    string? TargetCurrency);