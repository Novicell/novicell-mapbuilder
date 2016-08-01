namespace MapBuilder.Library.Helpers
{
    public static class StaticHelper
    {
        public static string UppercaseWordsAndRemoveWhiteSpace(string value)
        {
            var array = value.ToCharArray();

            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }

            for (var i = 1; i < array.Length; i++)
            {
                if (array[i - 1] != ' ') continue;

                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
            var str = new string(array);

            str = str.Replace(" ", string.Empty);

            str = char.ToLower(str[0]) + str.Substring(1);

            str = str.Replace("(", string.Empty).Replace(")", string.Empty);

            return str;
        }

        public static string GetMapsTableName()
        {
            return "NovicellMapBuilderMaps";
        }

        public static string GetDataTableName()
        {
            return "NovicellMapBuilderData";
        }
    }
}
