using DIRS21.DataModel;
using ModelMapper.Core.Handlers.Payloads;

namespace DIRS21.Unittests.Core.Handler;


public class JsonMapHandlerTests
{
    [Test, TestCase(""), TestCase(" "), TestCase("Invalid JSON String")]
    public void MapJsonContentToObject_ShouldReturnNull_WhenInvalidJson(object? invalidJson)
    {
        // Arrange
        string sourceType = "TestModel";

        // Act
        var result = JsonPayloadHandler.MapJsonContentToObject(invalidJson!, sourceType);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void MapJsonContentToObject_ShouldThrowException_WhenJsonDataIsNull()
    {
        // Arrange
        object? data = null;
        string invalidSourceType = "InvalidType";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => JsonPayloadHandler.MapJsonContentToObject(data!, invalidSourceType));
    }

    [Test, TestCase("InvalidType")]
    public void MapJsonContentToObject_ShouldThrowException_WhenSourceTypeIsInvalid(string invalidSourceType)
    {
        // Arrange
        var validJson = "{\"Name\":\"John\"}";

        // Act & Assert
        Assert.Throws<Exception>(() => JsonPayloadHandler.MapJsonContentToObject(validJson, invalidSourceType));
    }


    [Test, TestCase(""), TestCase(" ")]
    public void MapJsonContentToObject_ShouldThrowArgumentException_WhenSourceTypeIsInvalid(string invalidSourceType)
    {
        // Arrange
        var validJson = "{\"Name\":\"John\"}";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => JsonPayloadHandler.MapJsonContentToObject(validJson, invalidSourceType));
    }

    [Test]
    public void MapJsonContentToObject_ShouldReturnObject_WhenValidJsonAndSourceType()
    {
        // Arrange
        var validJson = "{\"Name\":\"John\"}";

        // Act 
        var obj = JsonPayloadHandler.MapJsonContentToObject(validJson, typeof(Room).FullName!);

        Assert.Multiple(() =>
        {
            //Assert
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj, Is.TypeOf<Room>());
        });
    }
}
