using FileStorage.Application.Commands;
using FileStorage.Application.Services;
using FileStorage.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Http.Controllers
{
    [Authorize]
    [ApiController]
    [Route("file")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ICurrentUserIdProvider _currentUserIdProvider;

        public FileController(IFileService fileService, ICurrentUserIdProvider currentUserIdProvider)
        {
            (_fileService, _currentUserIdProvider) = (fileService, currentUserIdProvider);
        }

        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        public async Task UploadAsync([FromForm] IFormFile file)
        {
            var command = new UploadFileCommand(_currentUserIdProvider.Get(),
                file.FileName,
                file.OpenReadStream());

            await _fileService.UploadAsync(command);
        }
    }
}
