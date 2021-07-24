namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profilePic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "profilePicture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "profilePicture");
        }
    }
}
