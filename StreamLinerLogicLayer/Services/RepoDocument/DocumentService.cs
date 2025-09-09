using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.RepoDocument
{
    public class DocumentService : IDocumentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Document> _documentRepository;
        private readonly IGenericRepository<ApplicationUser> UserRepository;


        public DocumentService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _documentRepository = _unitOfWork.Document;
            _mapper = mapper;
        }
        public async Task<List<getDocumentsDTO>> GetAllSunDocuments(int Id)
        {
            var documents = await _documentRepository.GetAll().Where(d=>d.FolderId == Id).ToListAsync();
            var documentsDto = _mapper.Map<List<getDocumentsDTO>>(documents);
            return documentsDto;
        }
    }
}
