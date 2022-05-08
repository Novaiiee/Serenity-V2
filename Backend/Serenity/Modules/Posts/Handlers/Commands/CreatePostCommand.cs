using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Serenity.Common;
using Serenity.Database;
using Serenity.Database.Entities;
using Serenity.Modules.Posts.Dto;

namespace Serenity.Modules.Posts.Handlers;

public record CreatePostCommand(CreatePostDto Dto, ClaimsPrincipal Claims) : IRequest<Response<object>>;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Response<object>>
{
    private readonly DataContext context;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;

    public CreatePostCommandHandler(IMapper mapper, DataContext context, UserManager<User> userManager)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.context = context;
    }

    public async Task<Response<object>> Handle(CreatePostCommand command, CancellationToken token)
    {
        Post post = mapper.Map<Post>(command.Dto);
        User user = await userManager.GetUserAsync(command.Claims);

        if (user is null)
        {
            return new()
            {
                Success = false,
                Data = null,
                Errors = new()
                {
                    new("UserNotFound", "Could not find the user")
                }
            };
        }

        post.UserId = user.Id.ToString();
        post.User = user;

        await context.Posts.AddAsync(post);
        var result = await context.SaveChangesAsync();

        if (result >= 0)
        {
            return new(true, null, null);
        }

        return new(false, new() { new("CreatePostError", "Could not create the Post") }, null);
    }
}