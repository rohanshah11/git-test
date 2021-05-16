using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/doctors")]
    public class DoctorsController : Controller
    {
        private readonly DoctorsRepository _doctorsRepository;
        private readonly DoctorsService _doctorsService;
        private readonly IMapper _mapper;
        private readonly PaginatedMetaService _paginatedMetaService;
        private readonly FileHelper _fileHelper;

        public DoctorsController(DoctorsRepository doctorsRepository, DoctorsService doctorsService, IMapper mapper, PaginatedMetaService paginatedMetaService, FileHelper fileHelper)
        {
            _doctorsRepository = doctorsRepository;
            _doctorsService = doctorsService;
            _mapper = mapper;
            _paginatedMetaService = paginatedMetaService;
            _fileHelper = fileHelper;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index(DoctorsFilter filter = null)
        {
            try
            {
                var doctors = _doctorsRepository.getQueryable();

                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    doctors = doctors.Where(a => a.name.Contains(filter.title));
                }

                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(doctors.Count(), filter.page, filter.number_of_rows);

                doctors = doctors.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

                return View(doctors.OrderByDescending(a => a.doctor_id).ToList());
            }

            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("/admin");

            }
        }
        [Route("new")]
        public IActionResult add()
        {
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(DoctorsDto doctor_dto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = doctor_dto.name;
                        doctor_dto.image = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _doctorsService.save(doctor_dto);
                    AlertHelper.setMessage(this, "Doctors saved successfully.", messageType.success);

                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(doctor_dto);
        }

        [HttpGet]
        [Route("edit/{doctor_id}")]
        public IActionResult edit(long doctor_id)
        {
            try
            {
                var doctor = _doctorsRepository.getById(doctor_id);
                DoctorsDto dto = _mapper.Map<DoctorsDto>(doctor);

                RouteData.Values.Remove("doctor_id");
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
        public IActionResult edit(DoctorsDto doctorsDto, IFormFile file = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string fileName = doctorsDto.name;
                        doctorsDto.image = _fileHelper.saveImageAndGetFileName(file, fileName);

                    }
                    _doctorsService.update(doctorsDto);
                    AlertHelper.setMessage(this, "Doctors updated successfully.");
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(doctorsDto);
        }

        [HttpGet]
        [Route("delete/{doctor_id}")]
        public IActionResult delete(long doctor_id)
        {
            try
            {
                _doctorsService.delete(doctor_id);
                AlertHelper.setMessage(this, "Doctor deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("enable/{doctor_id}")]
        public IActionResult enable(long doctor_id)
        {
            try
            {
                _doctorsService.enable(doctor_id);
                AlertHelper.setMessage(this, "Doctor enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{doctor_id}")]
        public IActionResult disable(long doctor_id)
        {
            try
            {
                _doctorsService.disable(doctor_id);
                AlertHelper.setMessage(this, "Doctor disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}