using BigDinner.Application.Common.Interfaces.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigDinner.Service.Date
{
    public class DateTimeprovider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
