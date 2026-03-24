namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Street { get; init; } = default!;
    public string City { get; init; } = default!;
    public string State { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string ZipCode { get; init; } = default!;
    public string? EmailAddress { get; init; } = default!;
    protected Address() { }
    private Address(string firstName, string lastName, string street, string city, string state, string country, string zipCode, string? emailAddress)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        EmailAddress = emailAddress;
    }

    public static Address Of(string firstName, string lastName, string street, string city, string state, string country, string zipCode, string? emailAddress = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName, nameof(firstName));
        ArgumentException.ThrowIfNullOrEmpty(lastName, nameof(lastName));
        ArgumentException.ThrowIfNullOrEmpty(street, nameof(street));
        ArgumentException.ThrowIfNullOrEmpty(city, nameof(city));
        ArgumentException.ThrowIfNullOrEmpty(state, nameof(state));
        ArgumentException.ThrowIfNullOrEmpty(country, nameof(country));
        ArgumentException.ThrowIfNullOrEmpty(zipCode, nameof(zipCode));
        return new Address(firstName, lastName, street, city, state, country, zipCode, emailAddress);
    }
}
