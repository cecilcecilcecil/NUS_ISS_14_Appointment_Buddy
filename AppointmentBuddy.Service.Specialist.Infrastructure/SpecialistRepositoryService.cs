using AppointmentBuddy.Core.Common.Helper;
using AppointmentBuddy.Infrastructure.Repository;
using AppointmentBuddy.Service.Specialist.API.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Service.Specialist.API.Infrastructure
{
    public class SpecialistRepositoryService : ISpecialistRepositoryService
    {
        private readonly AppointmentBuddyDBContext _context;
        private readonly ILogger<SpecialistRepositoryService> _logger;

        public SpecialistRepositoryService(AppointmentBuddyDBContext context, ILogger<SpecialistRepositoryService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
