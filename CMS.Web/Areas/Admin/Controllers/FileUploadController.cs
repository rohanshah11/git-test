using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Admin.FilterModel;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Areas.Core.ViewModels;
using CMS.Web.Controllers;
using CMS.Web.Exceptions;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/file-upload")]
    public class FileUpload : BaseController
    {
        private FileUploadService _fileUploadService;
        private FileUploadRepository _fileUploadRepo;
        private readonly PaginatedMetaService _paginatedMetaService;
        private IMapper _mapper;
        private FileHelper _fileHelper;
        public FileUpload(FileHelper fileHelper, IMapper mapper, FileUploadService fileUploadService, FileUploadRepository fileUploadRepo, PaginatedMetaService paginatedMetaService)
        {
            _fileUploadService = fileUploadService;
            _fileUploadRepo = fileUploadRepo;
            _paginatedMetaService = paginatedMetaService;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(FileUploadFilter filter = null)
        {
            try
            {
                var files = _fileUploadRepo.getQueryable();
                if (!string.IsNullOrWhiteSpace(filter.title))
                {
                    files = files.Where(a => a.title.Contains(filter.title));
                }
                ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(files.Count(), filter.page, filter.number_of_rows);
                files = files.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);
                var file = files.ToList();
                var filesIndexVM = getViewModelFrom(file);
                return View(filesIndexVM);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return Redirect("index");
            }
        }

        [HttpGet]
        [Route("new")]
        public IActionResult add()
        {
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult add(FileUploadModel model, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    throw new CustomException("File must be provided.");
                }

                if (ModelState.IsValid)
                {
                    FileUploadDto fileUploadDto = new FileUploadDto();
                    fileUploadDto.title = model.title;

                    fileUploadDto.file_name = _fileHelper.saveFileAndGetFileName(file, model.title);
                    fileUploadDto.description = model.description;
                    fileUploadDto.is_enabled = model.is_enabled;

                    _fileUploadService.save(fileUploadDto);
                    AlertHelper.setMessage(this, "File saved successfully.", messageType.success);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }

            return View(model);
        }

        [HttpGet]
        [Route("edit/{file_upload_id}")]
        public IActionResult edit(long file_upload_id)
        {
            try
            {
                CMS.Core.Entity.FileUpload fileUpload = _fileUploadRepo.getById(file_upload_id);
                FileUploadModel fileUploadModel = _mapper.Map<FileUploadModel>(fileUpload);
                return View(fileUploadModel);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
                return RedirectToAction("index");
            }

        }

        [HttpPost]
        [Route("edit")]
        public IActionResult edit(FileUploadModel model, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FileUploadDto fileUploadDto = new FileUploadDto();

                    fileUploadDto.file_upload_id = model.file_upload_id;
                    fileUploadDto.title = model.title;
                    fileUploadDto.description = model.description;

                    if (file != null)
                    {
                        fileUploadDto.file_name = _fileHelper.saveFileAndGetFileName(file, model.title);
                    }
                    
                    fileUploadDto.is_enabled = model.is_enabled;
                    _fileUploadService.update(fileUploadDto);
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return View(model);
        }

        [HttpGet]
        [Route("enable/{file_upload_id}")]
        public IActionResult enable(long file_upload_id)
        {
            try
            {
                _fileUploadService.enable(file_upload_id);
                AlertHelper.setMessage(this, "File enabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("disable/{file_upload_id}")]
        public IActionResult disable(long file_upload_id)
        {
            try
            {
                _fileUploadService.disable(file_upload_id);
                AlertHelper.setMessage(this, "File disabled successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("delete/{file_upload_id}")]
        public IActionResult delete(long file_upload_id)
        {
            try
            {
                _fileUploadService.delete(file_upload_id);
                AlertHelper.setMessage(this, "File deleted successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index));
        }

        private FileUploadIndexViewModel getViewModelFrom(List<CMS.Core.Entity.FileUpload> files)
        {
            FileUploadIndexViewModel vm = new FileUploadIndexViewModel();
            vm.file_upload_details = new List<FileUploadDetailModel>();
            foreach (var file in files)
            {
                var fileDetail = _mapper.Map<FileUploadDetailModel>(file);
                vm.file_upload_details.Add(fileDetail);
            }

            return vm;
        }
    }
}