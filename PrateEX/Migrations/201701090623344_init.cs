namespace PrateEX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Crews",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ShipId = c.Int(nullable: false),
            //            PirateId = c.Int(nullable: false),
            //            Booty = c.Decimal(nullable: false, precision: 18, scale: 0),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Pirates", t => t.PirateId)
            //    .ForeignKey("dbo.Ships", t => t.ShipId)
            //    .Index(t => t.ShipId)
            //    .Index(t => t.PirateId);
            
            //CreateTable(
            //    "dbo.Pirates",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Ships",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 20),
            //            Type = c.String(maxLength: 10),
            //            Tonnage = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Crews", "ShipId", "dbo.Ships");
            DropForeignKey("dbo.Crews", "PirateId", "dbo.Pirates");
            DropIndex("dbo.Crews", new[] { "PirateId" });
            DropIndex("dbo.Crews", new[] { "ShipId" });
            DropTable("dbo.Ships");
            DropTable("dbo.Pirates");
            DropTable("dbo.Crews");
        }
    }
}
