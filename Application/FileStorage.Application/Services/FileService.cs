using FileStorage.Application.Commands;
using FileStorage.Application.Services.Contracts;
using FileStorage.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileStorageService _fileStorageService;

        public FileService(IFileRepository fileRepository, IFileStorageService fileStorageService)
        {
            (_fileRepository, _fileStorageService) = (fileRepository, fileStorageService);
        }

        public async Task UploadAsync(UploadFileCommand command)
        {
            var file = Domain.Entities.File.Factory.Create(command.FileName, command.UserId);
            await _fileRepository.AddAsync(file);

            try
            {
                await _fileStorageService.UploadFileAsync(command.UserId,
                    command.FileName,
                    command.Stream);

                file.Success();
                await _fileRepository.UpdateAsync(file);
            } catch (Exception ex)
            {
                file.Failure();
                await _fileRepository.UpdateAsync(file);
            }
        }
    }
}
