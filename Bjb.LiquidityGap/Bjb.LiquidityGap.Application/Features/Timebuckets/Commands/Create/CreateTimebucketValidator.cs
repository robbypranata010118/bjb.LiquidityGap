﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.TimeBuckets.Commands.Create
{
    public class CreateTimeBucketValidator : AbstractValidator<CreateTimeBucketCommand>
    {
        public CreateTimeBucketValidator()
        {
            RuleFor(x => x.Code)
              .NotNull()
              .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
              .MaximumLength(10).WithMessage("{PropertyName} maksimal 10 karakter");
            RuleFor(x => x.Label)
              .MinimumLength(1).WithMessage("{PropertyName} minimal 1 karakter")
              .MaximumLength(50).WithMessage("{PropertyName} maksimal 50 length");
            RuleFor(x => x.Sequence)
              .GreaterThan(0).WithMessage("{PropertyName} harus lebih besar dari 0");
            RuleFor(x => x.CharacteristicTimebuckets.DayRange)
              .GreaterThan(0).WithMessage("{PropertyName} harus lebih besar dari 0");
            RuleFor(x => x.CharacteristicTimebuckets.Percentage)
              .NotEmpty().WithMessage("{PropertyName} jika menggunakan persentase maka persentase tidak boleh kosong ")
              .GreaterThan(0).WithMessage("{PropertyName} harus lebih besar dari 0")
              .When(x => x.CharacteristicTimebuckets.UsePercentage == true);

        }
    }
}
