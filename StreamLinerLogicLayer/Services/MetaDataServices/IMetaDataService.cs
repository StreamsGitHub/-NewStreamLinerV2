using Microsoft.AspNetCore.Mvc;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.MetaDataServices
{
    public interface IMetaDataService
    {
        Task<IEnumerable<MetaDataDTO>> GetAllTemplatesAsync();
        Task<MetaDataTemplate> Create(MetaDataDTO templateDTO);
        Task<IActionResult> Delete(int id);
        Task<MetaDataDTO> GetTemplateByIdAsync(int id);

        Task<bool> UpdateTemplateAsync(int id, MetaDataDTO templateDTO);
    }
}
