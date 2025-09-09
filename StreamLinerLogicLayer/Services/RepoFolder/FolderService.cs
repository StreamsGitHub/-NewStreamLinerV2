using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.RepoFolder
{
    public class FolderService : IFolderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Folder> FolderRepository;
        private readonly IGenericRepository<ApplicationUser> UserRepository;
        private readonly IGenericRepository<FolderUserPermission> _folderPermission;


        public FolderService(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            FolderRepository = _unitOfWork.Folder;
            _mapper = mapper;
            _folderPermission = _unitOfWork.FolderUserPermission;
        }
        public async Task<CreationResponse> CreateFolder(folderDTO folderdto)
        {
            CreationResponse creationResponse = new CreationResponse();
            var sameFolder = FolderRepository.GetAll().FirstOrDefault(f => f.FolderName == folderdto.Name && f.ParentId == folderdto.ParentId);
            if (sameFolder != null)
            {
                creationResponse.Message = "Folder with the same name already exists in this directory";
                return creationResponse;
            }

            string baseDirectory;
            int level;

            var parentFolder = FolderRepository.GetAll().FirstOrDefault(f => f.FolderId == folderdto.ParentId);
            if (parentFolder == null)
            {
                creationResponse.Message = "Parent folder not found";
                // Parent folder not found
                return creationResponse;
            }

            baseDirectory = Path.Combine(parentFolder.FolderPath, folderdto.Name);
            level = (int)parentFolder.Level + 1;

            Folder folder = new Folder
            {
                FolderName = folderdto.Name,
                FolderPath = baseDirectory,
                ParentId = folderdto.ParentId  ,
                Level = level,
                Deleted = false,
                OwnerId = folderdto.OwnerId,
            };
            var modelcreat = FolderRepository.AddAsync(folder);
            _unitOfWork.Save();

            if (modelcreat == null)
            {
                creationResponse.Message = "Failed to create folder";
                return creationResponse;
            }
            else
            {
                creationResponse.IsSuccess = true;
                creationResponse.Message = "Folder created successfully";
            }

            return creationResponse;

        }

        public Task<CreationResponse> DeleteFolder(int folderId)
        {
            var creationResponse = new CreationResponse();
            var folder = FolderRepository.GetAll().FirstOrDefault(f => f.FolderId == folderId);
            if (folder == null)
            {
                creationResponse.Message = "Folder not found";
                return Task.FromResult(creationResponse);
            }
            FolderRepository.Delete(folder);
            _unitOfWork.Save();
            creationResponse.IsSuccess = true;
            creationResponse.Message = "Folder deleted successfully";
            return Task.FromResult(creationResponse);
        }

        public Task<List<getfolderDTO>> GetAllFolders()
        {
            throw new NotImplementedException();
        }

        public Task<getfolderDTO> GetFolderById(int id)
        {

            var folder = FolderRepository.GetAll().FirstOrDefault(f => f.FolderId == id);
            if (folder == null)
            {
                return Task.FromResult<getfolderDTO>(null);
            }
            var folderDto = _mapper.Map<getfolderDTO>(folder);
            return Task.FromResult(folderDto);
        }


        public async Task<List<Folder>> GetSubFoldersAsync(int parentId)
        {
            var Folder = await FolderRepository.GetAll().Where(a => a.ParentId == parentId && a.FolderId !=parentId).ToListAsync();
            return Folder.ToList();
        }


        public Task<CreationResponse> RenameFolder(folderDTO folderdto)
        {
            // This method is not implemented yet.
            if (folderdto == null)
                throw new ArgumentNullException(nameof(folderdto));
            // get folder by id
            var folder = FolderRepository.GetAll().FirstOrDefault(f => f.FolderId == folderdto.FolderId);

            // Check if folder exists
            if (folder == null)
            {
                return Task.FromResult(new CreationResponse { IsSuccess = false, Message = "Folder not found" });
            }

            folder.FolderPath.Replace(folder.FolderName, folderdto.Name); // Update folder path with new name
            // rename folder
            folder.FolderName = folderdto.Name;
            folder.FolderPath = folderdto.FolderPath;
            // Update the folder in the repository
            FolderRepository.Update(folder);
            _unitOfWork.Save();

            return Task.FromResult(new CreationResponse { IsSuccess = true, Message = "Folder renamed successfully" });

        }

        public async Task<List<Folder>> GetMyRepositoryRootFoldersAsync()
        {
            var folderRepository = await GetMyRepositorieFolder();
            var folders = await FolderRepository.GetAll().Where(a => a.ParentId == folderRepository.Id && a.Level==1 ).ToListAsync();
            return folders;
        }

        public async Task<getfolderDTO> GetMyRepositorieFolder()
        {
          Folder folder = _unitOfWork.Folder.GetFindAsync(f=>f.Level==0).Result;
            if (folder == null)
            {
                return await Task.FromResult<getfolderDTO>(null);
            }
            var folderDto = _mapper.Map<getfolderDTO>(folder);
            folderDto.Id = folder.FolderId;
            return await Task.FromResult(folderDto);
        }

        public async Task<CreationResponse> CreateRepositorieFolder()
        {
            var admin = UserRepository.GetAll().FirstOrDefault();
            CreationResponse creationResponse = new CreationResponse();
            Folder folder = new Folder
            {
                FolderName = "Repositories File",
                FolderPath = "\\RepositoriesFile",
                ParentId = 0,
                Level = 0,
                Deleted = false,
                Active = true,
                CompanyId = 1,
                OwnerId = admin.Id,
                status=FolderStatus.shared,
            };
            var modelcreat = FolderRepository.AddAsync(folder);
            _unitOfWork.Save();
            if (modelcreat == null)
            {
                creationResponse.Message = "Failed to create folder";
                return await Task.FromResult(creationResponse);
            }
            else
            {
                creationResponse.IsSuccess = true;
                creationResponse.Message = "Folder created successfully";
            }
            return await Task.FromResult(creationResponse);
        }

        public async Task<CreationResponse> AddFolderPermission(AddFolderPermissionDto folderpermission)
        {
            CreationResponse creationResponse = new CreationResponse();
            FolderUserPermission folderUserPermission = new FolderUserPermission()
            {
                FolderId = folderpermission.FolderId,
                UserId = folderpermission.UserId,
                PermissionTypeId = folderpermission.FolderPermissionId,
                Active = true,
                Deleted = false,
                CompanyId = folderpermission.CompanyId,
                CreateId = folderpermission.UserId,
            };
            var modelcreat = _folderPermission.AddAsync(folderUserPermission);
            _unitOfWork.Save();
            if (modelcreat == null)
            {
                creationResponse.Message = "Failed to create folder";
                return await Task.FromResult(creationResponse);
            }
            else
            {
                creationResponse.IsSuccess = true;
                creationResponse.Message = "Folder created successfully";
            }
            return await Task.FromResult(creationResponse);

        }

        public Task<CreationResponse> DeleteFolderPermission(int permissionId)
        {
            var permission = _folderPermission.GetAll().FirstOrDefault(fp => fp.Id == permissionId);
            if (permission == null)
            {
                return Task.FromResult(new CreationResponse { IsSuccess = false, Message = "Permission not found" });
            }
            _folderPermission.Delete(permission);
            _unitOfWork.Save();
            return Task.FromResult(new CreationResponse { IsSuccess = true, Message = "Permission deleted successfully" });

        }

        public Task<List<FolderUserPermissionViewModel>> GetFolderPermissions(int folderId)
        {
            List<FolderUserPermissionViewModel> folderUserPermissions = new List<FolderUserPermissionViewModel>();
            folderUserPermissions = _folderPermission.GetAll()
                .Where(fup => fup.FolderId == folderId)
                .Select(fup => new FolderUserPermissionViewModel
                {
                    Id = fup.Id,
                    FolderId = fup.FolderId,
                    UserId = fup.UserId,
                    FolderPermissionId = fup.PermissionTypeId,
                    Username = fup.User.FirstName +" " + fup.User.LastName,
                    Permission = fup.PermissionType.PermissionName,
                    FolderName = fup.Folder.FolderName
                })
                .ToList();
            return Task.FromResult(folderUserPermissions);
        }
    }
}
