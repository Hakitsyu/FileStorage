using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.Application.Commands
{
    public record UploadFileCommand(string UserId, string FileName, Stream Stream);
}
