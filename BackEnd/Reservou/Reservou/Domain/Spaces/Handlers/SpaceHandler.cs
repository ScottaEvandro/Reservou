using Microsoft.AspNetCore.Http;
using Reservou.Domain.Spaces.Commands;
using Reservou.Domain.Spaces.Infrastructure;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Reservou.Domain.Spaces.Handlers;

public class SpaceHandler
{
    private readonly SpacesQueries _spacesQueries;
    private readonly SpacesRepositories _spaceRepositories;

    public SpaceHandler(SpacesQueries spacesQueries, SpacesRepositories spaceRepositories)
    {
        _spacesQueries = spacesQueries;
        _spaceRepositories = spaceRepositories;
    }

    public async Task<bool> CreateNewSpaceAsync(NewSpaceCommand newSpace, List<IFormFile> SpaceImages)
    {
        try
        {
            var result = await _spaceRepositories.SaveNewSpace(newSpace);

            if (result && SpaceImages.Count > 0)
            {
                int spaceId = await _spacesQueries.GetLastSpaceIdInserted();
                List<string> filesPath = await CreateFolderAndSaveFiles(SpaceImages, spaceId);

                await _spaceRepositories.SaveSpaceImages(filesPath, spaceId);
            }
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    private async Task<List<string>> CreateFolderAndSaveFiles(List<IFormFile> spaceImages, int spaceId)
    {
        List<string> filePaths = new List<string>();

        string localPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        while (localPath != null && !localPath.EndsWith("BackEnd", StringComparison.OrdinalIgnoreCase))
        {
            localPath = Directory.GetParent(localPath)?.FullName;
        }

        if (localPath == null)
        {
            throw new DirectoryNotFoundException("Não foi possível encontrar a pasta 'BackEnd' no caminho de execução.");
        }

        var uploadsFolder = Path.Combine(Directory.GetParent(localPath)?.FullName, "FrontEnd", "Assets", "Imagens", "Spaces", spaceId.ToString());

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        int maxWidth = 400; // Largura máxima desejada
        int maxHeight = 400; // Altura máxima desejada

        foreach (var imageFile in spaceImages)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePathToSave = Path.Combine(uploadsFolder, uniqueFileName); 

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    using (Image originalImage = Image.FromStream(memoryStream))
                    {
                        int newWidth;
                        int newHeight;

                        double ratioX = (double)maxWidth / originalImage.Width;
                        double ratioY = (double)maxHeight / originalImage.Height;
                        double ratio = Math.Min(ratioX, ratioY);

                        newWidth = (int)(originalImage.Width * ratio);
                        newHeight = (int)(originalImage.Height * ratio);

                        using (Bitmap newImage = new Bitmap(newWidth, newHeight))
                        {
                            using (Graphics graphics = Graphics.FromImage(newImage))
                            {
                                graphics.CompositingQuality = CompositingQuality.HighQuality;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.SmoothingMode = SmoothingMode.HighQuality;

                                graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                            }

                            ImageFormat format = ImageFormat.Jpeg;
                            string extension = Path.GetExtension(imageFile.FileName)?.ToLowerInvariant();
                            switch (extension)
                            {
                                case ".png": format = ImageFormat.Png; break;
                                case ".gif": format = ImageFormat.Gif; break;
                                case ".jpg":
                                case ".jpeg":
                                    format = ImageFormat.Jpeg;
                                    break;
                            }
                            newImage.Save(filePathToSave, format);
                        }
                    }
                }
                filePaths.Add(filePathToSave);
            }
            catch (Exception ex)
            {
                
            }
        }

        return filePaths;
    }
}
