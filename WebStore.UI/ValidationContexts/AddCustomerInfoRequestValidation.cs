using FluentValidation;
using WebStore.Application.Cart;

namespace WebStore.UI.ValidationContexts
{
    public class AddCustomerInfoRequestValidation : AbstractValidator<AddCustomerInfo.Request>
    {
        public AddCustomerInfoRequestValidation()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.EmailAddress).NotNull().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(8);
            RuleFor(x => x.Address1).NotNull();
            RuleFor(x => x.City).NotNull();
            RuleFor(x => x.Postcode).NotNull();
        }
    }
}
