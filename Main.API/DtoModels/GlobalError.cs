using System.Text.Json;

namespace Main.API.DtoModels;

public class GlobalError
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}