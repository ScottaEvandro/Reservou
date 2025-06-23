using Reservou.Domain.Users.Commands;
using Reservou.Domain.Users.Dtos;
using Reservou.Domain.Users.Infrastructure.Queries;
using Reservou.Domain.Users.Infrastructure.Repositories;
using System.Net;

namespace Reservou.Domain.Users.Handlers;

public class UserHandler
{
    private readonly UserQueries _userQueries;
    private readonly UserRepositories _userRepositories;

    public UserHandler(UserQueries userQueries, UserRepositories userRepositories)
    {
        _userQueries = userQueries;
        _userRepositories = userRepositories;
    }

    public async Task<UserDto> GetUserLogin(string username, string password)
    {
        var result = await _userQueries.GetUserAsync(username, password);

        return result;
    }

    public async Task<HttpStatusCode> RegisterNewUser(UserRegisterCommand newUser)
    {
        var result = await _userRepositories.CreateNewUser(newUser);

        return result;
    }
}
