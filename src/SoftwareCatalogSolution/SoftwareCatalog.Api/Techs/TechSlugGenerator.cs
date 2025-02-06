
using Marten;
using Slugify;

namespace SoftwareCatalog.Api.Techs;
using SoftwareCatalog.Api.Shared.Catalog;


public class TechSlugGenerator(ICheckForUniqueTechSlugs uniqueSlugChecker)
{
    private readonly SlugHelper _helper = new SlugHelper();
    public async Task<string> GenerateSlugFor(string techName)
    {


        var slug = _helper.GenerateSlug(techName);

        if (await uniqueSlugChecker.CheckUniqueSlug(slug))
        {
            return slug;
        }

        var tries = Enumerable.Range(1, 20);
        foreach (var aTry in tries)
        {
            var candidate = $"{slug}-{aTry}";
            if (await uniqueSlugChecker.CheckUniqueSlug(candidate))
            {
                return candidate;
            }
        }

        return slug + "-" + Guid.NewGuid();
    }
}

public interface ICheckForUniqueTechSlugs
{
    Task<bool> CheckUniqueSlug(string slug);
}

public class TechDataService(IDocumentSession session) : ICheckForUniqueTechSlugs, ICheckForTechExistenceForCatalog
{
    public async Task<bool> CheckUniqueSlug(string slug)
    {
        return !await session.Query<TechEntity>().AnyAsync(v => v.Slug == slug);

    }

    async Task<bool> ICheckForTechExistenceForCatalog.DoesTechExistAsync(string slug)
    {
        return !await this.CheckUniqueSlug(slug);
    }
}