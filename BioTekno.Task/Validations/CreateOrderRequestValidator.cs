using BioTekno.Task.Models.Input;
using FluentValidation;

namespace BioTekno.Task.Validations
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {

        public CreateOrderRequestValidator()
        {
            RuleFor(r => r.ProductDetails.Select(x => x.ProductId))
                .NotNull();

            RuleFor(r => r.CustomerName)
                .NotEmpty()
                .NotNull();
            RuleFor(r => r.CustomerEmail)
                .NotEmpty()
                .NotNull();
            RuleFor(r => r.CustomerGSM)
                .NotEmpty()
                .NotNull();

            
            //Do Something..
        }
    }
}
