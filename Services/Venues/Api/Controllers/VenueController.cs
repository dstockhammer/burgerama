using System;
using System.Collections.Generic;
using System.Web.Http;
using Burgerama.Services.Venues.Api.Models;
using Burgerama.Services.Venues.Core;

namespace Burgerama.Services.Venues.Api.Controllers
{
    public class VenueController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<VenueModel> Get()
        {
            return new[]
            {
                new VenueModel
                {
                    Id = Guid.NewGuid(),
                    Title = "Test venue 1",
                    Votes = 2
                },
                new VenueModel
                {
                    Id = Guid.NewGuid(),
                    Title = "Test venue 2",
                    Votes = 3,
                    Rating = 4.5
                }
            };
        }

        // GET api/<controller>/5
        public VenueModel Get(string id)
        {
            return new VenueModel
            {
                Id = Guid.Parse(id),
                Title = "Test venue"
            };
        }

        // POST api/<controller>
        public void Post(VenueModel model)
        {
            var venue = new Venue(model.Title);
        }
    }
}