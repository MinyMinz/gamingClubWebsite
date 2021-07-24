namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarketGUIDAndNewGamesTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Markets");
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameGUID = c.String(nullable: false, maxLength: 128),
                        GameName = c.String(),
                    })
                .PrimaryKey(t => t.GameGUID);
            
            AddColumn("dbo.Markets", "SalesGUID", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Markets", "GameGUID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Markets", "SalesGUID");
            CreateIndex("dbo.Markets", "GameGUID");
            AddForeignKey("dbo.Markets", "GameGUID", "dbo.Games", "GameGUID");
            DropColumn("dbo.Markets", "ItemID");
            DropColumn("dbo.Markets", "Game");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Markets", "Game", c => c.String());
            AddColumn("dbo.Markets", "ItemID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Markets", "GameGUID", "dbo.Games");
            DropIndex("dbo.Markets", new[] { "GameGUID" });
            DropPrimaryKey("dbo.Markets");
            DropColumn("dbo.Markets", "GameGUID");
            DropColumn("dbo.Markets", "SalesGUID");
            DropTable("dbo.Games");
            AddPrimaryKey("dbo.Markets", "ItemID");
        }
    }
}
