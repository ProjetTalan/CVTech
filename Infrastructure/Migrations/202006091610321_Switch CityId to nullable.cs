namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwitchCityIdtonullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Profiles", "CityId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Profiles", "CityId", c => c.Int(nullable: false));
        }
    }
}
