using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class DoctorsMakerImpl : DoctorsMaker
    {
        private readonly SlugGenerator _slugGenerator;

        public DoctorsMakerImpl(SlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }
        public void copy(Doctors doctors, DoctorsDto doctorsDto)
        {
            doctors.doctor_id = doctorsDto.doctor_id;
            doctors.name = doctorsDto.name;
            doctors.address = doctorsDto.address;
            doctors.education = doctorsDto.education;
            doctors.speciality = doctorsDto.speciality;
            doctors.experience = doctorsDto.experience;
            doctors.contact_number = doctorsDto.contact_number;
            doctors.email = doctorsDto.email;
            doctors.website = doctorsDto.website;
            doctors.facebook = doctorsDto.facebook;
            doctors.image = doctorsDto.image;
            doctors.twitter = doctorsDto.twitter;
            doctors.is_active = doctorsDto.is_active;
            doctors.is_contact_enabled = doctorsDto.is_contact_enabled;
            doctors.slug = _slugGenerator.generate(doctorsDto.name);

        }
    }
}
