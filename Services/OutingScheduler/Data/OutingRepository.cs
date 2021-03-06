﻿using Burgerama.Common.DataAccess.Rest;
using Burgerama.Services.OutingScheduler.Data.Rest.Converters;
using Burgerama.Services.OutingScheduler.Data.Rest.Models;
using Burgerama.Services.OutingScheduler.Domain;
using Burgerama.Services.OutingScheduler.Domain.Contracts;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Burgerama.Services.OutingScheduler.Data.Rest
{
    public sealed class OutingRepository : RestRepository, IOutingRepository
    {
        protected override string GetTargetServiceKey()
        {
            return "outings";
        }

        public IEnumerable<Outing> GetAll()
        {
            var request = new RestRequest(Method.GET);
            var response = Client.Execute<List<OutingModel>>(request);

            return response.Data.Select(o => o.ToDomain());
        }
    }
}
