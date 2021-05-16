using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Helper;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace CMS.Core.Service.Implementation
{
    public class FileUploadServiceImpl : FileUploadService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private FileUploadRepository _fileUploadRepo;
        private TransactionManager _transactionManager;
        private readonly FileUploadMaker _fileUploadMaker;


        public FileUploadServiceImpl(FileUploadRepository fileUploadRepo, TransactionManager transactionManager, FileUploadMaker fileUploadMaker, IHostingEnvironment hostingEnvironment)
        {
            _fileUploadRepo = fileUploadRepo;
            _transactionManager = transactionManager;
            _fileUploadMaker = fileUploadMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long file_upload_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var fileUpload = _fileUploadRepo.getById(file_upload_id);
                if (fileUpload == null)
                {
                    throw new ItemNotFoundException($"File with id {file_upload_id} doesn't exist.");
                }
                string oldImage = fileUpload.file_name;

                _fileUploadRepo.delete(fileUpload);
                deleteImage(oldImage);
                _transactionManager.commitTransaction();

            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void save(FileUploadDto fileUploadDto)
        {
            try
            {
                _transactionManager.beginTransaction();
            
                FileUpload fileUpload = new FileUpload();
                _fileUploadMaker.copy(ref fileUpload, fileUploadDto);
                _fileUploadRepo.insert(fileUpload);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }


        public void update(FileUploadDto fileUploadDto)
        {
            try
            {
                _transactionManager.beginTransaction();

                var file = _fileUploadRepo.getById(fileUploadDto.file_upload_id);
                if (file == null)
                {
                    throw new ItemNotFoundException($"File with id {fileUploadDto.file_upload_id} doesnot exist.");
                }

                string oldFile = file.file_name;

                _fileUploadMaker.copy(ref file, fileUploadDto);
                _fileUploadRepo.update(file);

                if (!string.IsNullOrWhiteSpace(fileUploadDto.file_name))
                {
                    if (!string.IsNullOrWhiteSpace(oldFile))
                    {
                        deleteImage(oldFile);
                    }
                }

                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void enable(long file_upload_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var files = _fileUploadRepo.getById(file_upload_id);
                if (files == null)
                    throw new ItemNotFoundException($"File with id {file_upload_id} doesnot exist.");

                files.enable();
                _fileUploadRepo.update(files);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void disable(long file_upload_id)
        {
            try
            {
                _transactionManager.beginTransaction();
                var files = _fileUploadRepo.getById(file_upload_id);
                if (files == null)
                    throw new ItemNotFoundException($"File with id {file_upload_id} doesnot exist.");

                files.disable();
                _fileUploadRepo.update(files);
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        protected void deleteImage(string image_path)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "files");
            if (File.Exists(Path.Combine(filePath, image_path)))
            {
                File.Delete(Path.Combine(filePath, image_path));
            }
        }
    }
}
