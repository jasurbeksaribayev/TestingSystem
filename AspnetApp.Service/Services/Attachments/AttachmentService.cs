using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Attachments;
using AspnetApp.Service.Exceptions;
using AspnetApp.Service.Helpers;
using AspnetApp.Service.IServices.Attachments;
using AutoMapper;

namespace AspnetApp.Service.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IGenericRepository<Attachment> attachmentRepository;
        private readonly IGenericRepository<Question> questionRepository;
        private readonly IMapper mapper;

        public AttachmentService(IGenericRepository<Attachment> attachmentRepository, IGenericRepository<Question> questionRepository, IMapper mapper)
        {
            this.attachmentRepository = attachmentRepository;
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        public async ValueTask<AttachmentForViewDTO> CreateAsync(int questionId, string filePath, string fileName)
        {
            var file = new Attachment()
            {
                Path = filePath
            };

            var existQuestion = await questionRepository.GetAsync(q => q.Id.Equals(questionId));

            if (existQuestion == null)
                throw new AspnetAppException(404, "Question not found");

            file.QuestionId = questionId;

            await attachmentRepository.CreateAsync(file);
            
            await attachmentRepository.SaveChangesasync();


            return mapper.Map<AttachmentForViewDTO>(file);
        }

        public async ValueTask<AttachmentForViewDTO> UpdateAsync(int id, Stream stream)
        {
            var existAttachment = await attachmentRepository.GetAsync(a => a.Id == id);

            if (existAttachment == null)
                throw new AspnetAppException(404, "Attachment not found");

            string fileNAme = existAttachment.Path;
            string filePath = Path.Combine(EnvironmentHelper.AttachmentImagePath, fileNAme);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }

            existAttachment.LastModifiedTime = DateTime.UtcNow;

            await attachmentRepository.SaveChangesasync();

            return mapper.Map<AttachmentForViewDTO>(existAttachment);
        }

        public async ValueTask<AttachmentForViewDTO> UploadAsync(int questionId, AttachmentForCreationDTO attachmentForCreationDTO)
        {
            string fileName = Guid.NewGuid().ToString("N") + "-" + attachmentForCreationDTO.Name;
            string filePath = Path.Combine(EnvironmentHelper.AttachmentImagePath, fileName);

            if (!Directory.Exists(EnvironmentHelper.AttachmentImagePath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentImagePath);

            FileStream fileStream = File.OpenWrite(filePath);
            await attachmentForCreationDTO.Stream.CopyToAsync(fileStream);

            await fileStream.FlushAsync();
            fileStream.Close();

            return await CreateAsync(questionId, fileName, Path.Combine(EnvironmentHelper.AttachmentImagePath , fileName));
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var deletedAttachment = await attachmentRepository.GetAsync(a => a.Id == id);

            if (deletedAttachment==null)
                throw new AspnetAppException(404, "Attachment not found");

            string filePath = Path.Combine(EnvironmentHelper.AttachmentImagePath, deletedAttachment.Path);

            File.Delete(filePath);

            await attachmentRepository.DeleteAsync(id);

            await attachmentRepository.SaveChangesasync();


            return true;
        }
    }
}
