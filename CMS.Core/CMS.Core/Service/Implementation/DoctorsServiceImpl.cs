using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class DoctorsServiceImpl : DoctorsService
    {
        private readonly DoctorsRepository _doctorRepository;
        private readonly DoctorsMaker _doctorsMaker;

        public DoctorsServiceImpl(DoctorsRepository doctorsRepository, DoctorsMaker doctorsMaker)
        {
            _doctorRepository = doctorsRepository;
            _doctorsMaker = doctorsMaker;

        }
        public void delete(long doctor_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var doctors = _doctorRepository.getById(doctor_id);
                    if (doctors == null)
                    {
                        throw new ItemNotFoundException($" {doctor_id}  not found");

                    }
                    _doctorRepository.delete(doctors);
                    tx.Complete();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
      
        public void disable(long doctor_id)
        {
            try
            {
                var doctor = _doctorRepository.getById(doctor_id);
                if (doctor == null)
                    throw new ItemNotFoundException($"Doctor with id {doctor_id} doesnot exist.");

                doctor.is_active = false;
                _doctorRepository.update(doctor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void enable(long doctor_id)
        {
            try
            {
                var doctor = _doctorRepository.getById(doctor_id);
                if (doctor == null)
                    throw new ItemNotFoundException($"Doctor with id {doctor_id} doesnot exist.");

                doctor.is_active = true;
                _doctorRepository.update(doctor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void save(DoctorsDto doctorsDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Doctors doctors = new Doctors();
                    _doctorsMaker.copy(doctors, doctorsDto);
                    _doctorRepository.insert(doctors);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(DoctorsDto doctorsDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Doctors doctor = _doctorRepository.getById(doctorsDto.doctor_id);
                    if (doctor == null)
                    {
                        throw new ItemNotFoundException($"Doctor with ID {doctorsDto.doctor_id} doesnot Exit.");
                    }
                    _doctorsMaker.copy(doctor, doctorsDto);
                    _doctorRepository.update(doctor);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
