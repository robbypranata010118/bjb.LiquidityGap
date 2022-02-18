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
            .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
             .MaximumLength(10).WithMessage("{PropertyName} maksimal 10 karakter");
            RuleFor(x => x.Code)
              .NotNull()
              .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
              .MaximumLength(10).WithMessage("{PropertyName} maksimal 10 karakter");
            RuleFor(x => x.Name)
              .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
              .MaximumLength(100).WithMessage("{PropertyName} maksimal 100 karakter");
        }
    }
}
