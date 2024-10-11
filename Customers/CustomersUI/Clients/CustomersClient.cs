using CustomersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Http.Json;

namespace CustomersUI.Clients
{
    public class CustomersClient(HttpClient httpClient)
    {
        /// <summary>
        /// Get all Customers.
        /// </summary>
        /// <returns>All Customers currently stored on the server-side database.</returns>
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Customer>>($"api/Customers") ?? new List<Customer>();
        }

        /// <summary>
        /// Adds a customer to the server-side database.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>A Http Response detailing the outcome of the API call.</returns>
        public async Task<HttpResponseMessage> AddCustomerAsync(Customer customer)
        {
            return await httpClient.PostAsJsonAsync($"api/Customers", customer);
        }

        /// <summary>
        /// Delete a customer with the given id.
        /// </summary>
        /// <param name="id">Id of the customer to delete.</param>
        /// <returns>A Http Response detailing the outcome of the API call.</returns>
        public async Task<HttpResponseMessage> DeleteCustomerAsync(long id)
        {
            return await httpClient.DeleteAsync($"api/Customers/{id}");
        }

        /// <summary>
        /// Updates the customer in the server-side database.
        /// </summary>
        /// <param name="customer">The customer to update.</param>
        /// <returns>A Http Response detailing the outcome of the API call.</returns>
        public async Task<HttpResponseMessage> UpdateCustomerAsync(Customer customer)
        {
            return await httpClient.PutAsJsonAsync($"api/Customers/{customer.Id}", customer);
        }
    }
}
