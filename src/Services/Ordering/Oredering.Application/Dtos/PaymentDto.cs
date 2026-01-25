namespace Oredering.Application.Dtos;

public record PaymentDto(string CardName,string CardNumber,string CVV,string Expiration,int PaymentMethod);
