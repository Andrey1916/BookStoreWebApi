using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreWebApp.Models.BindingModels;
using BookStoreWebApp.Models.ViewModels;
using BookStoreWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            this.authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return (await authorService.GetAllAsync()).ToList();
        }
        

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            return await authorService.GetByIdAsync(id);
        }
        
        [HttpGet("Books/{author}")]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> Get(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                return this.BadRequest();
            }

            var books = await authorService.GetAuthorBooksAsync(author);
            return this.Ok(mapper.Map<IEnumerable<BookViewModel>>(books));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                return this.BadRequest();
            }

            return this.Created("", await authorService.AddAsync(author));
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ChangeAuthorNameBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await authorService.ChangeNameAsync(model.OldName, model.NewName);

            return this.Ok();            
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                return this.BadRequest();
            }

            await authorService.RemoveAsync(author);
            return this.Ok();
        }
    }
}