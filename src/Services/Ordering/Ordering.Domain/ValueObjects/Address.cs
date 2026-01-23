namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string AdressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;

    protected Address() 
    {
        
    }
    private Address(
        string firstName,
        string lastName,
        string? emailAddress,
        string adressLine,
        string country,
        string state,
        string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AdressLine = adressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }
    public static Address Of(string firstName,
        string lastName,
        string? emailAddress,
        string adressLine,
        string country,
        string state,
        string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(adressLine);

        return new Address(
            firstName,
            lastName,
            emailAddress,
            adressLine,
            country,
            state,
            zipCode);
    }

}
