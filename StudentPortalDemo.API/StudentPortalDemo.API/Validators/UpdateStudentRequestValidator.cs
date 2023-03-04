using FluentValidation;
using StudentPortalDemo.API.DomainModels;
using StudentPortalDemo.API.Repositories;

namespace StudentPortalDemo.API.Validators
{
    public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentRequestValidator(IStudentsRepo studentsRepo)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).GreaterThan(99999).LessThan(100000000000);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = studentsRepo.GetGendersAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if (gender != null)
                {
                    return true;
                }

                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();

        }
    }
    }
}
