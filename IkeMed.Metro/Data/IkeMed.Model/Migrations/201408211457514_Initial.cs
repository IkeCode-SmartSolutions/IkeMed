namespace IkeMed.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        DateIns = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Email, unique: true, name: "IX_PEOPLE_EMAIL");

            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdmissionDate = c.DateTime(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        ProfileImage = c.String(),
                        DateIns = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.PersonId);

            CreateTable(
                "dbo.LegalPersons",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    SocialName = c.String(nullable: false, maxLength: 250),
                    CompanyName = c.String(nullable: false, maxLength: 250),
                    ProfileImage = c.String(),
                    DateIns = c.DateTime(nullable: false),
                    LastUpdate = c.DateTime(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    PersonId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.PersonId);

            CreateTable(
                "dbo.NaturalPersons",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Gender = c.Int(nullable: false),
                    Birthdate = c.DateTime(nullable: false),
                    ProfileImage = c.String(),
                    DateIns = c.DateTime(nullable: false),
                    LastUpdate = c.DateTime(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    PersonId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PersonId)
                .Index(t => t.PersonId);

            CreateTable(
                "dbo.DocumentTypes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    DateIns = c.DateTime(nullable: false),
                    LastUpdate = c.DateTime(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 30),
                        DateIns = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DocumentTypeId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DocumentTypes", t => t.DocumentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.DocumentTypeId)
                .Index(t => t.PersonId);

            CreateTable(
                "dbo.Addresses",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Street = c.String(nullable: false, maxLength: 250),
                    Number = c.String(nullable: false, maxLength: 10),
                    Complement = c.String(maxLength: 50),
                    Neighborhood = c.String(nullable: false, maxLength: 100),
                    ZipCode = c.String(nullable: false, maxLength: 20),
                    City = c.String(nullable: false, maxLength: 150),
                    State = c.String(nullable: false, maxLength: 3),
                    AddressType = c.Int(nullable: false),
                    DateIns = c.DateTime(nullable: false),
                    LastUpdate = c.DateTime(nullable: false),
                    IsActive = c.Boolean(nullable: false),
                    PersonId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);

            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 30),
                        PhoneType = c.Int(nullable: false),
                        DateIns = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.Number, unique: true, name: "IX_PHONE_NUMBER")
                .Index(t => t.PersonId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Phones", "PersonId", "dbo.People");
            DropForeignKey("dbo.NaturalPersons", "PersonId", "dbo.People");
            DropForeignKey("dbo.LegalPersons", "PersonId", "dbo.People");
            DropForeignKey("dbo.Documents", "PersonId", "dbo.People");
            DropForeignKey("dbo.Documents", "DocumentTypeId", "dbo.DocumentTypes");
            DropForeignKey("dbo.Doctors", "PersonId", "dbo.People");
            DropForeignKey("dbo.Addresses", "PersonId", "dbo.People");
            DropIndex("dbo.Phones", new[] { "PersonId" });
            DropIndex("dbo.Phones", "IX_PHONE_NUMBER");
            DropIndex("dbo.NaturalPersons", new[] { "PersonId" });
            DropIndex("dbo.LegalPersons", new[] { "PersonId" });
            DropIndex("dbo.Documents", new[] { "PersonId" });
            DropIndex("dbo.Documents", new[] { "DocumentTypeId" });
            DropIndex("dbo.Doctors", new[] { "PersonId" });
            DropIndex("dbo.People", "IX_PEOPLE_EMAIL");
            DropIndex("dbo.Addresses", new[] { "PersonId" });
            DropTable("dbo.Phones");
            DropTable("dbo.NaturalPersons");
            DropTable("dbo.LegalPersons");
            DropTable("dbo.DocumentTypes");
            DropTable("dbo.Documents");
            DropTable("dbo.Doctors");
            DropTable("dbo.People");
            DropTable("dbo.Addresses");
        }
    }
}
