namespace BigDinner.Domain.Models.Orders;

public enum OrderStatus
{
    Pending,
    Confirmed,
    Shipped,
    Delivered,
    Cancelled
}
