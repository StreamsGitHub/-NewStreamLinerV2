using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRContract : MasterModel
    {
        [Key]
        public int ContractId { get; set; }

        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }


        [Display(Name = "Contract Type")]
        [ForeignKey("HRContractType")]
        public int ContractTypeId { get; set; }
        public HRContractType? HRContractType { get; set; }


        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public decimal Salary { get; set; } = 0;

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "Job Descraption")]
        public int JobId { get; set; }

    }
}
