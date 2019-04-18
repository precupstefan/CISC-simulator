using System;

namespace Assembly.exceptions
{
    public class OffsetOutOfRangeException: Exception
    {
        public OffsetOutOfRangeException(string s) : base(s)
        {
        }
    }
}