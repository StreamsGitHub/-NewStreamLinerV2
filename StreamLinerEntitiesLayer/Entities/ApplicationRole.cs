using Microsoft.AspNetCore.Identity;

namespace StreamLinerEntitiesLayer.Entities
{

    public class ApplicationRole : IdentityRole<int>

    {
        public string? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
