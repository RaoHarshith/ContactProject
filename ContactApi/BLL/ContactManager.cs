using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactApi.Interfaces;
using ContactApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ContactApi.BLL
{
    public class ContactManager : IContactManager
    {
		private readonly Context _context;

		public ContactManager (Context context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Contact>> ListAsync()
		{
			return await _context.Contact.AsNoTracking().ToListAsync();
		}

		public async Task<Contact> GetAsync(int id)
		{
			Contact model = null; 

			model = await _context.Contact.Where(x => x.Id == id).FirstOrDefaultAsync();
			if (model == null)
			{
				throw new KeyNotFoundException("Contact not found");
			}
			else 
			{
				return model;
			}
		}

		public async Task<Contact> CreateAsync(Contact contact)
		{
			await _context.Contact.AddAsync(contact);
			await _context.SaveChangesAsync();
			return contact;
		}

		public async Task DeleteAsync(int id)
		{
			Contact contact = await GetAsync(id);
			_context.Contact.Remove(contact);
			await _context.SaveChangesAsync();
		}

		public async Task<Contact> UpdateAsync(Contact contact)
		{
			_context.Update(contact);
			await _context.SaveChangesAsync();
			return contact;
		}

		public async Task<Contact> PatchAsync(int id, JsonPatchDocument<Contact> contactPatch)
		{
			var contact = await _context.Contact.FindAsync(id);

			if (contact == null)
			{
				throw new InvalidOperationException($"Could not retrieve contact with id: {id}");
			}

			var idReplaceOperation = contactPatch.Operations.FirstOrDefault(x => x.path == "/Id");
			if (idReplaceOperation != null)
			{
				throw new JsonPatchException(new JsonPatchError(contact, idReplaceOperation, "The property at path 'Id' could not be updated."));
			}

			contactPatch.ApplyTo(contact);
			await _context.SaveChangesAsync();
			return contact;
		}
	}
}