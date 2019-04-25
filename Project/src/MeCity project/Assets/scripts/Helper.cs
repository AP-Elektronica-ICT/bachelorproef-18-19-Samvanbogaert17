public static class Helper
{
    public static bool IsOneOf<T>(this T value, params T[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Equals(value))
            {
                return true;
            }
        }

        return false;
    }

    public static string AddLeadingZero(this int value)
    {
        if (value < 10)
        {
            return "0" + value;
        }
        else
        {
            return value.ToString();
        }
    }
}
