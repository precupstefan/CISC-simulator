using System;

namespace Assembly.exceptions
{
    public class AccessModeException : Exception
    {
        public AccessModeException(string s) : base(s)
        {
        }
    }
}