namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Marketedit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markets", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Markets", "UserId");
            AddForeignKey("dbo.Markets", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Markets", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Markets", new[] { "UserId" });
            DropColumn("dbo.Markets", "UserId");
        }
    }
}
