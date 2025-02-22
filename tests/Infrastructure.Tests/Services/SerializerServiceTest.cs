using Infrastructure.Common.Services;

namespace Infrastructure.Tests.Services;

public class SerializerServiceTest
{
    [Test]
    public void SerializerService_Serialize_ShouldReturnSerializedString()
    {
        // Arrange
        var serializerService = new SerializerService();
        var obj = new { Name = "John Doe", Age = 30 };

        // Act
        var result = serializerService.Serialize(obj);

        // Assert
        Assert.That(result, Is.EqualTo("{\"Name\":\"John Doe\",\"Age\":30}"));
    }

    [Test]
    public void SerializerService_Deserialize_ShouldReturnDeserializedObject()
    {
        // Arrange
        var serializerService = new SerializerService();
        var json = "{\"Name\":\"John Doe\",\"Age\":30}";

        // Act
        var result = serializerService.Deserialize<TestClass>(json);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Name, Is.EqualTo("John Doe"));
            Assert.That(result.Age, Is.EqualTo(30));
        });
    }

    public class TestClass
    {
        public required string Name { get; set; }
        public int Age { get; set; }
    }
}