using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serenity.Common;
using Serenity.Modules.Posts.Dto;
using Serenity.Modules.Posts.Handlers;

namespace Serenity.Modules.Posts;

[Authorize]
[Route("posts")]
public class PostsController : ControllerBase
{
    private readonly IMediator mediator;

    public PostsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRecentPosts()
        => Ok(await mediator.Send(new GetRecentPostsQuery()));

    [HttpGet("my")]
    public async Task<IActionResult> GetUserPosts()
        => Ok(await mediator.Send(new GetUserPostsQuery(HttpContext?.User)));

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUserPostsById([FromRoute] string id)
        => Ok(await mediator.Send(new GetUserPostsByIdQuery(id)));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById([FromRoute] string id)
        => Ok(await mediator.Send(new GetPostByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto)
        => ResultHandler.Handle(await mediator.Send(new CreatePostCommand(dto, HttpContext.User)));

    [HttpPut("{id}")]
    public async Task<IActionResult> EditPost([FromBody] EditPostDto dto, [FromRoute] string id)
        => ResultHandler.Handle(await mediator.Send(new EditPostCommand(dto, HttpContext.User, id)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost([FromRoute] string id)
        => ResultHandler.Handle(await mediator.Send(new DeletePostCommand(id, HttpContext.User)));
}