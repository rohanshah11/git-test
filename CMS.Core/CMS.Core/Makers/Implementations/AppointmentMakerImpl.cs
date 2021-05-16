using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class AppointmentMakerImpl : AppointmentMaker
    {
        public void copy(Appointment appointment, AppointmentDto appointmentDto)
        {
            appointment.appointment_id = appointmentDto.appointment_id;
            appointment.name = appointmentDto.name;
            appointment.address = appointmentDto.address;
            appointment.country = appointmentDto.country;
            appointment.email = appointmentDto.email;
            appointment.contact_no = appointmentDto.contact_no;
            appointment.description = appointmentDto.description;
            appointment.appointment_date = appointmentDto.appointment_date;
            appointment.entry_date = appointmentDto.entry_date;
            appointment.type = appointmentDto.type;
        }
    }
}
