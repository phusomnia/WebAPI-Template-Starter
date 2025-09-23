using Newtonsoft.Json;

namespace WebAPI_Template_Starter.SharedKernel.utils;

public static class ConverterUtils
{
    public static Dictionary<String, Object> toDict<TValue>(TValue value) where TValue : class
    {
        string json = JsonConvert.SerializeObject(value);
        return JsonConvert.DeserializeObject<Dictionary<String, Object>>(json)!;
    }
}