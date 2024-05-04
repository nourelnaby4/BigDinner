namespace BigDinner.Domain.Models.Customers;

[ComplexType]
public class Address : ValueObject
{
    private int MaxPostCodeLength = 5;

    public string City { get; private set; }

    public string Street { get; private set; }

    public int PostalCode { get; private set; }

    public Address(string city, string street, int postalCode)
    {
        if (postalCode.ToString().Length > MaxPostCodeLength)
        {
            throw new ArgumentException("Post Code is In valid");
        }

        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return new object[] { City, Street, PostalCode };
    }
}

