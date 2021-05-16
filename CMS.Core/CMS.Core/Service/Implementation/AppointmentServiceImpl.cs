using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
   
    public class AppointmentServiceImpl : AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly AppointmentMaker _appointmentMaker;
        public AppointmentServiceImpl(AppointmentRepository appointmentRepository, AppointmentMaker appointmentMaker)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentMaker = appointmentMaker;
        }
        public void approved(long appointment_id)
        {
            try
            {
                var appointment = _appointmentRepository.getById(appointment_id);
                if (appointment == null)
                    throw new ItemNotFoundException($"Appointment with id {appointment_id} doesnot exist.");
                appointment.Approved();
                _appointmentRepository.update(appointment);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void delete(long appointment_id)
        {
            try
            {
                using(TransactionScope transaction=new TransactionScope(TransactionScopeOption.Required))
                {
                    var appointment = _appointmentRepository.getById(appointment_id);
                    if(appointment==null)
                    {
                        throw new ItemNotFoundException($"service Category With Id {appointment} doesnot Exist.");
                    }
                    _appointmentRepository.delete(appointment);
                    transaction.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }        }

        public void cancelled(long appointment_id)
        {
            try
            {
                var appointment = _appointmentRepository.getById(appointment_id);
                if (appointment == null)
                    throw new ItemNotFoundException($"appointment with id {appointment_id} doesnot exist.");

                appointment.Cancelled();
                _appointmentRepository.update(appointment);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void save(AppointmentDto appointmentDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Appointment appointment  = new Appointment();
                    _appointmentMaker.copy(appointment, appointmentDto);
                    _appointmentRepository.insert(appointment);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(AppointmentDto appointmentDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Appointment appointment = _appointmentRepository.getById(appointmentDto.appointment_id);
                    if (appointment == null)
                    {
                        throw new ItemNotFoundException($"Appointment with ID {appointmentDto.appointment_id} doesnot Exit.");
                    }

                    _appointmentMaker.copy(appointment, appointmentDto);
                    _appointmentRepository.update(appointment);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }        }
    }
}
