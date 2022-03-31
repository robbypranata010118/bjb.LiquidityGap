using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.TimeBuckets.Commands.Update
{
    public class UpdateTimeBucketValidator : AbstractValidator<UpdateTimeBucketCommand>
    {
        public UpdateTimeBucketValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Code)
             .NotNull()
              .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
              .MaximumLength(10).WithMessage("{PropertyName} maksimal 10 karakter");
            RuleFor(x => x.Label)
              .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
              .MaximumLength(50).WithMessage("{PropertyName} maksimal 50 length");
            RuleFor(x => x.Sequence.ToString())
              .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
              .MaximumLength(10).WithMessage("{PropertyName} maksimal 10 karakter");
            RuleFor(x => x.CharacteristicTimebuckets.FirstOrDefault().DayRange)
              .GreaterThan(0).WithMessage("{PropertyName} harus lebih besar dari 0");
            RuleFor(x => x.CharacteristicTimebuckets.FirstOrDefault().Percentage)
              .NotEmpty().WithMessage("{PropertyName} jika menggunakan persentase maka persentase tidak boleh kosong ")
              .GreaterThan(0).WithMessage("{PropertyName} harus lebih besar dari 0")
              .When(x => x.CharacteristicTimebuckets.FirstOrDefault().UsePercentage == true);
        }
    }
}
