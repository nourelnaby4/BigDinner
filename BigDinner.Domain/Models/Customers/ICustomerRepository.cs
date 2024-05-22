namespace BigDinner.Domain.Models.Customers;

public interface ICustomerRepository
{
    void Add(Customer customer);

    Task<IEnumerable<Customer>> GetAllAsync();

    Task<Customer?> GetByIdAsync(Guid customerId);

    void Update(Customer customer);
}
