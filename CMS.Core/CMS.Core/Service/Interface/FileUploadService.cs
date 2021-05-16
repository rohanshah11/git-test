using CMS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Service.Interface
{
    public interface FileUploadService
    {
        void save(FileUploadDto file_upload_dto);
        void update(FileUploadDto file_upload_dto);
        void delete(long file_upload_id);
        void enable(long file_upload_id);
        void disable(long file_upload_id);
    }
}
