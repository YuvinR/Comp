using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiDesktopApp1.Helpers
{
    public class Utils
    {
        public static List<int> GetYearList()
        {
            int oldestYear = 1950;
            int currentYear = DateTime.Now.Year;
            List<int> years = new List<int>();
            for (int i = currentYear; i >= oldestYear; i--)
            {
                years.Add(i);
            }
            return years;
        }

        public static List<string> GetMonths()
        {
            List<string> months = new List<string>
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };
            return months;
        }

    }
}
