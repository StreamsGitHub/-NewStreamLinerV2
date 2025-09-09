using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerViewModelLayer.ModelDTO
{
    public class CreatedocumentDto
    {
        public string title { get; set; }
        public string? ParentId { get; set; }
        public IFormFile document { get; set; }
    }
}
