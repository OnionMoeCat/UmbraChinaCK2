using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class Time: IComparable<Time>
    {
        public int year;
        public int month;
        public int day;
        public int CompareTo(Time other)
        {
            // Compares Height, Length, and Width.
            if (this.year.CompareTo(other.year) != 0)
            {
                return this.year.CompareTo(other.year);
            }
            else if (this.month.CompareTo(other.month) != 0)
            {
                return this.month.CompareTo(other.month);
            }
            else if (this.day.CompareTo(other.day) != 0)
            {
                return this.day.CompareTo(other.day);
            }
            else
            {
                return 0;
            }
        }
    }
}
