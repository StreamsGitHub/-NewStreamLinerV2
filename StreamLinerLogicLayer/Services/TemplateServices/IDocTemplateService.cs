using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.TemplateServices
{
    public interface IDocTemplateService
    {
        Task<DocTemplate> CreateTemplate(DocTemplateDto docTemplate);
        Task<CreationResponse> DeleteTemplate(int id);
        Task<DocTemplateDto> GetTemplateById(int  id);

        Task<List<DocTemplateDto>> GetAllTemplate();
        Task<CreationResponse> UpdateTemplate(DocTemplateDto docTemplateDto);
   

    }
}
