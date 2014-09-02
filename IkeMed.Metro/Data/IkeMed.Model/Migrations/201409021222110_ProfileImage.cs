namespace IkeMed.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NaturalPersons", "ProfileImageUrl", c => c.String());
            DropColumn("dbo.NaturalPersons", "ProfileImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NaturalPersons", "ProfileImage", c => c.String());
            DropColumn("dbo.NaturalPersons", "ProfileImageUrl");
        }
    }
}
