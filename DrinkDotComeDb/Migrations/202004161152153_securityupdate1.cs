namespace DrinkDotComDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class securityupdate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Country", c => c.String());
            AlterColumn("dbo.Users", "Address", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Address", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "Country", c => c.String(nullable: false));
        }
    }
}
