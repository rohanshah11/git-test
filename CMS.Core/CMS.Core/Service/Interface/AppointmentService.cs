using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface AppointmentService
    {
        void save(AppointmentDto appointmentDto);
        void update(AppointmentDto appointmentDto);
        void delete(long appointment_id);
        void approved(long appointment_id);
        void cancelled(long appointment_id);


    }
}
