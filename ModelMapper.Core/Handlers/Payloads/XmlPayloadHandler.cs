using NLog;
using System.Xml;
using System.Xml.Serialization;

namespace ModelMapper.Core.Handlers.Payloads;

/// <summary>
/// The xml payload handler convert a xml content to object type
/// </summary>
public static class XmlPayloadHandler
{
    /// <summary>
    /// Maps the XML content to object.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <returns></returns>
    /// <exception cref="Exception">
    /// The given data {data} is not a valid xml type
    /// or
    /// The {sourceType} type is not valid.
    /// </exception>
    public static object? MapXmlContentToObject(object data, string sourceType)
    {
        if (!IsValidXmlFormat(data)) return null;
        ArgumentException.ThrowIfNullOrWhiteSpace(sourceType);
        Type? type = Type.GetType($"{sourceType},DIRS21.DataModel") ?? Type.GetType($"{sourceType},ExternalClient.DataModel") ?? throw new Exception($"The {sourceType} type is not valid.");
        string xmlContent = data.ToString()!;

        // Create an instance of XmlSerializer with the type of object you want to deserialize.
        XmlSerializer serializer = new(type);
        object? model;
        // Use StringReader to convert the XML string to a readable stream for XmlSerializer.
        using (StringReader reader = new(xmlContent))
        {
            // Deserialize the XML content to a object.
            model = serializer.Deserialize(reader);
        }
        return model;
    }

    private static bool IsValidXmlFormat(object data)
    {
        ArgumentNullException.ThrowIfNull(data);
        if (data is not string payload) return false;
        try
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(payload);
            XmlConvert.VerifyXmlChars(payload);
            return true;
        }
        catch (Exception exception)
        {
            LogManager.GetCurrentClassLogger().Error($"The given data {data} is not a valid xml type:{exception}");
            return false;
        }
    }
}
