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
            this._context = context;

            if (this._context.Mementos.Count() <= 1) {
                this._context.Mementos
                    .Add(
                        new Memento { 
                            Owner = "Item2",
                            Type = "asd",
                            Phrase = "goingsparrow"
                });
                this._context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Memento>> GetAll() {
            return this._context.Mementos.ToList();
        }

        [Route("GetByHashtag/{hashtag}")]
        [HttpGet("{hashtag}", Name = "GetByHashtag")]
        public ActionResult<List<MementosView>> GetByHashtag(string hashtag) {
            var items = this._context.MementosView
                .Where(x => x.Phrase.ToUpper() == hashtag.ToUpper())
                .ToList();
            if (items == null) {
                return NotFound();
            }
            return items;
        }

        [HttpGet("{id}", Name = "Memento")]
        public ActionResult<Memento> GetById(long id) {
            var item = this._context.Mementos.Find(id);
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
            this._context.Mementos.AddRange(twitter.Mementos);
            this._context.SaveChanges();
            return CreatedAtRoute("GetByHashtag", hashtag);
        }
    }
}