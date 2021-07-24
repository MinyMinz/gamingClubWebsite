namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guidedit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Markets", "GameGUID", "dbo.Games");
            DropIndex("dbo.Markets", new[] { "GameGUID" });
            DropPrimaryKey("dbo.Games");
            DropPrimaryKey("dbo.Markets");
            AlterColumn("dbo.Games", "GameGUID", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.Markets", "SalesGUID", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.Markets", "GameGUID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Games", "GameGUID");
            AddPrimaryKey("dbo.Markets", "SalesGUID");
            CreateIndex("dbo.Markets", "GameGUID");
            AddForeignKey("dbo.Markets", "GameGUID", "dbo.Games", "GameGUID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Markets", "GameGUID", "dbo.Games");
            DropIndex("dbo.Markets", new[] { "GameGUID" });
            DropPrimaryKey("dbo.Markets");
            DropPrimaryKey("dbo.Games");
            AlterColumn("dbo.Markets", "GameGUID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Markets", "SalesGUID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Games", "GameGUID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Markets", "SalesGUID");
            AddPrimaryKey("dbo.Games", "GameGUID");
            CreateIndex("dbo.Markets", "GameGUID");
            AddForeignKey("dbo.Markets", "GameGUID", "dbo.Games", "GameGUID");
        }
    }
}
