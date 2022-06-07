using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            //var records = await _context.Books.Select(x => new BookModel
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description
            //}).ToListAsync();

            //return records;

            //With mapper

            var records=await _context.Books.ToListAsync();
            return _mapper.Map<List<BookModel>>(records);
        }

        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            //var records = await _context.Books.Where(x => x.Id == id)
            //    .Select(x => new BookModel
            //    {
            //        Id = x.Id,
            //        Title = x.Title,
            //        Description = x.Description
            //    }).ToListAsync();

            //return records;

            //With mapper
            var book = await _context.Books.FindAsync(id);
            return _mapper.Map<BookModel>(book);
        }

        public async Task<int> AddNewBookAsync(BookModel book)
        {
            var newBook = new Books
            {
                Title = book.Title,
                Description = book.Description
            };

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        {
            //var BookDetails =await _context.Books.FindAsync(bookId);
            //if (BookDetails != null)
            //{
            //    BookDetails.Title = bookModel.Title;
            //    BookDetails.Description = bookModel.Description;

            //    await _context.SaveChangesAsync();
            //}

            var book = new Books
            {
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
        {
            var BookDetails = await _context.Books.FindAsync(bookId);
            if (BookDetails != null)
            {
                bookModel.ApplyTo(BookDetails);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int bookId)
        {
            var books = new Books { Id = bookId };
            _context.Books.Remove(books);
            await _context.SaveChangesAsync();
        }
    }
}
