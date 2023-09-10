using FileStorage.Application.Services.Contracts;
using FileStorage.Domain.Events;
using FileStorage.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application.Handlers
{
    public sealed class FileStatusNotificationHandler : INotificationHandler<CreatedFileEvent>, INotificationHandler<FinishedFileEvent>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUserConnectedService _userConnectedService;

        public FileStatusNotificationHandler(IFileRepository fileRepository, IUserConnectedService userConnectedService)
        {
            _fileRepository = fileRepository;
            _userConnectedService = userConnectedService;
        }

        public async Task Handle(CreatedFileEvent notification, CancellationToken cancellationToken)
        {
            var user = await GetUserConnectedByFile(notification.FileId);
            if (user == null)
                return;

            await user.StartedUploadAsync(notification.FileId);
        }

        public async Task Handle(FinishedFileEvent notification, CancellationToken cancellationToken)
        {
            var user = await GetUserConnectedByFile(notification.FileId);
            if (user == null)
                return;

            if (notification.FileStatus == Domain.Enums.FileStatus.Success) 
            {
                await user.SuccessUploadAsync(notification.FileId);
            } else if (notification.FileStatus == Domain.Enums.FileStatus.Failure)
            {
                await user.FailureUploadAsync(notification.FileId);
            }
        }

        private async Task<IUserConnected?> GetUserConnectedByFile(Guid fileId)
        {
            var file = await _fileRepository.GetByIdAsync(fileId);
            if (file!.UserId == null)
                return null;

            return _userConnectedService.GetUserConnectedById(file.UserId);
        }
    }
}
