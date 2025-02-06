namespace SoftwareCatalog.Api.Techs.Endpoints;

using FluentValidation;
using global::SoftwareCatalog.Api.Techs;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public static class AddingATech
{

    public static async Task<Results<Created<TechDetailsResponseModel>, BadRequest>> CanAddTechAsync(
        [FromBody] TechCreateModel request,
        [FromServices] IValidator<TechCreateModel> validator,
        [FromServices] IDocumentSession session,
        [FromServices] TechSlugGenerator slugGenerator,
        [FromServices] IHttpContextAccessor _httpContextAccessor
        )
    {

        //var user = _httpContextAccessor.HttpContext.User; // Don't Do This!!@
        var sub = _httpContextAccessor.HttpContext.User.Identity.Name;

        //var sub = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;


        var validations = await validator.ValidateAsync(request);
        if (!validations.IsValid)
        {
            return TypedResults.BadRequest();
        }
        var entity = new TechEntity
        {
            Id = Guid.NewGuid(),
            Slug = await slugGenerator.GenerateSlugFor(request.Name),
            Email = request.Email,
            Phone = request.Phone,
            Sub = sub,
        };
        session.Store(entity);
        await session.SaveChangesAsync();
        var response = entity.MapToModel();
        return TypedResults.Created($"/techs/{entity.Slug}", response);
    }
}