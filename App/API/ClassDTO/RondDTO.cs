using App.Class;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;

namespace API.ClassDTO;

[JsonObject]

public class RondDTO : FormsDTO
{
    [JsonPropertyOrder(3)]
    public new double Rayon { get; set; } 

    public RondDTO(double rayon, string color) : base("Rond", color)
    {
        Rayon = rayon;
    }

    public RondDTO() : base("Rond", null)
    {
    }
}