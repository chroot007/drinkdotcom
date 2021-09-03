namespace DrinkDotComDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class formupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Dateofbirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Dateofbirth", c => c.DateTime(nullable: false));
        }
    }
}
