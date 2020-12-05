namespace Pezeshk.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedFAQTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FAQs",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Question = c.String(),
                    Answer = c.String(),
                })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("dbo.FAQs");
        }
    }
}
