using CustomersAPI.Models;
using CustomersUI.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;

namespace CustomersUI.Pages
{
    public class IndexModel : PageModel
    {
        private const string TelephoneNoRegex = @"^(?:(?:\(?(?:0(?:0|11)\)?[\s-]?\(?|\+)44\)?[\s-]?(?:\(?0\)?[\s-]?)?)|(?:\(?0))(?:(?:\d{5}\)?[\s-]?\d{4,5})|(?:\d{4}\)?[\s-]?(?:\d{5}|\d{3}[\s-]?\d{3}))|(?:\d{3}\)?[\s-]?\d{3}[\s-]?\d{3,4})|(?:\d{2}\)?[\s-]?\d{4}[\s-]?\d{4}))(?:[\s-]?(?:x|ext\.?|\#)\d{3,4})?$";
        private const string EmailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        private readonly CustomersClient _client;

        [BindProperty]
        public Customer? Customer { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<Customer> Customers { get; set; } = new List<Customer>();

        [BindProperty(SupportsGet = true)]
        public string? CurrentErrorMessage { get; private set; }

        public IndexModel(CustomersClient client)
        {
            _client = client;
        }

        public async Task OnGetAsync()
        {
            IEnumerable<Customer> customers = await _client.GetCustomersAsync();

            if (customers is not null)
            {
                Customers = customers.ToList();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Customer != null)
            {
                // NOTE:
                // Tried adding in validation for the contact number and email address however
                // I couldn't get it to retain the values for the message and the list of
                // customers meaning I would either get the error message to appear and have no
                // customers or get the customers to appear but have no error message.
                // It will now instead just reload the page if the user enters incorrect values.

                //string validateMessage = ValidateCustomer();
                //if (!string.IsNullOrWhiteSpace(validateMessage))
                //{
                //    CurrentErrorMessage = $"Add Failed...{validateMessage}";
                //    return RedirectToPage();
                //}

                HttpResponseMessage result = await _client.AddCustomerAsync(Customer);

                if (result.IsSuccessStatusCode)
                {
                    CurrentErrorMessage = string.Empty;
                    return RedirectToPage();
                }
                else
                {
                    CurrentErrorMessage = $"Add Failed...{result.StatusCode}";
                    return RedirectToPage();
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (Customer != null)
            {
                HttpResponseMessage result = await _client.DeleteCustomerAsync(Customer.Id);

                if (result.IsSuccessStatusCode)
                {
                    CurrentErrorMessage = string.Empty;
                    return RedirectToPage();
                }
                else
                {
                    CurrentErrorMessage = $"Delete Failed...{result.StatusCode}";
                    return RedirectToPage();
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (Customer != null)
            {
                // NOTE:
                // Tried adding in validation for the contact number and email address however
                // I couldn't get it to retain the values for the message and the list of
                // customers meaning I would either get the error message to appear and have no
                // customers or get the customers to appear but have no error message.
                // It will now instead just reload the page if the user enters incorrect values.

                //string validateMessage = ValidateCustomer();
                //if (!string.IsNullOrWhiteSpace(validateMessage))
                //{
                //    CurrentErrorMessage = $"Update Failed...{validateMessage}";
                //    return RedirectToPage();
                //}

                HttpResponseMessage result = await _client.UpdateCustomerAsync(Customer);

                if (result.IsSuccessStatusCode)
                {
                    CurrentErrorMessage = string.Empty;
                    return RedirectToPage();
                }
                else
                {
                    CurrentErrorMessage = $"Update Failed...{result.StatusCode}";
                    return RedirectToPage();
                }
            }

            return RedirectToPage();
        }

        /// <summary>
        /// Checks if the Customer contains valid values.
        /// </summary>
        /// <returns>If validation failed, a message detailing why the validation failed will be returned and otherwise, an empty string.</returns>
        private string ValidateCustomer()
        {
            if (!string.IsNullOrWhiteSpace(Customer?.ContactNo) && !Regex.IsMatch(Customer?.ContactNo!, TelephoneNoRegex))
            {
                return "Invalid Contact No!";
            }

            if (!string.IsNullOrWhiteSpace(Customer?.Email) && !Regex.IsMatch(Customer?.Email!, EmailRegex))
            {
                return "Invalid Email Address!"; 
            }

            return string.Empty;
        }
    }
}
