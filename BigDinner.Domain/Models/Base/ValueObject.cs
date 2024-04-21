namespace BigDinner.Domain.Models.BaseModels
{
    // importan hint that i can use record instead of this impelemetation 
    // but i do it for learning in deepth

    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetEqualityComponents();
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != typeof(ValueObject))
                return false;

            ValueObject valueObject = (ValueObject)obj;

            return GetEqualityComponents()
                .SequenceEqual(valueObject.GetEqualityComponents());

        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }
    }
}
