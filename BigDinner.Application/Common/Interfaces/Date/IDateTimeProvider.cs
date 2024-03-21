using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Application.Common.Interfaces.Date
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
