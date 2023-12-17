using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Services;

namespace SampleCrud.API.Workers
{
    public class InsertWorker : IHostedService
    {
        private readonly ILogger<InsertWorker> _logger; 
        private readonly IPersonService _personService;

        public InsertWorker(ILogger<InsertWorker> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("InsertWorker running at: {time}", DateTimeOffset.Now);
            _personService.Add(new Person
            {
                
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("InsertWorker running at: {time}", DateTimeOffset.Now);
            return Task.CompletedTask;
        }
    }
}