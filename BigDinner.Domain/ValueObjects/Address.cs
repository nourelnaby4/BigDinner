namespace BigDinner.Domain.ValueObjects;

public class Address : ValueObject
{
    private int MaxPostCodeLength = 5;

    public string Country { get; private set; }

    public string City { get; private set; }

    public string Street { get; private set; }

    public int PostalCode { get; private set; }

    public Address(string city, string street, int postalCode, string? country = "Egypt")
    {
        if (postalCode.ToString().Length != MaxPostCodeLength)
        {
            throw new ArgumentException("Post Code is In valid: must be five digits");
        }

        City = city;
        Street = street;
        PostalCode = postalCode;
        Country = country;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return new object[] { City, Street, PostalCode, Country };
    }
}

