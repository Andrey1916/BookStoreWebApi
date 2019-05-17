using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreWebApp.DAL;
using BookStoreWebApp.Models.BindingModels;
using BookStoreWebApp.Models.ViewModels;
using BookStoreWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBookService bookService;

        public BookController(IMapper mapper, IBookService bookService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<BookViewModel>> Get()
        {
            var books = await bookService.GetAllAsync();
            return mapper.Map<IEnumerable<BookViewModel>>(books.ToList());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewModel>> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            var book = await bookService.GetByIdAsync(id);

            return this.Ok(mapper.Map<BookViewModel>(book));
        }

        // GET api/<controller>/5
        [HttpGet("Reviews/{id}")]
        public async Task<ActionResult<IEnumerable<ReviewViewModel>>> GetReviews(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                this.BadRequest();
            }

            return this.Ok(await bookService.GetAllBookReviews(bookId));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody]AddBookBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            return this.Ok(await bookService.AddAsync(mapper.Map<Services.Dtos.Book>(model)));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody]BookViewModel model)
        {
            if (id == Guid.Empty || !this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await bookService.UpdateAsync(
                mapper.Map<Services.Dtos.Book>(model)
                );

            return this.Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            await bookService.RemoveAsync(id);
            return this.Ok();
        }
    }
}
