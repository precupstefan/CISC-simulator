namespace Assembly.helpers
{
    public static class StringHelper
    {
        public static string RemoveAtFirstChar(string _string, char _char)
        {
            var index = _string.IndexOf(_char);
            return index > 0 ? _string.Remove(index) : _string;
        }
    }
}