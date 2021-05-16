using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class FileUploadMakerImpl : FileUploadMaker
    {
        public void copy(ref FileUpload file_upload, FileUploadDto file_upload_dto)
        {
            file_upload.file_upload_id = file_upload_dto.file_upload_id;
            file_upload.title = file_upload_dto.title.Trim();
            file_upload.description = file_upload_dto.description.Trim();
            if (file_upload_dto.file_name != null)
            {
                file_upload.file_name = file_upload_dto.file_name;
            }
            file_upload.is_enabled = file_upload_dto.is_enabled;
        }
    }
}
