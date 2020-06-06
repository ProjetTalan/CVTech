namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Profiles", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Profiles", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Profiles", "Password", c => c.String());
            AlterColumn("dbo.Profiles", "Email", c => c.String());
        }
    }
}
