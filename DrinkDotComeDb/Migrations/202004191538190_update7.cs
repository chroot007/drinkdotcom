namespace DrinkDotComDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FullName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Country", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Users", "City", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "Address", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "ZipCode", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Users", "Dateofbirth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Dateofbirth", c => c.DateTime());
            AlterColumn("dbo.Users", "ZipCode", c => c.String());
            AlterColumn("dbo.Users", "Address", c => c.String());
            AlterColumn("dbo.Users", "City", c => c.String());
            AlterColumn("dbo.Users", "Country", c => c.String());
            AlterColumn("dbo.Users", "FullName", c => c.String());
        }
    }
}
