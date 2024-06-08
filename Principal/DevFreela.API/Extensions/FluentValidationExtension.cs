using DevFreela.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace DevFreela.API.Extensions;

public static class FluentValidationExtension
{
    public static void AddConfigFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>();
    }
}
