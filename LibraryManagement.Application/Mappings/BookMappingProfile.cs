using AutoMapper;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Shared.DTOs.Books;

namespace LibraryManagement.Application.Mappings
{
    /// <summary>
    /// Defines the AutoMapper profile configuration for mapping between
    /// Book-related domain entities and Data Transfer Objects (DTOs).
    /// </summary>
    public class BookMappingProfile : Profile
    {
        /// Provides mapping configurations between book-related DTOs and domain entities.
        public BookMappingProfile()
        {
            CreateMap<CreateBookRequest, Book>()
                .ForMember(dest => dest.AvailableCopies, opt => opt.MapFrom(src => src.TotalCopies))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => BookStatus.Available))
                .ForMember(dest => dest.BookAuthors, opt => opt.Ignore())
                .ForMember(dest => dest.BookId, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Publisher, opt => opt.Ignore())
                .ForMember(dest => dest.BookLoans, opt => opt.Ignore())
                .ForMember(dest => dest.Reservations, opt => opt.Ignore());

            CreateMap<Book, BookResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name))
                .ForMember(dest => dest.AuthorNames, opt => opt.MapFrom(src => 
                    src.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.LastName}")));
        }
    }
}