﻿using BigDinner.Domain.ValueObjects;
using Newtonsoft.Json;

namespace BigDinner.Domain.Models.Menus;

public sealed class MenuItem : Entity<Guid>
{
    public string Name { get; private set; }

    public string Description { get; private set; }

    public Price Price { get; private set; }

    public Guid MenuId { get; private set; }


    [JsonConstructor]
    private MenuItem(Guid id) : base(id)
    {
    }

    private MenuItem(Guid id,Guid menuId, string name, string description, Price price) : base(id)
    {
        MenuId = menuId;
        Name = name;
        Description = description;
        Price = price;
    }


    public static MenuItem Create(Guid  menuId, string name, string description, Price price)
    {
        return new MenuItem(Guid.NewGuid(), menuId, name, description, price);
    }

    public void UpdateInfo(string name, string description, Price price)
    {
        Price = price;
        Name = name;
        Description = description;
    }
}

