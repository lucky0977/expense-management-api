using ExpenseManagement.Application.DTOs.Expenses;
using FluentValidation;

namespace ExpenseManagement.Application.Validators
{
    public class CreateExpenseRequestValidator
        : AbstractValidator<CreateExpenseRequest>
    {
        public CreateExpenseRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category is required");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Date is required");
        }
    }
}
