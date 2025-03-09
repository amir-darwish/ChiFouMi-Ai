namespace API.ClassDTO;
using System.Text.Json.Serialization;

public class FormsDTO 
{
    public string Type { get; set; }  // "Rond", "Rectangle", "Triangle"
    public string Color { get; set; }
    
    // for post method 
    // "?" for Nullable Type {Null value}
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Rayon { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Length { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Width { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Adjacent { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Opposite { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Hypotenuse { get; set; }
    protected FormsDTO(string type, string color)
    {
        Type = type;
        Color = color;
    }
    public FormsDTO(){}
}