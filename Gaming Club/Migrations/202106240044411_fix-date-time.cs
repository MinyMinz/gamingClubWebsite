namespace Gaming_Club.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixdatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Markets", "ListedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Markets", "ListedDate", c => c.DateTime(nullable: false));
        }
    }
}
