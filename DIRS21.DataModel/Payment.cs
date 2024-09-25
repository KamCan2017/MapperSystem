
using DIRS21.DataModel.Validators;
namespace DIRS21.DataModel;

/// <summary>
/// The payment model
/// </summary>
public class Payment : MirrorModel
{
    public Payment() : base(new PaymentValidator())
    {

    }

    public string ServiceName { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}
