using FastEndpoints;
using FluentValidation;

namespace Rainfall.Api.Endpoints.Rainfall;

public class RainfallReadingsRequestValidator : Validator<RainfallReadingsRequest>
{
    public RainfallReadingsRequestValidator()
    {
        RuleFor(x => x.StationId)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Count)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .When(x => x.Count.HasValue);
    }

}
