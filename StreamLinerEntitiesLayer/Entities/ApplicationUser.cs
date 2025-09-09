using Microsoft.AspNetCore.Identity;
using StreamLinerEntitiesLayer.Entities.IEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace StreamLinerEntitiesLayer.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int PartnerId { get; set; }
        public int CompanyId { get; set; }
        public string? Company { get; set; }
        public int DepartmentId { get; set; }
        public string? JobTitle { get; set; }  
        public string? Address { get; set; }
        public int CityId { get; set; } = 0;
        public string? Mobile { get; set; }
        public DateTime? BirthDate{ get; set; } = DateTime.Now;
        public DateTime? HireDate { get; set; } = DateTime.Now;
        public decimal? Volumequota { get; set; } = 0;
        public decimal? Maxquota { get; set; } = 0;
        public string? pwd { get; set; } 
        public string? ProfileImage { get; set; } = "Image";

        [Display(Name = "Document Managenet System")]
        public bool DMS { get; set; }

        [Display(Name = "Content Management System")]
        public bool CMS { get; set; }
        [Display(Name = "Business Process Management")]
        public bool BPM { get; set; }
        [Display(Name = "Operations Management")]
        public bool Oper { get; set; }
        [Display(Name = "Customer Relationship Management")]
        public bool CRM { get; set; }
        [Display(Name = "Human Resource Management")]
        public bool HRM { get; set; }

        [Display(Name = "Accounting")]
        public bool ACC { get; set; }
        [Display(Name = "Sales")]
        public bool SlS { get; set; }
        [Display(Name = "Procurement")]
        public bool PRC { get; set; }
        [Display(Name = "Inventory")]
        public bool INV { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; } = 0;

        //[Display(Name = "Legal")]
        //public bool LEG { get; set; }

        //[Display(Name = "Marketing")]
        //public bool MKT { get; set; }

        //[Display(Name = "Finance")]
        //public bool FIN { get; set; }




        #region for Relations between entities with User
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }
        public bool IsDeleted { get ; set ; } = false;
        //public virtual ICollection<TasksUser> TasksUsersFrom { get; set; }
        //public virtual ICollection<TasksUser> TasksUsersTo { get; set; }
        //public virtual ICollection<MeetingUser> MeetingUsers { get; set; }
        //  public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public static implicit operator UserManager<object>(ApplicationUser v)
        //{
        //    throw new NotImplementedException();
        //}


        #endregion
    }
}
