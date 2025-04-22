using Newtonsoft.Json;

namespace API.ClassDTO;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

[JsonObject]
public class FormsDTO 
{
    [JsonPropertyOrder(1)]
    public string Type { get; set; }
    
    [JsonPropertyOrder(2)]
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
    public FormsDTO(string type, string color)
    {
        Type = type;
        Color = color;
    }
    public FormsDTO(){}
}