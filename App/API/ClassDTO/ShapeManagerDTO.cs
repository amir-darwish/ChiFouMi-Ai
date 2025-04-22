using Newtonsoft.Json;

namespace API.ClassDTO;
using App.Class;

using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

[JsonObject]

public class ShapeManagerDTO
{
    public int ID { get; set; } 
    public List<FormsDTO> Forms { get; set; } 
}