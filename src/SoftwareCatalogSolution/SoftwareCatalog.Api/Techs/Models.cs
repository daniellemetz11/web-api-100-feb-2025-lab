namespace SoftwareCatalog.Api.Techs;


using FluentValidation;


public record TechCreateModel
{
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    //public string Sub { get; set; } = string.Empty;
}

public class TechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public TechCreateModelValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(v => v).Must(v => !string.IsNullOrWhiteSpace(v.Email) || !string.IsNullOrWhiteSpace(v.Phone))
           .WithMessage("Either Email or Phone must be provided.");
    }
}

public class UpdatedTechCreateModelValidator : AbstractValidator<TechCreateModel>
{
    public UpdatedTechCreateModelValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(2).MaximumLength(100);
        RuleFor(v => v).Must(v => !string.IsNullOrWhiteSpace(v.Email) || !string.IsNullOrWhiteSpace(v.Phone))
           .WithMessage("Either Email or Phone must be provided.");
    }
}
public record TechDetailsResponseModel
{
    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;

    public string? Phone { get; set; } = string.Empty;

}

