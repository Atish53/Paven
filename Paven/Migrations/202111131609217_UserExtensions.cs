namespace Paven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserExtensions : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "Points");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Points", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
        }
    }
}
