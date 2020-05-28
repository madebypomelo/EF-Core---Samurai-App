using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class NewSprocs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.SamuraiWhoSaidAWord
                    @text VARCHAR(20)
                    AS
                    SELECT  Samurais.ID, Samurais.Name, Samurais.ClanID
                    FROM Samurais INNER JOIN
                    Quotes ON Samurais.ID = Quotes.SamuraiID
                    WHERE (Quotes.Text LIKE '%'+@text+'%')");

            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.DeleteQuotesForSamurai
                    @samuraiID int
                    as
                    DELETE FROM Quotes
                    WHERE Quotes.SamuraiID = @samuraiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
