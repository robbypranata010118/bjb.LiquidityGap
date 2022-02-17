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
             .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
             .MaximumLength(10).WithMessage("{PropertyName} maksimal karakter 10");
            RuleFor(x => x.DataSourceId.ToString())
             .MaximumLength(10).WithMessage("{PropertyName} maksimal karakter 10");
            RuleFor(x => x.SheetItemParentId.ToString())
             .MaximumLength(10).WithMessage("{PropertyName} maksimal karakter 10");
            RuleFor(x => x.Code)
             .NotNull()
             .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
             .MaximumLength(10).WithMessage("{PropertyName} maksimal karakter 10");
            RuleFor(x => x.Name)
             .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
             .MaximumLength(100).WithMessage("{PropertyName} maksimal karakter 100");
            RuleFor(x => x.MarkToCalculate)
             .NotEmpty();
            RuleFor(x => x.IsManualInput)
             .NotEmpty();

        }
    }
}
