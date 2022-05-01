using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BOOKController : ControllerBase
    {
        private readonly IBookBL bookBL;

        public BOOKController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        
        [HttpPost("Add")]
        public IActionResult AddBook(AddBookModel addbook)
        {
            try
            {
                var book = this.bookBL.AddBook(addbook);
                if (book!= null)
                {
                    return this.Ok(new { Success = true, message = "Book Added Sucessfully", Response = book });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Error While Adding Book" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Success = false, message = ex.Message });
            }
        }

        
        [HttpGet("{bookId}/Get")]
        public IActionResult GetBookByBookId(int bookId)
        {
            try
            {
                var book = this.bookBL.GetBookByBookId(bookId);
                if (book!= null)
                {
                    return this.Ok(new { Success = true, message = "Book Details of Given Book Id Fetched Sucessfully", Response = book });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Please Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Success = false, message = ex.Message });
            }
        }

        
        [HttpGet("GetAll")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var allbooks = this.bookBL.GetAllBooks();
                if (allbooks != null)
                {
                    return this.Ok(new { Success = true, message = "All Book Details Fetched Sucessfully", Response = allbooks });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Something Went wrong" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Success = false, message = ex.Message });
            }
        }

        
        [HttpPut("Update")]
        public IActionResult UpdateBookDetails(BookModel bookModel)
        {
            try
            {
                var Book = this.bookBL.UpdateBookDetails(bookModel);
                if (Book!= null)
                {
                    return this.Ok(new { Success = true, message = "Book Details Updated Sucessfully", Response = Book });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Error! There was problem Updating the Book Details" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Success = false, message = ex.Message });
            }
        }

        
        [HttpDelete("Delete")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                if (this.bookBL.DeleteBook(bookId))
                {
                    return this.Ok(new { Success = true, message = "Book Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Please Enter Correct Book Id" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Success = false, message = ex.Message });
            }
        }
    }
}
