using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.DataSources.Commands.Update
{
    internal class UpdateDataSourceValidator : AbstractValidator<UpdateDataSourceCommand>
    {
        public UpdateDataSourceValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name)
             .NotNull()
             .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
             .MaximumLength(150).WithMessage("{PropertyName} maksimal karakter 150");
            RuleFor(x => x.ConnString)
              .MinimumLength(1).WithMessage("{PropertyName} minimal length 1");
            RuleFor(x => x.UseEtl)
              .NotEmpty();
        }
    }
}
