using App.Class;
using System.Text.Json.Serialization;

namespace API.ClassDTO;

public class RondDTO : FormsDTO
{
    [JsonPropertyOrder(3)]
    public double Rayon{ get; set; }

    public RondDTO(double rayon, string color):base("Rond", color)
    {
        Rayon = rayon;
    }
    public RondDTO(){}
}