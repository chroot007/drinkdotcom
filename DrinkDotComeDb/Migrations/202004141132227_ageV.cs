namespace DrinkDotComDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ageV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Birthdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Birthdate");
        }
    }
}
