using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerRepositoryLayer.IRepositories
{
    public interface IUnitOfWork
    {

        IGenericRepository<Folder> Folder { get; set; }
        IGenericRepository<PermissionType> PermissionType { get; set; }
        IGenericRepository<FolderUserPermission> FolderUserPermission { get; set; }
        IGenericRepository<Field> Field { get; set; }
        IGenericRepository<States> States { get; set; }
        IGenericRepository<Projects> Projects { get; set; }
        IGenericRepository<MetaDataTemplate> MetaDataTemplate { get; set; }
        IGenericRepository<MetaDataTemplateField> MetaDataTemplateField { get; set; }
        IGenericRepository<Document> Document { get; set; }
        IGenericRepository<Organization> Organization { get; set; }
        IGenericRepository<RepositoriesDisk> RepositoriesDisk { get; }

        Task SaveAsync();
        void Save();
    }
}
