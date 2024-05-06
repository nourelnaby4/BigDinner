namespace BigDinner.Domain.Models.Customers;

[ComplexType]
public class Email : ValueObject
{
    private const int MaxEmailLength = 150;

    public string Value { get; private set; }

    public Email(string value)
    {
        if (!IsValidEmail(value))
        {
            throw new ArgumentException("Invalid email");
        }

        Value = value;
    }

    private bool IsValidEmail(string email)
    {
        return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Length < MaxEmailLength;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

