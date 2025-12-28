namespace Ordering.Application.Dtos;

public record PaymentDto(string? CardName, string CardNummber, string Expiration, string Cvv, int PaymentMethod);

