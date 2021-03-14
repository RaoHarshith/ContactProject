using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ContactApi.Interfaces;
using ContactApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
		private readonly IContactManager _contactManager;

		public ContactsController (IContactManager contactManager)
		{
			_contactManager = contactManager;
		}

		[HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            return  Ok(await _contactManager.ListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(int id)
        {
            try
			{
				return Ok(await _contactManager.GetAsync(id));
			}
			catch(KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> Post([FromBody] Contact contact)
        {
			return Ok(await _contactManager.CreateAsync(contact));
        }

        [HttpPut]
        public async Task<ActionResult<Contact>> Put([FromBody] Contact contact)
        {
			try
			{
				return Ok(await _contactManager.UpdateAsync(contact));
			}
			catch(Exception Ex)
			{
				return BadRequest(Ex.Message);
			}
			
        }

		[HttpPatch("{id}")]
        public async Task<ActionResult<Contact>> Patch([Required] int id,[FromBody] JsonPatchDocument<Contact> contactPatch)
        {
			try
			{
				return Ok(await _contactManager.PatchAsync( id, contactPatch));
			}
			catch(Exception Ex)
			{
				return BadRequest(Ex.Message);
			}
			
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
			try
			{
				await _contactManager.DeleteAsync(id);
				return Ok();
			}
			catch(KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
        }
    }
}

