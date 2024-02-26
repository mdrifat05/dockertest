using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbDocker.Dtos;

namespace MongoDbDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly MongoDbContext _db;

        public PeopleController(MongoDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public ActionResult<PeopleDto> Create(CreatePeopleDto personDto)
        {
            try
            {    
                var person = new Person
                {
                    Id = Guid.NewGuid(),
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    Age = personDto.Age
                };  
                _db._collection.InsertOne(person);

                var insertedPersonDto = new PeopleDto(person.Id, person.FirstName, person.LastName, person.Age);

                return CreatedAtAction(nameof(Get), new { id = person.Id }, insertedPersonDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create person: {ex.Message}");
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<PeopleDto>> GetAll()
        {
            var people = _db._collection.Find(_ => true).ToList();

            var peopleDtos = people.Select(person => new PeopleDto(person.Id, person.FirstName, person.LastName, person.Age));

            return Ok(peopleDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<PeopleDto> Get(Guid id)
        {
            var person = _db._collection.Find(p => p.Id == id).FirstOrDefault();

            if (person == null)
            {
                return NotFound();
            }
            var personDto = new PeopleDto(person.Id, person.FirstName, person.LastName, person.Age);

            return Ok(personDto);
        }
    }
}
