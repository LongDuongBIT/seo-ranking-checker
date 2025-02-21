using Application.Common.Interfaces;
using System.Text.Json;

namespace Infrastructure.Common.Services;

public class SerializerService : ISerializerService
{
    public T? Deserialize<T>(string text)
    {
        return JsonSerializer.Deserialize<T>(text);
    }

    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public string Serialize<T>(T obj, Type type)
    {
        return JsonSerializer.Serialize(obj, type);
    }
}