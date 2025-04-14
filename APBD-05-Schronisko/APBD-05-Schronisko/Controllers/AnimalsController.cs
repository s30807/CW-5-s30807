using APBD_05_Schronisko.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_05_Schronisko.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    public static List<Animal> Animals = [
    new Animal()
    {
        Id = 0,
        Category = "Cats",
        Color = "Blue",
        Name = "Cat",
        Mass = 15.0,
    },
    new Animal()
    {
        Id = 1,
        Category = "Dogs",
        Color = "Red",
        Name = "Dog",
        Mass = 17.0,
    }];

    //1
    [HttpGet]
    public IActionResult GetAllAnimals()
    {
        return Ok(Animals);
    }
    
    //2
    [HttpGet]
    [Route("animal/{id}")]
    public IActionResult GetAnimalById(int id)
    {
        var animal = Animals.FirstOrDefault(e => e.Id == id);

        if (animal == null)
        {
            return NotFound($"Animal with id: {id} not found");
        }
    
        return Ok(animal);
    }
    
    //3
    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        var nextId = Animals.Max(e => e.Id) + 1;
        
        animal.Id = nextId;
        Animals.Add(animal);
        
        return CreatedAtAction(nameof(GetAnimalById), new { id = animal.Id }, animal);
    }
    
    //4
    [HttpPut]
    [Route("{id}")]
    public IActionResult ReplaceStudent(int id, Animal animal)
    {
        var animalToUpdate = Animals.FirstOrDefault(e => e.Id == id);

        if (animalToUpdate == null)
        {
            return NotFound($"Animal with id: {id} not found");
        }
        
        animalToUpdate.Name = animal.Name;
        animalToUpdate.Category = animal.Category;
        animalToUpdate.Mass = animal.Mass;
        animalToUpdate.Color = animal.Color;
        
        return NoContent();
    }
    
    //5
    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animalToDelete = Animals.FirstOrDefault(e => e.Id == id);

        if (animalToDelete == null)
        {
            return NotFound($"Animal with id: {id} not found");
        }
        Animals.Remove(animalToDelete);
        
        return NoContent();
    }
    
    //6
    [HttpGet]
    [Route("name/{name}")]
    public IActionResult GetAnimalByName(string name)
    {
        var animal = Animals.FirstOrDefault(e => e.Name == name);

        if (animal == null)
        {
            return NotFound($"Animal with name: {name} not found");
        }
    
        return Ok(animal);
    }
    
    
    
}