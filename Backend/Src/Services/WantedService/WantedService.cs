namespace Backend.Src.Services.WantedService;

using Backend.Src.DTOs;
using Backend.Src.DTOs.Wanted;
using Backend.Src.Repositories.WantedRepo;
using Backend.Src.Models;
using Backend.Src.Services;
using Backend.Src.Services.Implementation;
using Backend.src.Converter.Wanted;

public class WantedService : BaseService<Wanted, WantedReadDTO, WantedCreateDTO, WantedUpdateDTO>, IWantedService
{
    private readonly IUserService _userService;
    
    public WantedService(IUserService userService, IWantedRepo repo, IWantedConverter converter) : base(repo, converter)
    {
        _userService = userService;
    }

    public async Task<ICollection<UserDTO>> MatchUsersToWanted(MatchDTO match)
    {
        return await _userService.GetAllUsersAsync(match);
    }
}