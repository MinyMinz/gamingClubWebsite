namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class marketplaceItemDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markets", "ItemName", c => c.String());
            AddColumn("dbo.Markets", "ItemDescription", c => c.String());
            DropColumn("dbo.Markets", "Item_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Markets", "Item_Name", c => c.String());
            DropColumn("dbo.Markets", "ItemDescription");
            DropColumn("dbo.Markets", "ItemName");
        }
    }
}
