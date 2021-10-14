﻿using System.Collections.Generic;
using System.Threading.Tasks;
using M = AppointmentBuddy.Core.Model;

namespace NUS_ISS_14_Appointment_Buddy.Interface
{
    public interface IAppointmentService
    {
        Task<M.Appointment> GetAppointmentByAppointmentId(string apptId, string token);
        Task<M.PaginatedResults<M.Appointment>> GetAllAppointments(string token, string dateFrom, string dateTo, int page, int pageSize);
    }
}
