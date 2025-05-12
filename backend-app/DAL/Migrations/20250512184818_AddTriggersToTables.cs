using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggersToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               USE MarketPlace; 

                DECLARE @tableName NVARCHAR(128);
                DECLARE @sql NVARCHAR(MAX);
                DECLARE table_cursor CURSOR FOR
                    -- Exclure la table des migrations __EFMigrationsHistory
                    SELECT name
                    FROM sys.tables
                    WHERE type = 'U' AND name != '__EFMigrationsHistory'; -- Filtrage de la table de migration

                OPEN table_cursor;
                FETCH NEXT FROM table_cursor INTO @tableName;

                WHILE @@FETCH_STATUS = 0
                BEGIN
                    -- Créer le script du trigger pour chaque table utilisateur
                    SET @sql = '
                        IF NOT EXISTS (
                            SELECT * FROM sys.triggers 
                            WHERE name = ''trg_Update_'' + @tableName
                        )
                        BEGIN
                            EXEC(''CREATE TRIGGER trg_Update_'' + ''' + @tableName + ''' + ''
                            ON ' + ''' + @tableName + ''' + '
                            AFTER UPDATE
                            AS
                            BEGIN
                                -- Mise à jour de la colonne last_updated
                                UPDATE ' + ''' + @tableName + ''' + '
                                SET last_updated = GETDATE()
                                WHERE id IN (SELECT id FROM inserted);
                            END'');
                        END';
    
                    -- Exécution du SQL dynamique
                    EXEC sp_executesql @sql, N'@tableName NVARCHAR(128)', @tableName;

                    FETCH NEXT FROM table_cursor INTO @tableName;
                END;

                CLOSE table_cursor;
                DEALLOCATE table_cursor;

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
