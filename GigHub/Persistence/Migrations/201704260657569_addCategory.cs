namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCategory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Genres", newName: "Categories");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Categories", newName: "Genres");
        }
    }
}
