namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CivilStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProExps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        CompaniesId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        Nationality = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(),
                        Zip = c.String(),
                        CityId = c.Int(),
                        PhoneNumber = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                        PhotoUrl = c.String(),
                        Technology_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Technologies", t => t.Technology_Id)
                .Index(t => t.Technology_Id);
            
            CreateTable(
                "dbo.TechnologyProExps",
                c => new
                    {
                        Technology_Id = c.Int(nullable: false),
                        ProExp_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Technology_Id, t.ProExp_Id })
                .ForeignKey("dbo.Technologies", t => t.Technology_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProExps", t => t.ProExp_Id, cascadeDelete: true)
                .Index(t => t.Technology_Id)
                .Index(t => t.ProExp_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profiles", "Technology_Id", "dbo.Technologies");
            DropForeignKey("dbo.ProExps", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.TechnologyProExps", "ProExp_Id", "dbo.ProExps");
            DropForeignKey("dbo.TechnologyProExps", "Technology_Id", "dbo.Technologies");
            DropIndex("dbo.TechnologyProExps", new[] { "ProExp_Id" });
            DropIndex("dbo.TechnologyProExps", new[] { "Technology_Id" });
            DropIndex("dbo.Profiles", new[] { "Technology_Id" });
            DropIndex("dbo.ProExps", new[] { "ProfileId" });
            DropTable("dbo.TechnologyProExps");
            DropTable("dbo.Profiles");
            DropTable("dbo.Technologies");
            DropTable("dbo.ProExps");
            DropTable("dbo.Countries");
            DropTable("dbo.Companies");
            DropTable("dbo.CivilStates");
            DropTable("dbo.Cities");
        }
    }
}
