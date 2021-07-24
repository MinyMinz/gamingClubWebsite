namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class justworkplease : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameGUID = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        GameName = c.String(),
                    })
                .PrimaryKey(t => t.GameGUID);
            
            CreateTable(
                "dbo.Markets",
                c => new
                    {
                        SalesGUID = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        ItemName = c.String(),
                        GameGUID = c.Guid(nullable: false),
                        ItemDescription = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SalesGUID)
                .ForeignKey("dbo.Games", t => t.GameGUID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.GameGUID)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Markets", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Markets", "GameGUID", "dbo.Games");
            DropIndex("dbo.Markets", new[] { "UserId" });
            DropIndex("dbo.Markets", new[] { "GameGUID" });
            DropTable("dbo.Markets");
            DropTable("dbo.Games");
        }
    }
}
