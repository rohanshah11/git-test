using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Enums;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class AppointmentRepositoryImpl : BaseRepositoryImpl<Appointment>, AppointmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public AppointmentRepositoryImpl(AppDbContext appDbContext, DetailsEncoder<Appointment> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(appDbContext, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = appDbContext;
        }

        public List<Appointment> getAppointmentWithinDate(DateTime start_date, DateTime end_date)
        {
            return _appDbContext.appointments.Where(a => a.entry_date.Date >= start_date && a.entry_date.Date <= end_date).ToList();
        }
    }
}
