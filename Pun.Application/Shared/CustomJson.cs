using System.Text.Json;

namespace WebAPI_Template_Starter.Shared;

public class CustomJson
{
    public static String json(Object value, CustomJsonOptions options)
    {
        switch (options)
        {
            case CustomJsonOptions.WriteIndented:
                return JsonSerializer.Serialize(value, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            case CustomJsonOptions.None:
                return JsonSerializer.Serialize(value);      
            default:
                throw new Exception("GeGe");
        }
    }
}

public enum CustomJsonOptions
{
    WriteIndented,
    None
}