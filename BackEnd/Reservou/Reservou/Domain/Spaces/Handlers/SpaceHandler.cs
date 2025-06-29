using Reservou.Domain.Spaces.Commands;
using Reservou.Domain.Spaces.Infrastructure;
using System.Net;

namespace Reservou.Domain.Spaces.Handlers;

public class SpaceHandler
{
    private readonly SpacesRepositories _spaceRepositories;

    public SpaceHandler(SpacesRepositories spaceRepositories)
    {
        _spaceRepositories = spaceRepositories;
    }

    public async Task<bool> CreateNewSpaceAsync(NewSpaceCommand newSpace)
    {
        try
        {
            var result = await _spaceRepositories.SaveNewSpace(newSpace);

            if (result && newSpace.SpaceImages.Count() > 0)
            {
                List<string> filesPath = await CreateFolderAndSaveFiles(newSpace.SpaceImages);

                await _spaceRepositories.SaveSpaceImages(filesPath);
            }
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    private async Task<List<string>> CreateFolderAndSaveFiles(string[] spaceImages)
    {
        List<string> filePaths = new List<string>();

        return filePaths;
    }
}
