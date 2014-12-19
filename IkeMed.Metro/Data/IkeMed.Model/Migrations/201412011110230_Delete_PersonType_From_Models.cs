namespace IkeMed.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_PersonType_From_Models : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctors", "PersonType");
            DropColumn("dbo.LegalPersons", "PersonType");
            DropColumn("dbo.NaturalPersons", "PersonType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NaturalPersons", "PersonType", c => c.Int(nullable: false));
            AddColumn("dbo.LegalPersons", "PersonType", c => c.Int(nullable: false));
            AddColumn("dbo.Doctors", "PersonType", c => c.Int(nullable: false));
        }
    }
}
