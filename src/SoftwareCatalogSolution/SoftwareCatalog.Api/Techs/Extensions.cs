using FluentValidation;
using SoftwareCatalog.Api.Shared.Catalog;
using SoftwareCatalog.Api.Techs.Endpoints;


namespace SoftwareCatalog.Api.Techs;

public static class Extensions
{

    public static IServiceCollection AddTechs(this IServiceCollection services)
    {
        services.AddScoped<TechSlugGenerator>();
        services.AddScoped<ICheckForUniqueTechSlugs, TechDataService>();

        services.AddScoped<IValidator<TechCreateModel>, UpdatedTechCreateModelValidator>();
        services.AddScoped<ICheckForTechExistenceForCatalog, TechDataService>();

        services.AddAuthorizationBuilder()
            .AddPolicy("canAddTechs", p =>
            {
                p.RequireRole("manager");
                p.RequireRole("software-center");
            });

        return services;
    }

    public static IEndpointRouteBuilder MapTechs(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("techs").WithTags("Approved Techs").WithDescription("The Approved Techs for the Company");

        //group.MapPost("/", AddingATech.CanAddTechAsync).RequireAuthorization("canAddtechs");
        group.MapPost("/", AddingATech.CanAddTechAsync);
        //group.MapGet("/{id}", GettingATech.GetTechAsync).WithTags("Approved Techs", "Catalog");
        //group.MapGet("/", GettingATech.GetTechsAsync);
        return group;

    }
}
