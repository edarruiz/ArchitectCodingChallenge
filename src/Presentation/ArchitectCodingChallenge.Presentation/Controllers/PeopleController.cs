using ArchitectCodingChallenge.Domain.Models;
using ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase.Abstractions;
using ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectCodingChallenge.Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase {

    public PeopleController(
        IJsonInMemoryDatabaseContext dbContext
    ) {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbContext.Connect();
        if (!_dbContext.Connected) {
            throw new JsonInMemoryDatabaseException("In-memory Json database could not be connect to the data source.");
        }
    }

    private IJsonInMemoryDatabaseContext _dbContext;

    [HttpGet]
    public async Task<List<PersonModel>> Get() {
        return await _dbContext.GetPeople() ?? new List<PersonModel>();
    }

    /*
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
     */
}
