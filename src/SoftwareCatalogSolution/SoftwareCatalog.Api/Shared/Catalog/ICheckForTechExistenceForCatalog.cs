namespace SoftwareCatalog.Api.Shared.Catalog;

public interface ICheckForTechExistenceForCatalog
{
    Task<bool> DoesTechExistAsync(string slug);

}
