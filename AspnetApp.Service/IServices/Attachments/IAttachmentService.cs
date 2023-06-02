using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Attachments;

namespace AspnetApp.Service.IServices.Attachments
{
    public interface IAttachmentService
    {
        ValueTask<AttachmentForViewDTO> CreateAsync(int questionId,string filePath, string fileName);
        ValueTask<AttachmentForViewDTO> UpdateAsync(int id, Stream stream);
        ValueTask<AttachmentForViewDTO> UploadAsync(int questionId, AttachmentForCreationDTO attachmentForCreationDTO);
        ValueTask<bool> DeleteAsync(int id);
    }
}
