using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MementoScraperApi.Models;
using MementoScraperApi.Database;

namespace MementoScraperApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MementoController : ControllerBase
    {
        private readonly DataContext _context;

        public MementoController(DataContext context)
        {
            _context = context;

            if (_context.Mementos.Count() == 0)
            {
                _context.Mementos
                    .Add(
                        new Memento { 
                            Owner = "Item1",
                            Type = "asd",
                            Creation = System.DateTime.Now
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Memento>> GetAll()
        {
            return _context.Mementos.ToList();
        }

        [HttpGet("{id}", Name = "Memento")]
        public ActionResult<Memento> GetById(long id)
        {
            var item = _context.Mementos.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}