using December.Business.Exceptions;
using December.Business.Services.Interfaces;
using December.Business.Utilities.FileConfiguration;
using Microsoft.AspNetCore.Http;

namespace December.Business.Services.Implementations;

public class FileService : IFileService
{
    public void RemoveFile(string root, string filePath)
    {
        string fileroot = Path.Combine(root, filePath);
        if (File.Exists(fileroot))
        {
            File.Delete(fileroot);
        }
    }


    public async Task<string> UploadFile(IFormFile file, string root, int kb, params string[] folders)
    {
        if (!file.CheckFileSize(kb))
        {
            throw new FileSizeException("The file size is incorrect");
        }
        if (!file.CheckFileType("image"))
        {
            {
                throw new FileTypeException("The file type is incorrect");
            }
        }
        string folderRoot = string.Empty;
        foreach (var folder in folders)
        {
            folderRoot = Path.Combine(folderRoot, folder);
        }
        string filename = await file.UploadFile(root, folderRoot);
        return filename;
    }
}
