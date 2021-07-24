namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class marketandgamesGUID_Hotfix1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Markets", "GameGUID", "dbo.Games");
            DropPrimaryKey("dbo.Games");
            DropPrimaryKey("dbo.Markets");
            AlterColumn("dbo.Games", "GameGUID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Markets", "SalesGUID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Games", "GameGUID");
            AddPrimaryKey("dbo.Markets", "SalesGUID");
            AddForeignKey("dbo.Markets", "GameGUID", "dbo.Games", "GameGUID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Markets", "GameGUID", "dbo.Games");
            DropPrimaryKey("dbo.Markets");
            DropPrimaryKey("dbo.Games");
            AlterColumn("dbo.Markets", "SalesGUID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Games", "GameGUID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Markets", "SalesGUID");
            AddPrimaryKey("dbo.Games", "GameGUID");
            AddForeignKey("dbo.Markets", "GameGUID", "dbo.Games", "GameGUID");
        }
    }
}
