namespace DrinkDotComDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FullName", c => c.String());
            AlterColumn("dbo.Users", "Address", c => c.String());
            AlterColumn("dbo.Users", "ZipCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "ZipCode", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Users", "Address", c => c.String(maxLength: 30));
            AlterColumn("dbo.Users", "FullName", c => c.String(nullable: false));
        }
    }
}
