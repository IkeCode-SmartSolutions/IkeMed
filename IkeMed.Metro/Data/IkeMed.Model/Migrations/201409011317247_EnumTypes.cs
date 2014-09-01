namespace IkeMed.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnumTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "PersonType", c => c.Int(nullable: false));
            AddColumn("dbo.LegalPersons", "PersonType", c => c.Int(nullable: false));
            AddColumn("dbo.NaturalPersons", "PersonType", c => c.Int(nullable: false));
            DropColumn("dbo.Doctors", "Birthdate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Birthdate", c => c.DateTime(nullable: false));
            DropColumn("dbo.NaturalPersons", "PersonType");
            DropColumn("dbo.LegalPersons", "PersonType");
            DropColumn("dbo.Doctors", "PersonType");
        }
    }
}
