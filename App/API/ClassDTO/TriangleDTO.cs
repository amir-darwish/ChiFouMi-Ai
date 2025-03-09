namespace API.ClassDTO;
using System.Text.Json.Serialization;


public class TriangleDTO:FormsDTO
{
    [JsonPropertyOrder(3)]

    public double Adjacent { get; set; }
    [JsonPropertyOrder(4)]

    public double Opposite { get; set; }
    [JsonPropertyOrder(5)]

    public double Hypotenuse { get; set; }
}