using CMS.Core.Repository.Interface;
using CMS.Web.FilterModel;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CMS.Web.Controllers
{
    public class FIleController : Controller
    {
        private readonly FileUploadRepository _fileUploadRepo;
        private readonly PaginatedMetaService _paginatedMetaService;

        public FIleController(FileUploadRepository fileUploadRepo,PaginatedMetaService paginatedMetaService)
        {
            _fileUploadRepo = fileUploadRepo;
            _paginatedMetaService = paginatedMetaService;
        }

        public IActionResult Index(FileFilter filter=null)
        {
            var files = _fileUploadRepo.getQueryable().Where(a => a.is_enabled == true);

            ViewBag.pagerInfo = _paginatedMetaService.GetMetaData(files.Count(), filter.page, filter.number_of_rows);


            files = files.Skip(filter.number_of_rows * (filter.page - 1)).Take(filter.number_of_rows);

            return View(files.ToList());
        }
    }
}