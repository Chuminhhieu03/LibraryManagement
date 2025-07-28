using FluentValidation;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Validators
{
    public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookRequestValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required")
                .Length(13).WithMessage("ISBN must be exactly 13 characters")
                .MustAsync(BeUniqueISBN).WithMessage("ISBN already exists");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(300).WithMessage("Title cannot exceed 300 characters");

            RuleFor(x => x.Pages)
                .GreaterThan(0).WithMessage("Pages must be greater than 0");

            RuleFor(x => x.TotalCopies)
                .GreaterThan(0).WithMessage("Total copies must be greater than 0");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category is required")
                .MustAsync(CategoryExists).WithMessage("Category does not exist");

            RuleFor(x => x.PublisherId)
                .GreaterThan(0).WithMessage("Publisher is required")
                .MustAsync(PublisherExists).WithMessage("Publisher does not exist");

            RuleFor(x => x.AuthorIds)
                .NotEmpty().WithMessage("At least one author is required")
                .MustAsync(AllAuthorsExist).WithMessage("One or more authors do not exist");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .When(x => x.Price.HasValue);
        }

        private async Task<bool> BeUniqueISBN(string isbn, CancellationToken cancellationToken)
        {
            var existingBook = await _unitOfWork.Repository<Book>()
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
            return existingBook == null;
        }

        private async Task<bool> CategoryExists(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>()
                .GetByIdAsync(categoryId);
            return category != null;
        }

        private async Task<bool> PublisherExists(int publisherId, CancellationToken cancellationToken)
        {
            var publisher = await _unitOfWork.Repository<Publisher>()
                .GetByIdAsync(publisherId);
            return publisher != null;
        }

        private async Task<bool> AllAuthorsExist(List<int> authorIds, CancellationToken cancellationToken)
        {
            if (!authorIds.Any()) return false;

            foreach (var authorId in authorIds)
            {
                var author = await _unitOfWork.Repository<Author>()
                    .GetByIdAsync(authorId);
                if (author == null) return false;
            }
            return true;
        }
    }
}