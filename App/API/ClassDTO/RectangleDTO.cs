namespace API.ClassDTO;
using System.Text.Json.Serialization;

public class RectangleDTO:FormsDTO
{
    [JsonPropertyOrder(3)]

    public double Length { get; set; }
    [JsonPropertyOrder(4)]

    public double Width { get; set; }
}