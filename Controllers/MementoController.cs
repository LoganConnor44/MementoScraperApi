using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MementoScraperApi.Models;
using MementoScraperApi.Database;

namespace MementoScraperApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MementoController : ControllerBase {
        private readonly DataContext _context;

        public MementoController(DataContext context) {
            _context = context;

            if (_context.Mementos.Count() <= 1) {
                _context.Mementos
                    .Add(
                        new Memento { 
                            Owner = "Item2",
                            Type = "asd",
                            Phrase = "goingsparrow"
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Memento>> GetAll() {
            return _context.Mementos.ToList();
        }

        [Route("GetByHashtag/{hashtag}")]
        [HttpGet("{hashtag}", Name = "GetByHashtag")]
        public ActionResult<List<MementosView>> GetByHashtag(string hashtag) {
            var items = _context.MementosView
                .Where(x => x.Phrase.ToUpper() == hashtag.ToUpper())
                .ToList();
            if (items == null) {
                return NotFound();
            }
            return items;
        }

        [HttpGet("{id}", Name = "Memento")]
        public ActionResult<Memento> GetById(long id) {
            var item = _context.Mementos.Find(id);
            if (item == null) {
                return NotFound();
            }
            return item;
        }

        [Route("CreateMementos/{Hashtag}")]
        [HttpPost("{hashtag}", Name = "CreateMementosForHashtag")]
        public IActionResult CreateMementosForHashtag(string hashtag) {
            var twitter = new Twitter();
            var results = twitter.GetSearchFor("#" + hashtag);
            var mediaOnly = twitter.GetTweetsWithMedia(results);
            twitter.CreateMementos(mediaOnly);
            _context.Mementos.AddRange(twitter.Mementos);
            _context.SaveChanges();
            return CreatedAtRoute("GetByHashtag", hashtag);
        }
    }
}