﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Entity;
using CMS.Core.Enums;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using CMS.Web.Areas.Core.Models;
using CMS.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Areas.Core.Controllers
{
    [Authorize]
    [Area("admin")]
    [Route("admin/setup")]
    public class SetupController : Controller
    {
        private readonly SetupRepository _setupRepo;
        private readonly SetupService _setupService;
        private readonly FileHelper _fileHelper;

        public SetupController(FileHelper fileHelper, SetupRepository setupRepo, SetupService setupService)
        {
            _setupRepo = setupRepo;
            _setupService = setupService;
            _fileHelper = fileHelper;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index(List<Setup> setups = null)
        {
            if (setups.Count == 0)
            {
                setups = _setupRepo.getQueryable().ToList();
            }
            return View(setups);
        }

        [HttpPost]
        [Route("save")]
        public IActionResult save(List<Setup> datas, IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string qrcode = "QR";
                    var filename = _fileHelper.saveImageAndGetFileName(file, qrcode);

                    Setup setup = new Setup();
                    setup.key = Web.Models.SetupKeys.getQrCodeKey;
                    setup.value = filename;
                    datas.Add(setup);
                }
                datas.Add(new Setup() { key = Web.Models.SetupKeys.getLocationKey, value = $"{Request.Form["latitude_id"]},{Request.Form["longitude_id"]}" });
                datas.RemoveAll(a => string.IsNullOrWhiteSpace(a.value));

                _setupService.saveOrUpdate(datas);


                AlertHelper.setMessage(this, "Setup saved successfully.", messageType.success);
            }
            catch (Exception ex)
            {
                AlertHelper.setMessage(this, ex.Message, messageType.error);
            }
            return RedirectToAction(nameof(Index), datas);
        }
    }
}