namespace BigDinner.Application.Features.Shippings.Command;

public record ShippingMethodCommand
{
    public string Name { get;  set; }

    public string Description { get;  set; }
}
