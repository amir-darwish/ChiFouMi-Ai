using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API.ClassDTO;
using App.Class;


namespace MyGameAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ShapesManagerController : ControllerBase
    {
        
        private static List<ShapeManager> _compositions = new List<ShapeManager>();

       public ShapesManagerController()
        {
            if (!_compositions.Any())
            {
                _compositions.Add(new ShapeManager());
                _compositions[0].AddForm(new Rond(5,"Yellow"));
                _compositions[0].AddForm(new Rectangle(10, 20, "Blue"));
                _compositions[0].AddForm(new Triangle(3, 4, 5, "Green"));
            }
        }
       // get all
        [HttpGet]
        public IActionResult GetCompositions()
        {
            var compositionsDto = _compositions.Select(comp => new ShapeManagerDTO()
            {
                ID = comp.ID,
                Forms = comp.GetForms()
                    .Select<Form, FormsDTO>(form => 
                    {
                        switch (form)
                        {
                            case Rond r:
                                return new RondDTO { Type = "Rond", Color = r.getColor(), Rayon = r.getRayon() };
                            case Rectangle rec:
                                return new RectangleDTO { Type = "Rectangle", Color = rec.getColor(), Length = rec.getLongueur(), Width = rec.getLargeur() };
                            case Triangle tri:
                                return new TriangleDTO { Type = "Triangle", Color = tri.getColor(), Adjacent = tri.getAdjacent(), Opposite = tri.getOppose(), Hypotenuse = tri.getHypotenuse() };
                            default:
                                return null; 
                        }
                    })
                    .Where(dto => dto != null)
                    .ToList()
            }).ToList();

            return Ok(compositionsDto);
        }
        
        // get by id
        [HttpGet("{id}/compositions")]
        public IActionResult GetFormsInComposition(int id)
        {
            var composition = _compositions.FirstOrDefault(comp => comp.ID == id);
            if (composition == null)
            {
                return NotFound($"Composition with ID {id} not found.");
            }

            var formsDto = composition.GetForms()
                .Select<Form, FormsDTO>(form =>
                {
                    switch (form)
                    {
                        case Rond r:
                            return new RondDTO { Type = "Rond", Color = r.getColor(), Rayon = r.getRayon() };
                        case Rectangle rec:
                            return new RectangleDTO { Type = "Rectangle", Color = rec.getColor(), Length = rec.getLongueur(), Width = rec.getLargeur() };
                        case Triangle tri:
                            return new TriangleDTO { Type = "Triangle", Color = tri.getColor(), Adjacent = tri.getAdjacent(), Opposite = tri.getOppose(), Hypotenuse = tri.getHypotenuse() };
                        default:
                            return null;
                    }
                })
                .Where(dto => dto != null) 
                .ToList();

            return Ok(formsDto);
        }


// post composition
        [HttpPost]
        public IActionResult AddComposition([FromBody] ShapeManagerDTO newComposition)
        {
            if (newComposition == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            // check if ID available
            if (_compositions.Any(comp => comp.ID == newComposition.ID))
            {
                return BadRequest("Composition with the same ID already exists.");
            }

            // Creat new composition
            var composition = new ShapeManager();

            // Add forms to  Composition
            foreach (var formDto in newComposition.Forms)
            {
                switch (formDto.Type.ToLower())
                {
                    case "rond":
                        if (formDto.Rayon.Value != 0 && !string.IsNullOrEmpty(formDto.Color))
                        {
                            composition.AddForm(new Rond(formDto.Rayon.Value, formDto.Color));
                        }
                        break;

                    case "rectangle":
                        if (formDto.Length.Value != 0&& formDto.Width.Value != 0 && !string.IsNullOrEmpty(formDto.Color))
                        {
                            composition.AddForm(new Rectangle(formDto.Length.Value, formDto.Width.Value, formDto.Color));
                        }
                        break;

                    case "triangle":
                        if (formDto.Adjacent.Value != 0 && formDto.Opposite.Value != 0 && formDto.Hypotenuse.Value != 0 && !string.IsNullOrEmpty(formDto.Color))
                        {
                            composition.AddForm(new Triangle(formDto.Adjacent.Value, formDto.Opposite.Value, formDto.Hypotenuse.Value, formDto.Color));
                        }
                        break;

                    default:
                        return BadRequest($"Invalid shape type: {formDto.Type}");
                }
            }

            // Add to list 
            _compositions.Add(composition);

            return Ok("Composition Successfully added."); 
        }
        
// post form to composition 
        [HttpPost("{id}/add-form")]
        public IActionResult AddFormToComposition(int id, [FromBody] FormsDTO formDto)
        {
            // search for composition by id 
            var composition = _compositions.FirstOrDefault(comp => comp.ID == id);
            if (composition == null)
            {
                return NotFound($"Composition with ID {id} not found.");
            }

            if (formDto == null)
            {
                return BadRequest("Form data cannot be null.");
            }

            //Add form to Composition
            switch (formDto.Type.ToLower())
            {
                case "rond":
                    if (formDto.Rayon.Value != 0 && !string.IsNullOrEmpty(formDto.Color))
                    {
                        composition.AddForm(new Rond(formDto.Rayon.Value, formDto.Color));
                    }
                    else
                    {
                        return BadRequest("Rayon and Color are required for Rond.");
                    }
                    break;

                case "rectangle":
                    if (formDto.Length.Value != 0 && formDto.Width.Value != 0 && !string.IsNullOrEmpty(formDto.Color))
                    {
                        composition.AddForm(new Rectangle(formDto.Length.Value, formDto.Width.Value, formDto.Color));
                    }
                    else
                    {
                        return BadRequest("Length, Width, and Color are required for Rectangle.");
                    }
                    break;

                case "triangle":
                    if (formDto.Adjacent.Value != 0 && formDto.Opposite.Value != 0 && formDto.Hypotenuse.Value != 0 && !string.IsNullOrEmpty(formDto.Color))
                    {
                        composition.AddForm(new Triangle(formDto.Adjacent.Value, formDto.Opposite.Value, formDto.Hypotenuse.Value, formDto.Color));
                    }
                    else
                    {
                        return BadRequest("Adjacent, Opposite, Hypotenuse, and Color are required for Triangle.");
                    }
                    break;

                default:
                    return BadRequest($"Invalid shape type: {formDto.Type}");
            }

            return Ok($"Form added successfully to composition {id}.");
        }

    }
}