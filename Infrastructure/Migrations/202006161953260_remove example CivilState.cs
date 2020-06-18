namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeexampleCivilState : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CivilStates");
        }
        
        public override void Down()
        {
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
            
        }
    }
}
