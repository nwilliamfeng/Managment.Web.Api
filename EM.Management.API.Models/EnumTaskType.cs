using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Management.API.Models
{
    public enum EnumTaskType
    {
        Unlimited = 0,
        Once = 1,
        Day = 2,
        Month = 3,
        Year = 4,
        Activity = 5
    }
}
