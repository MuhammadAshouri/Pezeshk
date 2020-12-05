namespace Pezeshk.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedShowInSiteToComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ShowInSite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "ShowInSite");
        }
    }
}
