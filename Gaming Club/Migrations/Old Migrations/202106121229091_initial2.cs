namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Markets",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Item_Name = c.String(),
                        Game = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
                


        }
        
        public override void Down()
        {
            DropTable("dbo.Markets");
        }
    }
}
