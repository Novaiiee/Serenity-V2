using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serenity.Common;
using Serenity.Database;
using Serenity.Database.Entities;

namespace Serenity.Modules.Friends.Handlers;

public record RemoveFriendCommand(ClaimsPrincipal Claims, string Id) : IRequest<Response>;

public class RemoveFriendCommandHandler : IRequestHandler<RemoveFriendCommand, Response>
{
    private readonly DataContext context;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public RemoveFriendCommandHandler(IMapper mapper, DataContext context, UserManager<User> userManager)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.context = context;
    }

    public async Task<Response> Handle(RemoveFriendCommand command, CancellationToken token)
    {
        var user = await userManager.GetUserAsync(command.Claims);
        var foundUser = user.Friends.Where(x => x.Id == command.Id).First();

        if (foundUser is null)
        {
            return new Response(false, new() { new("FriendNotFound", $"Could not find the friend with Id of {command.Id}") });
        }

        user.Friends.Remove(foundUser);
        foundUser.Friends.Remove(user);

        var result = context.SaveChanges();

        if (result >= 0)
        {
            return new(true, null);
        }

        return new(false, new() { new("RemoveFriendError", "Could not add the friend") });
    }
}