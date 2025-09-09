using StreamLinerEntitiesLayer.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamLinerEntitiesLayer.HREntities
{
    public class HRRequest : MasterModel
    {
        [Key]
        public int RequestId { get; set; }

        // Form Number
        [Display(Name = "Request Number")]
        public int FormId { get; set; }

        // Type
        [Display(Name = "Request Type")]
        [ForeignKey("HRRequestType")]
        public int HRRequestTypeId { get; set; }
        public HRRequestType? HRRequestType { get; set; }

        // Users
        [Display(Name = "Empolyee")]
        [ForeignKey("Partner")]
        public int PartnerId { get; set; }
        public Partner? Partner { get; set; }

        // State
        [Display(Name = "Request State")]
       
        public int HRRequestStateId { get; set; }
       
       

        //public int To { get; set; }
        //public int See { get; set; }
        //public int Approve { get; set; }
        //public int Reject { get; set; }


    }
}
