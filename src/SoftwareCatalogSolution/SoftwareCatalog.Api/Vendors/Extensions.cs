﻿using FluentValidation;
using SoftwareCatalog.Api.Shared.Catalog;
using SoftwareCatalog.Api.Vendors.Endpoints;

namespace SoftwareCatalog.Api.Vendors;

public static class Extensions
{
    public static IServiceCollection AddVendors(this IServiceCollection services)
    {
        services.AddScoped<VendorSlugGenerator>();
        services.AddScoped<ICheckForUniqueVendorSlugs, VendorDataService>();

        services.AddScoped<IValidator<VendorCreateModel>, UpdatedVendorCreateModelValidator>();
        services.AddScoped<ICheckForVendorExistenceForCatalog, VendorDataService>();

        services.AddAuthorizationBuilder()
            .AddPolicy("canAddVendors", p =>
            {
                p.RequireRole("manager");
                p.RequireRole("software-center");
            });

        return services;
    }

    public static IEndpointRouteBuilder MapVendors(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("vendors").WithTags("Approved Vendors").WithDescription("The Approved Vendors for the Company");

        //group.MapPost("/", AddingAVendor.CanAddVendorAsync).RequireAuthorization("canAddVendors");
        group.MapPost("/", AddingAVendor.CanAddVendorAsync);
        group.MapGet("/{id}", GettingAVendor.GetVendorAsync).WithTags("Approved Vendors", "Catalog");
        group.MapGet("/", GettingAVendor.GetVendorsAsync);
        return group;

    }
}
