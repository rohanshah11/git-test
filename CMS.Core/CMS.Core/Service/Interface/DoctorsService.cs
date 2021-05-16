using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
   public interface DoctorsService
    {
        void save(DoctorsDto doctorsDto);
        void delete(long doctor_id);
        void update(DoctorsDto doctorsDto);
        void enable(long doctor_id);
        void disable(long doctor_id);
    }
}
