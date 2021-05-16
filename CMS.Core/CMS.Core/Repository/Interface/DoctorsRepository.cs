using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface DoctorsRepository
    {
        void insert(Doctors doctors);
        void delete(Doctors doctors);
        void update(Doctors doctors);
        List<Doctors> getAll();
        Doctors getById(long doctor_id);
        IQueryable<Doctors> getQueryable();
        Doctors getBySlug(string slug);

    }
}
