namespace DrinkDotComDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ConfigurationsSettings", newName: "Configurations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Configurations", newName: "ConfigurationsSettings");
        }
    }
}
