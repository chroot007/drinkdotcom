namespace DrinkDotComDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class securityupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Address", c => c.String( maxLength: 30));
            AlterColumn("dbo.Users", "ZipCode", c => c.String( maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "ZipCode", c => c.String());
            AlterColumn("dbo.Users", "Address", c => c.String());
            AlterColumn("dbo.Users", "Country", c => c.String());
            AlterColumn("dbo.Users", "FullName", c => c.String());
        }
    }
}
