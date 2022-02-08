using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Create
{
    internal class CreateTimebucketValidator : AbstractValidator<CreateTimebucketCommand>
    {
        public CreateTimebucketValidator()
        {
            RuleFor(x => x.Code)
              .NotNull()
              .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
              .MaximumLength(10).WithMessage("{PropertyName} maksimal karakter 10");
            RuleFor(x => x.Label)
              .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
              .MaximumLength(50).WithMessage("{PropertyName} maksimal length 50");
            RuleFor(x => x.Sequence.ToString())
              .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
              .MaximumLength(10).WithMessage("{PropertyName} maksimal length 10");
        }
    }
}
