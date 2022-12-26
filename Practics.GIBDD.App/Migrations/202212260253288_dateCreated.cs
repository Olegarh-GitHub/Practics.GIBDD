namespace Practics.GIBDD.App.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateCreated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "DateCreated", c => c.DateTime());
        }
    }
}
