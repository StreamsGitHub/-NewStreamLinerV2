using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerRepositoryLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Folder> Folder { get; set; }
        public IGenericRepository<RepositoriesDisk> RepositoriesDisk { get; set; }
        public IGenericRepository<PermissionType> PermissionType { get; set; }
        public IGenericRepository<FolderUserPermission> FolderUserPermission { get; set; }
      
        public IGenericRepository<Field> Field { get; set; }
        public IGenericRepository<States> States { get; set; }
        public IGenericRepository<Projects> Projects { get; set; }
        public IGenericRepository<MetaDataTemplate> MetaDataTemplate { get; set; }
        public IGenericRepository<MetaDataTemplateField> MetaDataTemplateField { get; set; }
        public IGenericRepository<Document> Document { get; set; }
        public IGenericRepository<Organization> Organization { get; set; }


         



        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ;
            Folder = new GenericRepository<Folder>(_context);
            Field = new GenericRepository<Field>(_context);
            States = new GenericRepository<States>(_context);
            Projects = new GenericRepository<Projects>(_context);
            MetaDataTemplate = new GenericRepository<MetaDataTemplate>(_context);
            MetaDataTemplateField = new GenericRepository<MetaDataTemplateField>(_context);
            Document = new GenericRepository<Document>(_context);
            Organization = new GenericRepository<Organization>(_context);
            FolderUserPermission = new GenericRepository<FolderUserPermission>(_context);
            PermissionType = new GenericRepository<PermissionType>(_context);
            RepositoriesDisk = new GenericRepository<RepositoriesDisk>(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
