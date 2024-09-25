using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace ModelMapper.Core.Handlers.Payloads;

/// <summary>
/// The json payload handler convert a json content to object type
/// </summary>
public static class JsonPayloadHandler
{

    /// <summary>Maps the json content to object.</summary>
    /// <param name="data">The data.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    /// <exception cref="Exception">The given data {data} is not a valid json type
    /// or
    /// The {sourceType} type is not valid.</exception>
    public static object? MapJsonContentToObject(object data, string sourceType)
    {
        if (!IsValidJson(data)) return null;
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceType);
        Type? type = Type.GetType($"{sourceType},DIRS21.DataModel") ?? Type.GetType($"{sourceType},ExternalClient.DataModel") ?? throw new Exception($"The {sourceType} type is not valid.");
        string? payload = data as string;
        var model = JsonConvert.DeserializeObject(payload!, type);
        return model;
    }

    private static bool IsValidJson(object data)
    {
        ArgumentNullException.ThrowIfNull(data);
        if (data is not string payload) return false;
        try
        {
            JObject.Parse(payload);
            return true;
        }
        catch (Exception exception)
        {
            LogManager.GetCurrentClassLogger().Error($"The given data {data} is not a valid json type:{exception}");
            return false;
        }
    }
}
