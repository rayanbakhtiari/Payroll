using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests
{
    public abstract class ValidatorTestHelper<T, TValidator> where T: class where TValidator : AbstractValidator<T>, new()
    {
        protected AbstractValidator<T> targetValidator;

        protected IEnumerable<ValidationFailure> AssertThrowValidationErrorFor<TProperty>(T target, Expression<Func<T, TProperty>> memberAccessor)
        {
            targetValidator = new TValidator();
            var validationResult = targetValidator.TestValidate(target);
            return validationResult.ShouldHaveValidationErrorFor(memberAccessor);
        }
        protected IEnumerable<ValidationFailure> AssertThrowValidationErrorFor<TProperty>(T target, string propertyName)
        {
            targetValidator = new TValidator();
            var validationResult = targetValidator.TestValidate(target);
            return validationResult.ShouldHaveValidationErrorFor(propertyName);
        }
        protected void AssertNotThrowValidationErrorFor<TProperty>(T target, Expression<Func<T, TProperty>> memberAccessor)
        {
            targetValidator = new TValidator();
            var validationResult = targetValidator.TestValidate(target);
            validationResult.ShouldNotHaveValidationErrorFor(memberAccessor);
        }
        protected void AssertNotThrowValidationErrorFor<TProperty>(T target, string propertyName)
        {
            targetValidator = new TValidator();
            var validationResult = targetValidator.TestValidate(target);
            validationResult.ShouldNotHaveValidationErrorFor(propertyName);
        }
    }
}
