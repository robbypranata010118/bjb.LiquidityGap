using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.DataSources.Commands.Create
{
    public class CreateDataSourceValidator : AbstractValidator<CreateDataSourceCommand>
    {
        public CreateDataSourceValidator()
        {
            RuleFor(x => x.Name)
              .NotNull()
              .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
              .MaximumLength(150).WithMessage("{PropertyName} maksimal 150 karakter");
            RuleFor(x => x.ConnString)
              .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter");
            RuleFor(x => x.UseEtl)
              .NotEmpty();

        }
    }
}
