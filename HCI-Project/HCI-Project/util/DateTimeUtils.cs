using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.util
{
    public class DateTimeUtils
    {
        public static DateTime calculateEndDate(DateTime length, DateTime startDate)
        {
            return startDate + (length - new DateTime(1, 1, 1));
        }
    }
}
