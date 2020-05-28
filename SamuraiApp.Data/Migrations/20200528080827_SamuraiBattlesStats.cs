using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class SamuraiBattlesStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE FUNCTION[dbo].[EarliestBattleFoughtBySamurai](@samuraiID int)
                    RETURNS char(30) AS
                    BEGIN
                        DECLARE @ret char(30)
                        SELECT TOP 1 @ret = Name
                        FROM Battles
                        WHERE Battles.ID IN (SELECT BattleID from SamuraiBattle WHERE SamuraiID = @samuraiID)
                        ORDER BY StartDate
                        RETURN @ret
                    END");
            migrationBuilder.Sql(
                @"CREATE OR ALTER VIEW dbo.SamuraiBattleStats
                    AS
                    SELECT dbo.SamuraiBattle.SamuraiID, dbo.Samurais.Name,
                    COUNT(dbo.SamuraiBattle.BattleID) AS NumberOfBattles,
	                    dbo.EarliestBattleFoughtBySamurai(MIN(dbo.Samurais.ID)) AS EarliestBattle
                    FROM dbo.SamuraiBattle INNER JOIN
	                    dbo.Samurais ON dbo.SamuraiBattle.SamuraiID = dbo.Samurais.ID
                    GROUP BY dbo.Samurais.Name, dbo.SamuraiBattle.SamuraiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.SamuraiBattleStats");
            migrationBuilder.Sql("DROP FUNCTION dbo.EarliestBattleFoughtBySamurai");
        }
    }
}
