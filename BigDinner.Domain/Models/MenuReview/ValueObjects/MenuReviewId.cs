namespace BigDinner.Domain.Models.MenueReview.ValueObjects;

public sealed class MenuReviewId : ValueObject
{
    public Guid Value { get; }

    private MenuReviewId(Guid value) 
    {
        Value = value;
    }

    public static MenuReviewId Create()
    {
        return new MenuReviewId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

