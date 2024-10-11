﻿using Microsoft.EntityFrameworkCore;

namespace CustomersAPI.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public DbSet<Customer> CustomerCollection { get; set; } = null!;
    }
}
