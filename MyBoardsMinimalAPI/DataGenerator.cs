﻿using Bogus;
using MyBoardsMinimalAPI.Entities;

namespace MyBoardsMinimalAPI
{
    public class DataGenerator
    {
        public static void Seed(MyBoardsContext context)
        {
            var locale = "pl";
            var addressGenerator = new Faker<Address>(locale)
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
                .RuleFor(a => a.Street, f => f.Address.StreetName());

            //Address address = addressGenerator.Generate();

            var userGenerator = new Faker<User>(locale)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.FullName, f => f.Person.FullName)
                .RuleFor(u => u.Address, addressGenerator.Generate());

            var users = userGenerator.Generate(100);

            context.AddRange(users);
            //context.SaveChanges();  
        }
    }
}
