using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.SubCategories.Commands.Update
{
    public class UpdateSubCategoryValidator : AbstractValidator<UpdateSubCategoryCommand>
    {
        public UpdateSubCategoryValidator()
        {
            RuleFor(x => x.CategoryId.ToString())
            .NotNull()
            .MinimumLength(1).WithMessage("{PropertyName} min length 1")
            .MaximumLength(10).WithMessage("{PropertyName} max length 10");
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Code)
             .NotNull()
             .MinimumLength(1).WithMessage("{PropertyName} min length 1")
             .MaximumLength(10).WithMessage("{PropertyName} max length 10");
            RuleFor(x => x.Name)
              .MinimumLength(1).WithMessage("{PropertyName} min length 1")
              .MaximumLength(100).WithMessage("{PropertyName} max length 100");
        }
    }
}
