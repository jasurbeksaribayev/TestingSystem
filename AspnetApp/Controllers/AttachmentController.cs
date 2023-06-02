using AspnetApp.Extensions;
using AspnetApp.Service.Attributes;
using AspnetApp.Service.IServices.Attachments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            this.attachmentService = attachmentService;
        }

        [HttpPost ("{questionId}")]
        public async ValueTask<IActionResult> UploadAsync([FromRoute] int questionId,[FormFileAttributes] IFormFile formFile)
            => Ok(await attachmentService.UploadAsync(questionId, formFile.ToAttachmentOrDefault()));

        [HttpPut ("{id}")]
        public async ValueTask<IActionResult> UpdateAsync([FromRoute] int id,[FormFileAttributes] IFormFile formFile)
            => Ok(await attachmentService.UpdateAsync(id,formFile.ToAttachmentOrDefault().Stream));

        [HttpDelete ("{id}")]
        public async ValueTask<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await attachmentService.DeleteAsync(id));
    }
}
