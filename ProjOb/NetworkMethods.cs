using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjOb;

public static class NetworkMethods
{
    private static readonly JsonSerializerOptions Options = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve,
        WriteIndented = true
    };
    public static async Task SendDataAsync(NetworkStream stream, object data)
    {
        await SendJsonAsync(stream, SerializeData(data));
    }
    
    public static string SerializeData(object data)
    {
        return JsonSerializer.Serialize(data, Options);
    }
    
    public static async Task SendJsonAsync(NetworkStream stream, string jsonData)
    {
        byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        byte[] lengthPrefix = BitConverter.GetBytes(dataBytes.Length);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(lengthPrefix);
        }
        
        await stream.WriteAsync(lengthPrefix, 0, lengthPrefix.Length);
        await stream.WriteAsync(dataBytes, 0, dataBytes.Length);
    }
    
    public static async Task<T?> ReceiveDataAsync<T>(NetworkStream stream)
    {
        byte[] lengthPrefix = new byte[4];
        int totalRead = 0;
        while (totalRead < lengthPrefix.Length)
        {
            int read = await stream.ReadAsync(lengthPrefix, totalRead, lengthPrefix.Length - totalRead);
            if (read == 0) return default;
            totalRead += read;
        }
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(lengthPrefix);
        }
        
        int length = BitConverter.ToInt32(lengthPrefix);
        byte[] dataBytes = new byte[length];
        
        totalRead = 0;
        while (totalRead < length)
        {
            int bytesRead = await stream.ReadAsync(dataBytes, totalRead, length - totalRead);
            if (bytesRead == 0) return default;
            totalRead += bytesRead;
        }
        
        string jsonData = System.Text.Encoding.UTF8.GetString(dataBytes);
        return JsonSerializer.Deserialize<T>(jsonData, Options) ?? throw new Exception("Failed to deserialize data");
    }
}