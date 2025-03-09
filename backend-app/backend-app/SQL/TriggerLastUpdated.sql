USE MarketPlace;
GO
DECLARE @tableName NVARCHAR(128);
DECLARE @sql NVARCHAR(MAX);

DECLARE table_cursor CURSOR FOR
SELECT name
FROM sys.tables
WHERE type = 'U'; -- Only user tables

OPEN table_cursor;

FETCH NEXT FROM table_cursor INTO @tableName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @sql = '
    CREATE TRIGGER trg_Update_' + @tableName + '
    ON ' + @tableName + '
    AFTER UPDATE
    AS
    BEGIN
        -- Update the last_updated field for the modified row
        UPDATE ' + @tableName + '
        SET last_updated = GETDATE()
        WHERE id IN (SELECT id FROM inserted);
    END';
    
    -- Check if trigger already exists, then create it
    EXEC sp_executesql @sql;
    
    FETCH NEXT FROM table_cursor INTO @tableName;
END;

CLOSE table_cursor;
DEALLOCATE table_cursor;