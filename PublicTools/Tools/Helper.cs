using System.Globalization;

namespace PublicTools.Tools;

public class Helper
{
    public static string GregorianToPersianDateConverter(DateTime date)
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
