using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.ViewModels;
using CMS.Web.Areas.Core.FilterModel;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/appointment")]
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _appointmentService;
        private readonly AppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        public AppointmentController(AppointmentRepository appointmentRepository, AppointmentService appointmentService, IMapper mapper, PaginatedMetaService paginatedMetaService)
        {
            _appointmentService = appointmentService;
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
        }
        public IActionResult Index(AppointmentFilter filter = null)
        {
            try
            {
                var appointment = _appointmentRepository.getQueryable();

             

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(appointment.Count(), filter.page, filter.number_of_rows);

                appointment = appointment.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                appointment = appointment.Where(a => a.entry_date.Date >= filter.starting_date.Date && a.entry_date.Date <= filter.ending_date.Date);

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    appointment = appointment.Where(a => a.name.Contains(filter.title) && a.entry_date.Date >= filter.starting_date.Date && a.entry_date.Date <= filter.ending_date.Date);
                }

                var AppointmentDetail = appointment.ToList();


                var appointmentIndexVM = getViewModelFrom(AppointmentDetail);
                return View(appointmentIndexVM);
            }

            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");

            }
        }

        private AppointmentIndexViewModel getViewModelFrom(List<Appointment> appointmentDetail)
        {
            AppointmentIndexViewModel vm = new AppointmentIndexViewModel();
            vm.details = new List<AppointmentDetail>();
            foreach (var appointment in appointmentDetail)
            {
                var appointmentData = _mapper.Map<AppointmentDetail>(appointment);
                vm.details.Add(appointmentData);
            }

            return vm;
        }

        [Route("new")]
        public IActionResult add()
        {
            AppointmentDto dto = new AppointmentDto();
            return View(dto);
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(AppointmentDto appontmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _appointmentService.save(appontmentDto);
                    AlertHelper.setMessage(this, "Appointment saved successfully.", messageType.success);

                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(appontmentDto);
        }

        [HttpGet]
        [Route("edit/{Appointment_id}")]
        public IActionResult edit(long Appointment_id)
        {
            try
            {
                var appointment = _appointmentRepository.getById(Appointment_id);
                AppointmentDto dto = _mapper.Map<AppointmentDto>(appointment);

                RouteData.Values.Remove("Appointment_id");
                return View(dto);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(AppointmentDto appointmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _appointmentService.update(appointmentDto);
                    AlertHelper.setMessage(this, "Appointment updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(appointmentDto);
        }

        [HttpGet]
        [Route("delete/{Appointment_id}")]
        public IActionResult delete(long Appointment_id)
        {
            try
            {
                _appointmentService.delete(Appointment_id);
                AlertHelper.setMessage(this, "Appointment deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Route("approved/{appointment_id}")]
        public IActionResult approved(long appointment_id)
        {
            try
            {
                _appointmentService.approved(appointment_id);
                AlertHelper.setMessage(this, "Appointment agreed successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("cancelled/{appointment_id}")]
        public IActionResult cancelled(long appointment_id)
        {
            try
            {
                _appointmentService.cancelled(appointment_id);
                AlertHelper.setMessage(this, "Appointment cancelled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}