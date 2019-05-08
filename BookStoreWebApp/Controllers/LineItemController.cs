using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreWebApp.Models.ViewModels;
using BookStoreWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILineItemService lineItemService;

        public LineItemController(IMapper mapper, ILineItemService lineItemService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.lineItemService = lineItemService ?? throw new ArgumentNullException(nameof(lineItemService));
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<LineItemViewModel>> Get()
        {
            return mapper.Map<IEnumerable<LineItemViewModel>>(await lineItemService.GetAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineItemViewModel>> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            return this.Ok(await lineItemService.GetByIdAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LineItemViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var li = mapper.Map<Services.Dtos.LineItem>(model);

            return this.Created("", await lineItemService.AddAsync(li));
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put(Guid id, [FromBody] LineItemViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var li = mapper.Map<Services.Dtos.LineItem>(model);

            await lineItemService.UpdateAsync(li);

            return this.Ok();
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            await lineItemService.RemoveAsync(id);
            return this.Ok();
        }
    }
}
