using AutoMapper;
using MediatR;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Interfaces;
using LibraryManagement.Shared.DTOs.Common;
using LibraryManagement.Shared.DTOs.Books;
using LibraryManagement.Application.Commands.Books;

namespace LibraryManagement.Application.Handlers.Books
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ApiResponse<BookResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BookResponse>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var book = _mapper.Map<Book>(command.Request);

                await _unitOfWork.Repository<Book>().AddAsync(book);
                await _unitOfWork.SaveChangesAsync();

                foreach (var authorId in command.Request.AuthorIds)
                {
                    var bookAuthor = new BookAuthor
                    {
                        BookId = book.BookId,
                        AuthorId = authorId
                    };
                    await _unitOfWork.Repository<BookAuthor>().AddAsync(bookAuthor);
                }

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                var createdBook = await _unitOfWork.Repository<Book>().GetByIdAsync(
                    book.BookId,
                    b => b.Category,
                    b => b.Publisher,
                    b => b.BookAuthors
                );

                if (createdBook == null)
                {
                    throw new KeyNotFoundException($"Book with ID {book.BookId} was not found after creation.");
                }

                var response = _mapper.Map<BookResponse>(createdBook);

                return ApiResponse<BookResponse>.SuccessResult(response, "Book created successfully");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }

    //public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, ApiResponse<BookResponse>>
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMapper _mapper;

    //    public GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _mapper = mapper;
    //    }

    //    public async Task<ApiResponse<BookResponse>> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
    //    {
    //        var book = await _unitOfWork.Repository<Book>().GetByIdAsync(
    //            query.Id,
    //            b => b.Category,
    //            b => b.Publisher,
    //            b => b.BookAuthors
    //        );

    //        if (book == null)
    //        {
    //            throw new KeyNotFoundException($"Book with ID {query.Id} was not found.");
    //        }

    //        var response = _mapper.Map<BookResponse>(book);
    //        return ApiResponse<BookResponse>.SuccessResult(response);
    //    }
    //}

    //public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, ApiResponse<PagedResponse<BookResponse>>>
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IMapper _mapper;

    //    public GetBooksQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _mapper = mapper;
    //    }

    //    public async Task<ApiResponse<PagedResponse<BookResponse>>> Handle(GetBooksQuery query, CancellationToken cancellationToken)
    //    {
    //        var (books, totalCount) = await _unitOfWork.Repository<Book>().GetPagedAsync(
    //            query.PageNumber - 1,
    //            query.PageSize,
    //            filter: b => string.IsNullOrEmpty(query.SearchTerm) || 
    //                       b.Title.Contains(query.SearchTerm) ||
    //                       b.ISBN.Contains(query.SearchTerm),
    //            orderBy: q => q.OrderBy(b => b.Title)
    //        );

    //        var bookResponses = _mapper.Map<IEnumerable<BookResponse>>(books);

    //        var pagedResponse = PagedResponse<BookResponse>.Create(
    //            bookResponses,
    //            query.PageNumber,
    //            query.PageSize,
    //            totalCount);

    //        return ApiResponse<PagedResponse<BookResponse>>.SuccessResult(pagedResponse);
    //    }
    //}
}