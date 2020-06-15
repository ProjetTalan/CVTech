namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minorchange : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TechLevels");
            DropColumn("dbo.TechLevels", "TechLevelId");
            AddColumn("dbo.TechLevels", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TechLevels", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TechLevels", "TechLevelId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.TechLevels");
            DropColumn("dbo.TechLevels", "Id");
            AddPrimaryKey("dbo.TechLevels", "TechLevelId");
        }
    }
}
