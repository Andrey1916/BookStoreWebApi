using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreWebApp.Models.BindingModels;
using BookStoreWebApp.Models.ViewModels;
using BookStoreWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;

        public ReviewController(IMapper mapper, IReviewService reviewService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<ReviewViewModel>> Get()
        {
            var reviews = await reviewService.GetAllAsync();
            return mapper.Map<IEnumerable<ReviewViewModel>>(reviews);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewViewModel>> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            var review = await reviewService.GetByIdAsync(id);
            return mapper.Map<ReviewViewModel>(review);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]AddReviewBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var review = mapper.Map<Services.Dtos.Review>(model);

            return this.Ok(await reviewService.AddAsync(review));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody]ReviewViewModel model)
        {
            if (id != model.Id || !this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var review = mapper.Map<Services.Dtos.Review>(model);
            await reviewService.Update(review);

            return this.Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                this.BadRequest();
            }

            await reviewService.Remove(id);

            return this.Ok();
        }
    }
}
