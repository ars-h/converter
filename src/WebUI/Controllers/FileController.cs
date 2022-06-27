using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Files.Commands;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{

    public class FileController : ApiControllerBase
    {
        [HttpPost("UploadFile")]
        public async Task<Result> UploadFile(UploadFileCommand command)
        {
            /*//IFormFile
            //FileStream
            var httpRequest = HttpContext;
            var aa = file;
            return Result.Success();*/
            return await Mediator.Send(command);
        }
    }
}