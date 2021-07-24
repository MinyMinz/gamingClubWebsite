namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class priceToPPU : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markets", "PPU", c => c.Double(nullable: false));
            DropColumn("dbo.Markets", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Markets", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Markets", "PPU");
        }
    }
}
