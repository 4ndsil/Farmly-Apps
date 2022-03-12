﻿namespace FarmlyCore.Application.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public CustomerTypeDto CustomerType { get; set; }
        public string BankGiro { get; set; }
        public string OrgNumber { get; set; }
        public ICollection<CustomerAddressDto> AddressList { get; set; } = Array.Empty<CustomerAddressDto>();
    }

    public enum CustomerTypeDto
    {
        Farmer,
        Restaurant
    }
}
