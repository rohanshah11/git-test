using CMS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Core.Repository.Interface
{
    public interface FileUploadRepository
    {
        void insert(FileUpload file_upload);
        void update(FileUpload file_upload);
        void delete(FileUpload file_upload);
        List<FileUpload> getAll();
        FileUpload getById(long file_upload_id);
        FileUpload getByName(string name);
        IQueryable<FileUpload> getQueryable();
    }
}
