using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreWebApp.Models.ViewModels;
using BookStoreWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IOrderService orderService;

        public OrderController(IMapper mapper, IOrderService orderService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<OrderInfoViewModel>> Get()
        {
            return mapper.Map<IEnumerable<OrderInfoViewModel>>(await orderService.GetAllAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderInfoViewModel>> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            return this.Ok(await orderService.GetByIdAsync(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var order = mapper.Map<Services.Dtos.Order>(model);

            return this.Created("", await orderService.AddAsync(order));
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put(Guid id, [FromBody] OrderInfoViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }


            //await orderService.(model.OldName, model.NewName);

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

            await orderService.RemoveAsync(id);
            return this.Ok();
        }
    }
}