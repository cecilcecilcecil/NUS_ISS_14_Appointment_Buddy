using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Services.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Services.API.Infrastructure
{
    public class ServicesRepositoryService : IServicesRepositoryService
    {
        private readonly AppointmentBuddyDBContext _context;
        private readonly ILogger<ServicesRepositoryService> _logger;

        public ServicesRepositoryService(AppointmentBuddyDBContext context, ILogger<ServicesRepositoryService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
