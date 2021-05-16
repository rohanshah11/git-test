using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Areas.Core.ViewModels
{
    public class FileUploadIndexViewModel
    {
        public List<FileUploadDetailModel> file_upload_details { get; set; }
    }

    public class FileUploadDetailModel
    {
        public long file_upload_id { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        public string file_name { get; set; }
        public bool is_enabled { get; set; }
    }
}
