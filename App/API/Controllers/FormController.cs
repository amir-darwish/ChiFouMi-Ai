using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using App.Class;
using API.ClassDTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private static List<Form> _Forms = new List<Form>(); 

        public FormController()
        {
            if (!_Forms.Any())
            {
                _Forms.Add(new Rond(5, "Black"));
                _Forms.Add(new Triangle(5, 4, 3, "RED"));
                _Forms.Add(new Rectangle(1, 1, "WHITE"));
                _Forms.Add(new Rectangle(1, 1, "WHITE"));
            }
        }

        [HttpGet]
        public IActionResult GetForms()
        {
            var formsDto = _Forms.Select<Form, object>(form =>
            {
                switch (form)
                {
                    case Rond r:
                        return new RondDTO { Type = "Rond", Color = r.getColor(), Rayon = r.getRayon() };
                    case Rectangle rec:
                        return new RectangleDTO
                        {
                            Type = "Rectangle", Color = rec.getColor(), Length = rec.getLongueur(),
                            Width = rec.getLargeur()
                        };
                    case Triangle tri:
                        return new TriangleDTO()
                        {
                            Type = "Triangle", Color = tri.getColor(), Adjacent = tri.getAdjacent(),
                            Opposite = tri.getOppose(), Hypotenuse = tri.getHypotenuse()
                        };
                    default:
                        return null;
                }
            }).ToList();
            return Ok(formsDto);
        }
// get by index
    [HttpGet("{id}")]
        public IActionResult GetFormById(int id)
        {
            if (id < 0 || id >= _Forms.Count)
            {
                return NotFound($"No form found with ID {id}");
            }

            var form = _Forms[id];
            switch (form)
            {
                case Rond r:
                    return Ok(new RondDTO { Type = "Rond", Color = r.getColor(), Rayon = r.getRayon() });
                case Rectangle rec:
                    return Ok(new RectangleDTO { Type = "Rectangle", Color = rec.getColor(), Length = rec.getLongueur(), Width = rec.getLargeur() });
                case Triangle tri:
                    return Ok(new TriangleDTO() { Type = "Triangle", Color = tri.getColor(), Adjacent = tri.getAdjacent(), Opposite = tri.getOppose(), Hypotenuse = tri.getHypotenuse() });
                default:
                    return NotFound();
            }
        }
        [HttpPost]
        public IActionResult AddForm([FromBody] FormsDTO form)
        {
            if (form == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            switch (form.Type.ToLower())
            {
                case "rond":
                    if (form.Rayon.Value != 0 && !string.IsNullOrEmpty(form.Color))
                    {
                        var rond = new Rond(form.Rayon.Value, form.Color);
                        _Forms.Add(rond);
                        return Ok("Done adding form.");
                        
                    } 
                    return BadRequest("Rayon and Color are required for Rond.");
                break;
                case "triangle":
                    if ((form.Adjacent.Value != 0 && form.Opposite.Value !=0 && form.Hypotenuse.Value !=0) && !string.IsNullOrEmpty(form.Color))
                    {
                        var triangle  = new Triangle(form.Adjacent.Value, form.Opposite.Value, form.Hypotenuse.Value, form.Color);
                        _Forms.Add(triangle);
                        return Ok("Done adding triangle.");
                    }
                    return BadRequest("Adjacent and Opposite are required for triangle.");
                break;
                case "rectangle":
                    if ((form.Length.Value != 0 && form.Width.Value != 0) && !string.IsNullOrEmpty(form.Color))
                    {
                        var rectangle = new Rectangle(form.Length.Value, form.Width.Value, form.Color);
                        _Forms.Add(rectangle);
                        return Ok("Done adding rectangle.");
                    }
                    return BadRequest("Length and Width are required for rectangle.");
            }

            return BadRequest("Invalid Type.");
        }
        [HttpPut("{index}")]
        public IActionResult UpdateForm(int index, [FromBody] FormsDTO updatedForm)
        {
            if (index < 0 || index >= _Forms.Count)
            {
                return NotFound("Form not found.");
            }

            // bring object from list
            var existingForm = _Forms[index];

            // edite by type
            switch (existingForm)
            {
                case Rond rond:
                    if (updatedForm.Rayon.HasValue)
                        rond.setRayon(updatedForm.Rayon.Value);
                    if (!string.IsNullOrEmpty(updatedForm.Color))
                        rond.setColor(updatedForm.Color);
                    break;

                case Rectangle rect:
                    if (updatedForm.Length.HasValue)
                        rect.SetLongueur(updatedForm.Length.Value);
                    if (updatedForm.Width.HasValue)
                        rect.setLargeur(updatedForm.Width.Value);
                    if (!string.IsNullOrEmpty(updatedForm.Color))
                        rect.setColor(updatedForm.Color);
                    break;

                case Triangle tri:
                    if (updatedForm.Adjacent.HasValue)
                        tri.setAdjacent(updatedForm.Adjacent.Value);
                    if (updatedForm.Opposite.HasValue)
                        tri.setOppose(updatedForm.Opposite.Value);
                    if (updatedForm.Hypotenuse.HasValue)
                        tri.setHypotenuse(updatedForm.Hypotenuse.Value);
                    if (!string.IsNullOrEmpty(updatedForm.Color))
                        tri.setColor(updatedForm.Color);
                    break;

                default:
                    return BadRequest("Unsupported form type.");
            }

            return Ok(new { message = "Form updated successfully." });
        }
        [HttpDelete("{index}")]
        public IActionResult DeleteForm(int index)
        {
            if (index < 0 || index >= _Forms.Count)
            {
                return NotFound("Form not found.");
            }

            _Forms.RemoveAt(index);

            return Ok(new { message = "Form deleted successfully." });
        }

        
    }
}