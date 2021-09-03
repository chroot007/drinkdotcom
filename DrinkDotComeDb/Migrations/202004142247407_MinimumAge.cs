namespace DrinkDotComDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinimumAge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Dateofbirth", c => c.DateTime());
            DropColumn("dbo.Users", "Birthdate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Birthdate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "Dateofbirth");
        }
    }
}
