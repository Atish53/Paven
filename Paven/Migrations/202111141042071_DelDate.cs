namespace Paven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deliveries", "DeliveryDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "DeliveryDate");
        }
    }
}
