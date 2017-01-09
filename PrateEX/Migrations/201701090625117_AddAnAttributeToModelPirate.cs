namespace PrateEX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnAttributeToModelPirate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pirates", "MyHabit", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pirates", "MyHabit");
        }
    }
}
