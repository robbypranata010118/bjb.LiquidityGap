using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bjb.LiquidityGap.Application.Features.Characteristics.Commands.Update
{
    public class UpdateCharacteristicValidator : AbstractValidator<UpdateCharacteristicCommand>
    {
        public UpdateCharacteristicValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Code)
             .NotNull()
             .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
             .MaximumLength(10).WithMessage("{PropertyName} maksimal karakter 10");
            RuleFor(x => x.Name)
              .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
              .MaximumLength(100).WithMessage("{PropertyName} maksimal karakter 100");
            RuleFor(x => x.Description)
              .MinimumLength(1).WithMessage("{PropertyName} minimal karakter 1")
              .MaximumLength(150).WithMessage("{PropertyName} maksimal karakter 150");
        }
    }

}
