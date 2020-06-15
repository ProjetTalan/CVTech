namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedsometable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Profiles", "Technology_Id", "dbo.Technologies");
            DropIndex("dbo.Profiles", new[] { "Technology_Id" });
            CreateTable(
                "dbo.ProfileTechnologies",
                c => new
                    {
                        ProfileId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                        TechLevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfileId, t.TechnologyId })
                .ForeignKey("dbo.Technologies", t => t.TechnologyId, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.TechnologyId);
            
            CreateTable(
                "dbo.TechLevels",
                c => new
                    {
                        TechLevelId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TechLevelId);
            
            DropColumn("dbo.Profiles", "Technology_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profiles", "Technology_Id", c => c.Int());
            DropForeignKey("dbo.ProfileTechnologies", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ProfileTechnologies", "TechnologyId", "dbo.Technologies");
            DropIndex("dbo.ProfileTechnologies", new[] { "TechnologyId" });
            DropIndex("dbo.ProfileTechnologies", new[] { "ProfileId" });
            DropTable("dbo.TechLevels");
            DropTable("dbo.ProfileTechnologies");
            CreateIndex("dbo.Profiles", "Technology_Id");
            AddForeignKey("dbo.Profiles", "Technology_Id", "dbo.Technologies", "Id");
        }
    }
}
