namespace IkeMed.Model.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<IkeMedContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IkeMedContext context)
        {
            //context.DocumentTypes.AddOrUpdate(
            //    d => d.Name,
            //    new DocumentType()
            //    {
            //        Name = "RG"
            //    },
            //    new DocumentType()
            //    {
            //        Name = "CPF"
            //    });

            var person = new Person
            {
                Name = "Leandro Barral",
                Email = "ikecode@gmail.com",
                Doctor = new Doctor()
                        {
                            AdmissionDate = DateTime.Now.AddYears(-2),
                            Birthdate = new DateTime(1989, 12, 20)
                        },
                LegalPerson = new LegalPerson()
                        {
                            CompanyName = "Barral Development Informatica Ltda ME",
                            SocialName = "IkeCode"
                        },
                NaturalPerson = new NaturalPerson()
                        {
                            Birthdate = new DateTime(1989, 12, 20),
                            Gender = 2
                        },
                Documents = new List<Document>()
                {
                    new Document()
                    {
                        Value = "466853336",
                        DocumentType = new DocumentType()
                            {
                                Name = "RG"
                            }
                    },
                    new Document()
                    {
                        Value = "39100818860",
                        DocumentType = new DocumentType()
                            {
                                Name = "CPF"
                            }
                    }
                },
                Phones = new List<Phone>()
                {
                    new Phone()
                    {
                        Number = "11 988856996",
                        PhoneType = 1
                    }
                },
                Addresses = new List<Address>()
                {
                    new Address()
                    {
                        City = "São Paulo",
                        State = "SP",
                        Street = "Rua do Rocio",
                        Number = "220",
                        Neighborhood = "Vila Olimpia",
                        Complement = "cj 131",
                        ZipCode = "05550-000",
                        AddressType = 1
                    }
                }
            };
            context.People.AddOrUpdate(p => p.Email, person);

            context.SaveChanges();
        }
    }
}
