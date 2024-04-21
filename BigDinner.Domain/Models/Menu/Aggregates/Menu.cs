using BigDinner.Domain.Models.BaseModels;
using BigDinner.Domain.Models.Dinner.ValueObjects;
using BigDinner.Domain.Models.Host.ValueObjects;
using BigDinner.Domain.Models.Menu.Entities;
using BigDinner.Domain.Models.MenueReview.ValueObjects;

namespace BigDinner.Domain.Models.Menu.Aggregates;

public sealed class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _section = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();

    public string Name { get; }
    public string Description { get; }
    public float AverageRating { get; }
    public DateTime CreateDateOnUtc { get; }
    public DateTime LastUpdateDateOnUtc { get; }

    public HostId HostId { get; }

    public IReadOnlyList<MenuSection> Sections=> _section;
    public IReadOnlyList<DinnerId> DinnerIds=> _dinnerIds;
    public IReadOnlyList<MenuReviewId> MenuReviewIds=> _menuReviewIds;

    private Menu(MenuId id,string name,string description,HostId hostId,DateTime createdDateTime,DateTime lastUpdatedDateTime) : base(id)
    {
        Name = name;
        Description = description;
        HostId = hostId;
        CreateDateOnUtc = createdDateTime;
        LastUpdateDateOnUtc = lastUpdatedDateTime;
        
    }

    public static Menu Create(string name, string description, HostId hostId)
    {
        return new Menu(
            MenuId.Create(),
            name,
            description,
            hostId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}

