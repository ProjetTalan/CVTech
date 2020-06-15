namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedchange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExperienceDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProExpId = c.Int(nullable: false),
                        PositionId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProExps", "Company_Id", c => c.Int());
            CreateIndex("dbo.ProExps", "CityId");
            CreateIndex("dbo.ProExps", "Company_Id");
            AddForeignKey("dbo.ProExps", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProExps", "Company_Id", "dbo.Companies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProExps", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.ProExps", "CityId", "dbo.Cities");
            DropForeignKey("dbo.ExperienceDescriptions", "PositionId", "dbo.Positions");
            DropIndex("dbo.ProExps", new[] { "Company_Id" });
            DropIndex("dbo.ProExps", new[] { "CityId" });
            DropIndex("dbo.ExperienceDescriptions", new[] { "PositionId" });
            DropColumn("dbo.ProExps", "Company_Id");
            DropTable("dbo.Positions");
            DropTable("dbo.ExperienceDescriptions");
        }
    }
}
