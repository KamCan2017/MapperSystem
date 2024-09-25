using DIRS21.DataModel;
using ModelMapper.Core.Handlers.Payloads;

namespace DIRS21.Unittests.Core.Handler;


public class XmlMapHandlerTests
{
    private readonly string _validXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Payment xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Id>14</Id>\r\n  <ServiceName>paypal</ServiceName>\r\n  <Token>xdr-789-tr</Token>\r\n</Payment>";

    [Test, TestCase(""), TestCase(" ")]
    public void MapXmlContentToObject_ShouldReturnNull_WhenInvalidXml(object? invalidXml)
    {
        // Arrange
        string sourceType = "TestModel";

        // Act
        var result = XmlPayloadHandler.MapXmlContentToObject(invalidXml!, sourceType);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void MapXmlContentToObject_ShouldThrowException_WhenXmlDataIsNull()
    {
        // Arrange
        object? data = null;
        string invalidSourceType = "InvalidType";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => XmlPayloadHandler.MapXmlContentToObject(data!, invalidSourceType));
    }

    [Test, TestCase("InvalidType")]
    public void MapXmlContentToObject_ShouldThrowException_WhenSourceTypeIsInvalid(string invalidSourceType)
    {
        // Act & Assert
        Assert.Throws<Exception>(() => XmlPayloadHandler.MapXmlContentToObject(_validXml, invalidSourceType));
    }


    [Test, TestCase(""), TestCase(" ")]
    public void MapXmlContentToObject_ShouldThrowArgumentException_WhenSourceTypeIsInvalid(string invalidSourceType)
    {

        // Act & Assert
        Assert.Throws<ArgumentException>(() => XmlPayloadHandler.MapXmlContentToObject(_validXml, invalidSourceType));
    }

    [Test]
    public void MapXmlContentToObject_ShouldReturnObject_WhenValidXmlAndSourceType()
    {
        // Act 
        var obj = XmlPayloadHandler.MapXmlContentToObject(_validXml, typeof(Payment).FullName!);

        Assert.Multiple(() =>
        {
            //Assert
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj, Is.TypeOf<Payment>());
        });
    }
}
