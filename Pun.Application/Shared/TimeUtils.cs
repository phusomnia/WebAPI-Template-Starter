namespace WebAPI_Template_Starter.Shared;

public static class TimeUtils
{
    public static DateTime? AsiaTimeZone(DateTime time)
    {
        return TimeZoneInfo.ConvertTime(time, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
    }
}