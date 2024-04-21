using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Domain.ValueObjects
{
    public sealed class AverageRating : ValueObject
    {
        public double Value { get; private set; }
        public int NumRating { get; private set; }

        private AverageRating(double value, int numRating)
        {
            Value = value;
            NumRating = numRating;
        }

        public static AverageRating Create(double rating = 0, int newRating = 0)
        {
            return new AverageRating(rating, newRating);
        }

        public void AddNewRating(Rating rating)
        {
            Value = ((Value * NumRating) + rating.Value) / ++NumRating;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
