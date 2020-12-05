namespace PezeshkGit.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EmailAddedToAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Email", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Admins", "Email");
        }
    }
}
