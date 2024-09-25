using ExternalClient.DataModel.Google.Validators;
using ExternalClient.DataModel.Interfaces;
using System.Xml.Serialization;

namespace ExternalClient.DataModel.Google;

/// <summary>
/// The payment model
/// </summary>
public class Payment : GoogleModel, IExternalModel
{
    public Payment() : base(new PaymentValidator())
    {

    }

    [XmlElement]
    public string ServiceName { get; set; } = string.Empty;

    [XmlElement]
    public string Token { get; set; } = string.Empty;
}
