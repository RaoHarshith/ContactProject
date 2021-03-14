using System.Collections.Generic;
using System.Threading.Tasks;
using ContactApi.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ContactApi.Interfaces
{
    public interface IContactManager
    {
		Task<IEnumerable<Contact>> ListAsync();

		Task<Contact> GetAsync(int id);

		Task<Contact> CreateAsync(Contact contact);

		Task<Contact> UpdateAsync(Contact contact);

		Task<Contact> PatchAsync(int id, JsonPatchDocument<Contact> contactPatch);

		Task DeleteAsync(int id);
    }
}