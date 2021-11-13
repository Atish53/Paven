namespace Paven.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelFee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deliveries", "DeliveryFee", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "DeliveryFee");
        }
    }
}
