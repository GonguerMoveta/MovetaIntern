IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DropPrimaryKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DropPrimaryKey]
GO

CREATE PROCEDURE [dbo].[DropPrimaryKey]
    @tableName varchar(255)
AS
    DECLARE @pkName varchar(255)

    SET @pkName= (
        SELECT [name] FROM sysobjects
            WHERE [xtype] = 'PK'
            AND [parent_obj] = OBJECT_ID(N'[dbo].['+@tableName+N']')
    )
    DECLARE @dropSql varchar(4000)

    SET @dropSql=
        'ALTER TABLE [dbo].['+@tableName+']
            DROP CONSTRAINT ['+@PkName+']'
    EXEC(@dropSql)
GO