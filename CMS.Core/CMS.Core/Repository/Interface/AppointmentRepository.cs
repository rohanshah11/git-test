using CMS.Core.Entity;
using CMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface AppointmentRepository
    {
        void insert(Appointment appointment);
        void update(Appointment appointment);
        void delete(Appointment appointment);
        List<Appointment> getAll();
        List<Appointment> getAppointmentWithinDate(DateTime start_date, DateTime end_date);

        Appointment getById(long appointment_id);
        IQueryable<Appointment> getQueryable();

    }
}
