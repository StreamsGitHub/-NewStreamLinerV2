using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerLogicLayer.Services.RepoFolder
{
    public interface IFolderService
    {

        Task<getfolderDTO> GetMyRepositorieFolder();
        Task<CreationResponse> CreateRepositorieFolder();
        Task<CreationResponse> CreateFolder(folderDTO folderdto);
        Task<CreationResponse> DeleteFolder(int folderId);
        Task<getfolderDTO> GetFolderById(int id);

        Task<List<getfolderDTO>> GetAllFolders();
        Task<CreationResponse> RenameFolder(folderDTO folderdto);
        Task<List<Folder>> GetSubFoldersAsync(int parentId);
        Task<List<Folder>> GetMyRepositoryRootFoldersAsync();

        Task<CreationResponse> AddFolderPermission(AddFolderPermissionDto folderpermission);
        Task<CreationResponse> DeleteFolderPermission(int permissionId);

        Task<List<FolderUserPermissionViewModel>> GetFolderPermissions(int folderId);

    }
}