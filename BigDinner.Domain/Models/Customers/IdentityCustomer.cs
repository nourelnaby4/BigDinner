using BigDinner.Domain.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace BigDinner.Domain.Models.Customers
{
    public class IdentityCustomer : ApplicationUser
    {
        private IdentityCustomer(){ }

        private IdentityCustomer(Guid customerId,  string email, string username)
        {
            Id = customerId.ToString();
            Email = email;
            UserName = username;
        }

        public static IdentityCustomer MapToIdentity(Customer customer)
        {
            return new IdentityCustomer(customer.Id, customer.Email.Value, customer.Name);
        }
    }
}
