    using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.SheetItems.Commands.Update
{
    public class UpdateSheetItemValidator : AbstractValidator<UpdateSheetItemCommand>
    {
        public UpdateSheetItemValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.SubCategoryId.ToString())
             .NotNull()
             .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
             .MaximumLength(10).WithMessage("{PropertyName} maksimal 10 karakter");
            RuleFor(x => x.DataSourceId.ToString())
             .MaximumLength(10).WithMessage("{PropertyName} maksimal 10 karakter");
            RuleFor(x => x.SheetItemParentId.ToString())
             .MaximumLength(10).WithMessage("{PropertyName} maksimal 10 karakter");
            RuleFor(x => x.Code)
             .NotNull()
             .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
             .MaximumLength(10).WithMessage("{PropertyName} maksimal 10 karakter");
            RuleFor(x => x.Name)
             .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
             .MaximumLength(100).WithMessage("{PropertyName} maksimal 100 karakter");
            RuleFor(x => x.MarkToCalculate)
             .NotEmpty();
            RuleFor(x => x.IsManualInput)
             .NotEmpty();

        }
    }
}
