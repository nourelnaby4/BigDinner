namespace BigDinner.Domain.Models.Customers;

public class Customer : AggregateRoot<Guid>
{
    private const int MaxPhoneLength = 15;

    public string Name { get; private set; }

    public Email Email { get; private set; }

    public string Phone { get; private set; }

    public Address Address { get; private set; }

    private Customer(Guid id) : base(id)
    {
    }

    private Customer(Guid id, string name, Email email, string phone, Address address) : base(id)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public static Customer Create(Guid id, string name, Email email, string phone, Address address) 
    {
        if (!IsValidPhone(phone))
        {
            throw new ArgumentException("Invalid phone number");
        }

        return new Customer(id, name, email, phone, address);
    }

    private static bool IsValidPhone(string phone)
    {
        return !string.IsNullOrWhiteSpace(phone) && phone.Length > MaxPhoneLength;
    }

}

