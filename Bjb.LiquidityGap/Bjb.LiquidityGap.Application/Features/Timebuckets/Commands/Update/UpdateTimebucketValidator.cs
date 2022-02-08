using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.Timebuckets.Commands.Update
{
    public class UpdateTimebucketValidator : AbstractValidator<UpdateTimebucketCommand>
    {
        public UpdateTimebucketValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Code)
             .NotNull()
             .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
             .MaximumLength(10).WithMessage("{PropertyName} maksimal karakter 10");
            RuleFor(x => x.Label)
              .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
              .MaximumLength(50).WithMessage("{PropertyName} maksimal karakter 50");
            RuleFor(x => x.Sequence.ToString())
              .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
              .MaximumLength(10).WithMessage("{PropertyName} maksimal karakter 10");
        }
    }
}
