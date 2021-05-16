using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Repo
{
    public class FileUploadRepositoryImpl : BaseRepositoryImpl<FileUpload>, FileUploadRepository
    {
        private readonly AppDbContext _appDbContext;

        public FileUploadRepositoryImpl(AppDbContext context, DetailsEncoder<FileUpload> detailsEncoder, HtmlEncodingClassHelper htmlEncodingClassHelper) : base(context, detailsEncoder, htmlEncodingClassHelper)
        {
            _appDbContext = context;
        }

        public FileUpload getByName(string name)
        {
            return _appDbContext.file_uploads.Where(a => a.title == name).SingleOrDefault();
        }
    }
}