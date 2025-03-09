using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using App.Class;


namespace MyGameAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapesController : ControllerBase
    {
        private static List<string> shapes = new List<string> { "Triangle", "Rond", "Rectangle" };

        [HttpGet]
        public IActionResult GetShapes()
        {
            return Ok(shapes);
        }

        [HttpPost]
        public IActionResult AddShape([FromBody] string shape)
        {
            shapes.Add(shape);
            return Ok($"Shape {shape} added!");
        }
    }
}