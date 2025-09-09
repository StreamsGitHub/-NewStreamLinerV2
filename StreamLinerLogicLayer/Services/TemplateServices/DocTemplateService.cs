using Microsoft.EntityFrameworkCore;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.TemplateServices
{
    public class DocTemplateService : IDocTemplateService
    {
        private readonly IGenericRepository<DocTemplate> _docTemplate;
        public DocTemplateService(IGenericRepository<DocTemplate> docTemplate)
        {
            _docTemplate = docTemplate;
        }
        public async Task<DocTemplate> CreateTemplate(DocTemplateDto docTemplateDto)
        {
            DocTemplate docTemplate = new DocTemplate()
            {
                TemplateName = docTemplateDto.TemplateName
            };

            _docTemplate.AddAsync(docTemplate);
            await _docTemplate.SaveChangesAsync();

            return docTemplate;
        }

        public async Task<CreationResponse> DeleteTemplate(int  Id)
        {
            var response = new CreationResponse();
            try
            {
                DocTemplate template =await _docTemplate.GetByIdAsync(Id);
                if (template != null)
                {
                    _docTemplate.Delete(template);
                    await _docTemplate.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Message = "Template deleted successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Template not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error deleting template: {ex.Message}";
            }
            return response;
        }

        public async Task<List<DocTemplateDto>> GetAllTemplate()
        {
            var templates = await _docTemplate.GetAll().ToListAsync();

            var templateDtos = templates.Select(t => new DocTemplateDto
            {
                Id = t.TemplateId,
                TemplateName = t.TemplateName
            }).ToList();


            return templateDtos;
        }

        public Task<DocTemplateDto> GetTemplateById(int  id)
        {
            var template = _docTemplate.GetByIdAsync(id).Result;
            if (template != null)
            {
                var templateDto = new DocTemplateDto
                {
                    Id = template.TemplateId,
                    TemplateName = template.TemplateName
                };
                return Task.FromResult(templateDto);
            }
            return Task.FromResult<DocTemplateDto>(null);

        }

        public async Task<CreationResponse> UpdateTemplate(DocTemplateDto folderdto)
        {
            var response = new CreationResponse();
            try
            {
               
                    var template =  _docTemplate.GetByIdAsync(folderdto.Id).Result;
                    if (template != null)
                    {
                        template.TemplateName = folderdto.TemplateName;
                        _docTemplate.Update(template);
                        _docTemplate.SaveChangesAsync();
                        response.IsSuccess = true;
                        response.Message = "Template updated successfully.";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Template not found.";
                    }
               
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error updating template: {ex.Message}";
            }
            return response;

        }
    }
}
