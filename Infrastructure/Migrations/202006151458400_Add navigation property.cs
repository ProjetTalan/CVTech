namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addnavigationproperty : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ProfileTechnologies", "TechLevelId");
            AddForeignKey("dbo.ProfileTechnologies", "TechLevelId", "dbo.TechLevels", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileTechnologies", "TechLevelId", "dbo.TechLevels");
            DropIndex("dbo.ProfileTechnologies", new[] { "TechLevelId" });
        }
    }
}
