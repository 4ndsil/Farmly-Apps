﻿namespace FarmlyCore.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }        
        public string? Email { get; set; }
        public string Password { get; set; }
        public int FkCustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}