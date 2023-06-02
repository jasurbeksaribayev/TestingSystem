using AspnetApp.Data.IGenericRepositories;
using AspnetApp.Domain.Entities;
using AspnetApp.Service.DTOs.Quizes;
using AspnetApp.Service.DTOs.QuizResults;
using AspnetApp.Service.DTOs.SolvedQuestions;
using AspnetApp.Service.Exceptions;
using AspnetApp.Service.Helpers;
using AspnetApp.Service.IServices.QuizResults;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Linq.Expressions;

namespace AspnetApp.Service.Services.QuizResults
{
    public class QuizResultService : IQuizResultService
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<Quiz> quizRepository;
        private readonly IGenericRepository<QuizRezult> quizResultRepository;
        private readonly IMapper mapper;

        public QuizResultService(IGenericRepository<User> userRepository , IGenericRepository<Quiz> quizRepository, IGenericRepository<QuizRezult> quizResultRepository,IMapper mapper)
        {
            this.userRepository = userRepository;
            this.quizRepository = quizRepository;
            this.quizResultRepository = quizResultRepository;
            this.mapper = mapper;
        }
        public async ValueTask<QuizResultForViewDTO> StartQuiz(QuizResultForCreationDTO quizResultForCreationDTO)
        {
            var existQuiz = await quizRepository.GetAsync(q => q.Id.Equals(quizResultForCreationDTO.QuizId));

            if (existQuiz is null)
                throw new AspnetAppException(404, "quiz not found");

            var existUser = await userRepository.GetAsync(u => u.Id.Equals(quizResultForCreationDTO.UserId));

            if (existUser is null)
                throw new AspnetAppException(404, "user not found");

            var quizResult = await quizResultRepository.CreateAsync(mapper.Map<QuizRezult>(quizResultForCreationDTO));

            await quizResultRepository.SaveChangesasync();

            return mapper.Map<QuizResultForViewDTO>(quizResult);
        }
        public async ValueTask<QuizResultForViewDTO> CreateAsync(int quizResultId)
        {
            var inc = await quizResultRepository.GetAll().Include(q => q.SolvedQuestions).FirstOrDefaultAsync(q=>q.Id.Equals(quizResultId));

            int countAnswers = 0; 
            
             foreach (var answer in inc.SolvedQuestions)
            {
                if (answer.CorrectAnswer)
                    countAnswers++;
            }

            inc.CorrectAnswers = countAnswers;

            await quizResultRepository.SaveChangesasync();

            return mapper.Map<QuizResultForViewDTO>(inc);
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var isDeleted = await quizResultRepository.DeleteAsync(id);

            if (!isDeleted)
                throw new AspnetAppException(404, "quizResult not found");

            await quizResultRepository.SaveChangesasync();

            return true;
        }

        public async ValueTask<IEnumerable<QuizResultForViewDTO>> GetAllAsync(Expression<Func<QuizRezult, bool>> expression = null)
        {
            var quizResults = await quizResultRepository.GetAll()
            .Include(q => q.SolvedQuestions)
            .ToListAsync();
 
            return mapper.Map<IEnumerable<QuizResultForViewDTO>>(quizResults);
        }

        public async ValueTask<QuizResultForViewDTO> GetAsync(Expression<Func<QuizRezult, bool>> expression)
        {
            var quizResult = await quizResultRepository.GetAll()
            .Include(q => q.SolvedQuestions)
            .FirstOrDefaultAsync(expression);
            
            if (quizResult == null)
                throw new AspnetAppException(404, "not found");

            return mapper.Map<QuizResultForViewDTO>(quizResult);
        }

        public async ValueTask<string> GetAllInExcel(int quizId)
        {
            //var quizResults = mapper.Map<List<QuizResultForViewDTO>>(await quizResultRepository.
            //    GetAll(qr => qr.QuizId == quizId).ToListAsync());

            var quizResults = mapper.Map<List<QuizResultForViewDTO>>(await quizResultRepository.GetAll(qr => qr.QuizId == quizId)
            .Include(q => q.User)
            .Include(q => q.Quiz).ToListAsync());

            var quizResultsForExcel = new List<QuizResultForViewInExcelDTO>();


            foreach (var i in quizResults)
            {
                quizResultsForExcel.Add(new QuizResultForViewInExcelDTO()
                {
                    FirstName = i.User.FirstName,
                    LastName = i.User.LastName,
                    CorrectAnswers = i.CorrectAnswers
                });
            }


            string fileName = $"{Guid.NewGuid():N}_{quizResults.FirstOrDefault().Quiz.Title}.xlsx";
            string rootPath = EnvironmentHelper.ExcelRootPath;

            string filePath = Path.Combine(EnvironmentHelper.ExcelRootPath, fileName);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add(quizResults.FirstOrDefault().Quiz.Title);

                for (int i = 0; i < typeof(QuizResultForViewInExcelDTO).GetProperties().Length; i++)
                {
                    workSheet.Cells[1, i + 1].Value = typeof(QuizResultForViewInExcelDTO).GetProperties()[i].Name;
                }

                for (int i = 0; i < quizResultsForExcel.Count(); i++)
                {
                    for (int j = 0; j < typeof(QuizResultForViewInExcelDTO).GetProperties().Length; j++)
                    {
                        workSheet.Cells[i + 2, j + 1].Value = typeof(QuizResultForViewInExcelDTO).GetProperties()[j].GetValue(quizResultsForExcel[i]);
                    }
                }

                if (!Directory.Exists(EnvironmentHelper.ExcelRootPath))
                    Directory.CreateDirectory(EnvironmentHelper.ExcelRootPath);

                File.WriteAllBytes(filePath, package.GetAsByteArray());
            }
            return Path.Combine(EnvironmentHelper.ExcelPath, fileName);
        }
        }
    }
