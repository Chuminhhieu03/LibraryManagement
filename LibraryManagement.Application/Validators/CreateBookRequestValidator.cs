using FluentValidation;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.Validators
{
    /// <summary>
    /// Validator for the <see cref="CreateBookRequest"/> class that ensures all properties adhere to predefined validation rules and business constraints.
    /// </summary>
    /// <remarks>
    /// This validator uses FluentValidation to ensure the following:
    /// - ISBN is required, must be 13 characters long, and unique.
    /// - Title is required and cannot exceed 300 characters.
    /// - Pages must be greater than 0.
    /// - Total copies must be greater than 0.
    /// - Category must be specified and exist in the database.
    /// - Publisher must be specified and exist in the database.
    /// - At least one valid author must be included.
    /// - Price, if provided, must be greater than 0.
    /// </remarks>
    public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Provides validation logic for the <see cref="CreateBookRequest"/> DTO to ensure all required fields meet specific constraints and business rules.
        /// </summary>
        /// <param name="unitOfWork">The unit of work responsible for database operations and repository access.</param>
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

        /// <summary>
        /// Checks whether a book with the specified ISBN already exists in the database.
        /// </summary>
        /// <param name="isbn">The ISBN of the book to check for uniqueness.</param>
        /// <param name="cancellationToken">The cancellation token to observe during the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the ISBN is unique, otherwise false.</returns>
        private async Task<bool> BeUniqueISBN(string isbn, CancellationToken cancellationToken)
        {
            var existingBook = await _unitOfWork.Repository<Book>()
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
            return existingBook == null;
        }

        /// <summary>
        /// Checks whether a category with the specified ID exists in the database.
        /// </summary>
        /// <param name="categoryId">The ID of the category to check.</param>
        /// <param name="cancellationToken">The cancellation token to observe during the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the category exists, otherwise false.</returns>
        private async Task<bool> CategoryExists(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.Repository<Category>()
                .GetByIdAsync(categoryId);
            return category != null;
        }

        /// <summary>
        /// Checks whether a publisher with the specified ID exists in the database.
        /// </summary>
        /// <param name="publisherId">The ID of the publisher to check.</param>
        /// <param name="cancellationToken">The cancellation token to observe during the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if the publisher exists, otherwise false.</returns>
        private async Task<bool> PublisherExists(int publisherId, CancellationToken cancellationToken)
        {
            var publisher = await _unitOfWork.Repository<Publisher>()
                .GetByIdAsync(publisherId);
            return publisher != null;
        }

        /// <summary>
        /// Validates whether all authors with the specified IDs exist in the database.
        /// </summary>
        /// <param name="authorIds">The list of author IDs to validate.</param>
        /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation. The task result is true if all specified authors exist, otherwise false.</returns>
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