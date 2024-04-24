using System.Globalization;

namespace PublicTools.Tools;

public static class Helper
{
    public static string ConvertToPersian(this DateTime date)
    {
        var persianCalendar = new PersianCalendar();
        var year = persianCalendar.GetYear(date);
        var month = persianCalendar.GetMonth(date);
        var day = persianCalendar.GetDayOfMonth(date);
        var time = date.TimeOfDay;
        var result = $"{year}/{month}/{day} {time}";
        return result;
    }
}
