namespace SoftwareCatalog.Api.Techs;

using Riok.Mapperly.Abstractions;


[Mapper]
public static partial class TechMappers
{
    public static partial IQueryable<TechDetailsResponseModel> ProjectToModel(this IQueryable<TechEntity> entity);

    [MapProperty(nameof(TechEntity.Slug), nameof(TechDetailsResponseModel.Name))]
    [MapperIgnoreSource(nameof(TechEntity.Id))]
    [MapperIgnoreSource(nameof(TechEntity.Sub))]
    public static partial TechDetailsResponseModel MapToModel(this TechEntity entity);
}
