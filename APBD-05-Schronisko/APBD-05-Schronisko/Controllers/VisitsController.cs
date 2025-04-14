using APBD_05_Schronisko.Models;
using APBD_05_Schronisko.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace APBD_05_Schronisko.Controllers;

[ApiController]
[Route("[controller]")]
public class VisitsController : ControllerBase
{
    public static List<Visit> Visits = [
        new Visit()
        {
            Date = new DateTime(2024, 2, 3),
            Animal = AnimalsController.Animals[0],
            Description = "Przepisano leki",
            Price = 36.00
        },
        new Visit()
        {
            Date = new DateTime(2023, 5, 16),
            Animal = AnimalsController.Animals[1],
            Description = "Wizyta kontrolna",
            Price = 87.00
        }];
    
    //1  chcielibyśmy mieć możliwość pobrania listy wizyt powiązanych z danym zwierzęciem
    [HttpGet]
    [Route("animal/{id}")]
    public IActionResult GetVisitsByAnimalId(int id)
    {
        var animal = AnimalsController.Animals.FirstOrDefault(e => e.Id == id);

        if (animal == null)
        {
            return NotFound($"Visit with this animal's id: {id} not found");
        }
        
        var visitsForAnimal = VisitsController.Visits
            .Where(v => v.Animal.Id == id)
            .ToList();

        return Ok(visitsForAnimal);
    }
    
    //2 chcielibyśmy mieć możliwość dodawania nowych wizyt
    [HttpPost]
    public IActionResult AddVisit(Visit visit)
    {
        var nextId = Visits.Max(e => e.Id) + 1;
        
        visit.Id = nextId;
        Visits.Add(visit);
        
        return CreatedAtAction(nameof(GetVisitsByAnimalId), new { id = visit.Animal.Id }, visit);
    }
}