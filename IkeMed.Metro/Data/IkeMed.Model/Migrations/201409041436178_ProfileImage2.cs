namespace IkeMed.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileImage2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "ProfileImageUrl", c => c.String());
            AddColumn("dbo.LegalPersons", "ProfileImageUrl", c => c.String());
            DropColumn("dbo.Doctors", "ProfileImage");
            DropColumn("dbo.LegalPersons", "ProfileImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LegalPersons", "ProfileImage", c => c.String());
            AddColumn("dbo.Doctors", "ProfileImage", c => c.String());
            DropColumn("dbo.LegalPersons", "ProfileImageUrl");
            DropColumn("dbo.Doctors", "ProfileImageUrl");
        }
    }
}
