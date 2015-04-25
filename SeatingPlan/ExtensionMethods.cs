using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatingPlanCreator
{
    public static class ExtensionMethods
    {
        public static void Use<T>(this T item, Action<T> work)
        {
            work(item);
        }
    }
}
