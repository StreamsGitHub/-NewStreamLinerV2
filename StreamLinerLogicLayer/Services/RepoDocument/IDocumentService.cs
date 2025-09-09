using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.RepoDocument
{
    public interface IDocumentService
    {
        Task<List<getDocumentsDTO>> GetAllSunDocuments(int Id);


        //////Task<getfolderDTO> UploadDocument();
        //////Task<CreationResponse> CreateRepositorieFolder();
        //////Task<CreationResponse> CreateFolder(folderDTO folderdto);
        //////Task<CreationResponse> DeleteFolder(int folderId);
        //////Task<getfolderDTO> GetFolderById(int id);

        //////Task<List<getfolderDTO>> GetAllFolders();
        //////Task<CreationResponse> RenameFolder(folderDTO folderdto);
        //////Task<List<Folder>> GetSubFoldersAsync(int parentId);
        //////Task<List<Folder>> GetMyRepositoryRootFoldersAsync();

        //////Task<CreationResponse> AddFolderPermission(AddFolderPermissionDto folderpermission);
        //////Task<CreationResponse> DeleteFolderPermission(int permissionId);

        //////Task<List<FolderUserPermissionViewModel>> GetFolderPermissions(int folderId);

        //////Task UploadDocument();
        //////Task DownloadDocument();
        //////Task DeleteDocument();
        //////Task MoveDocument();
        //////Task RenameDocument();
        //////Task SignDocument();
        //////Task GetDocumentById();
        //////Task GetDocumentsByFolderId();
        //////Task SearchDocuments();
        //////Task GetRecentDocuments();
        //////Task GetFavoriteDocuments();
        //////Task AddToFavorites();
        //////Task RemoveFromFavorites();
        //////Task GetDocumentsByType();
        //////Task GetDocumentsByDateRange();
        //////Task GetDocumentsByOwner();
        //////Task GetDocumentHistory();
        //////Task RestoreDocumentVersion();
        //////Task GetSharedDocuments();
        //////Task ShareDocument();
        //////Task UnshareDocument();

    }
}
