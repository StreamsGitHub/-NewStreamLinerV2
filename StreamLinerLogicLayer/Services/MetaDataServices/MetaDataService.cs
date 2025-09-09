using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.MetaDataServices
{
    public class MetaDataService : IMetaDataService
    {
        private readonly IGenericRepository<MetaDataTemplate> TemplateRepository;
        private readonly IGenericRepository<MetaDataTemplateField> MetaDataTemplateField;
        private readonly IUnitOfWork uintofwork;

        public MetaDataService(IUnitOfWork _uintofwork, IHttpContextAccessor _httpContextAccessor)
        {
            uintofwork = _uintofwork;
            TemplateRepository = uintofwork.MetaDataTemplate;
            MetaDataTemplateField = uintofwork.MetaDataTemplateField;
        }

        public Task<IEnumerable<MetaDataDTO>> GetAllTemplatesAsync()
        {
            var templates = TemplateRepository.GetAll().Select(t => new MetaDataDTO
            {
                Name = t.Name,
                Description = t.Description,
                Id = t.MetaDataTemplateId,
            });
            return Task.FromResult(templates.AsEnumerable());
        }

        public async Task<MetaDataTemplate> Create(MetaDataDTO templateDTO)
        {
            var template = new MetaDataTemplate
            {
                Name = templateDTO.Name,
                Description = templateDTO.Description,
                LastModifiedTime = DateTime.UtcNow
            };
            TemplateRepository.AddAsync(template);
            uintofwork.Save();
            return template;
        }

        public Task<IActionResult> Delete(int id)
        {
            var template = TemplateRepository.GetByIdAsync(id).Result;
            if (template == null)
            {
                return Task.FromResult<IActionResult>(new NotFoundResult());
            }
            TemplateRepository.Delete(template);
            uintofwork.Save();
            return Task.FromResult<IActionResult>(new OkResult());
        }

        public Task<MetaDataDTO> GetTemplateByIdAsync(int id)
        {
            var template = TemplateRepository.GetByIdAsync(id).Result;
            if (template == null)
            {
                throw new Exception("Template not found");
            }
            MetaDataDTO metaDataDTO= new MetaDataDTO();
            metaDataDTO.Name = template.Name;
            metaDataDTO.Description = template.Description;
            metaDataDTO.Id = template.MetaDataTemplateId;

            return Task.FromResult(metaDataDTO);


        }

        public Task<bool> UpdateTemplateAsync(int id, MetaDataDTO templateDTO)
        {
            var template = TemplateRepository.GetByIdAsync(id).Result;
            if (template == null)
            {
                throw new Exception("Template not found");
            }
            template.Name = templateDTO.Name;
            template.Description = templateDTO.Description;
            template.LastModifiedTime = DateTime.UtcNow;
            TemplateRepository.Update(template);
            uintofwork.Save();
            return Task.FromResult(true);

        }
    }
}
