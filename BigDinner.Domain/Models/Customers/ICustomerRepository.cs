namespace BigDinner.Domain.Models.Customers;

public interface ICustomerRepository
{
    void Add(Customer customer);
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> GetById(Guid customerId);
}
