/* =============================================
   View: SYSCOLUMNSVIEW
   Corresponding SQLBase System Catalog SYSCOLUMNS

   Description:
	 Lists each column of every table.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSCOLUMNSVIEW]'))
DROP VIEW [dbo].[SYSCOLUMNSVIEW]
GO

CREATE VIEW [dbo].[SYSCOLUMNSVIEW]
AS
SELECT 
UPPER(c.column_name collate Latin1_General_CS_AS) AS NAME,
UPPER(c.table_name collate Latin1_General_CS_AS) AS TBNAME,
UPPER(c.table_schema collate Latin1_General_CS_AS) AS TBCREATOR,
c.ordinal_position AS COLNO,
UPPER(c.data_type collate Latin1_General_CS_AS) AS COLTYPE,
ISNULL(c.character_maximum_length, 0) +
ISNULL(c.numeric_precision, 0) + 
ISNULL(c.datetime_precision, 0) AS LENGTH,
c.numeric_scale AS SCALE,
(SUBSTRING(c.is_nullable, 1, 1) collate Latin1_General_CS_AS) AS NULLS
FROM INFORMATION_SCHEMA.COLUMNS c
GO

/* =============================================
   View: SYSVIEWS
   Corresponding SQLBase System Catalog SYSVIEWS

   Description:
	 Lists the text of each view.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSVIEWS]'))
DROP VIEW [dbo].[SYSVIEWS]
GO
CREATE VIEW [dbo].[SYSVIEWS]
AS
SELECT 
UPPER(table_name collate Latin1_General_CS_AS) AS NAME,
UPPER(table_schema collate Latin1_General_CS_AS) AS CREATOR,
UPPER(view_definition collate Latin1_General_CS_AS) AS TEXT
FROM INFORMATION_SCHEMA.VIEWS
GO

/* =============================================
   View: SYSTABLES
   Corresponding SQLBase System Catalog SYSTABLES

   Description:
	 Lists each table or view..
   ============================================= */

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSTABLES]'))
DROP VIEW [dbo].[SYSTABLES]
GO
CREATE VIEW [dbo].[SYSTABLES]
AS
SELECT
UPPER(t.table_schema collate Latin1_General_CS_AS) AS CREATOR, 
UPPER(c.table_name collate Latin1_General_CS_AS) AS NAME,
COUNT(c.table_name collate Latin1_General_CS_AS) AS COLCOUNT,
CASE UPPER(t.table_type) 
	WHEN 'BASE TABLE' THEN 'T' 
	WHEN 'VIEW' THEN 'V' 
END AS 'TYPE'
FROM INFORMATION_SCHEMA.TABLES t 
JOIN INFORMATION_SCHEMA.COLUMNS c ON t.table_name = c.table_name
GROUP BY c.table_name, t.table_schema, t.table_type
GO

/* =============================================
   View: SYSPKCONSTRAINTS
   Corresponding SQLBase System Catalog SYSPKCONSTRAINTS

   Description:
	 Lists each primary key constraint.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSPKCONSTRAINTS]'))
DROP VIEW [dbo].[SYSPKCONSTRAINTS]
GO
CREATE VIEW [dbo].[SYSPKCONSTRAINTS]
AS
SELECT
UPPER(t.table_schema collate Latin1_General_CS_AS) AS CREATOR, 
UPPER(t.table_name collate Latin1_General_CS_AS) AS NAME,
c.ordinal_position AS PKCOLSEQNUM,
UPPER(c.column_name collate Latin1_General_CS_AS) AS COLNAME
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS t INNER JOIN
INFORMATION_SCHEMA.KEY_COLUMN_USAGE  c ON
t.constraint_name = c.constraint_name 
WHERE t.constraint_type = 'PRIMARY KEY'
GO

/* =============================================
   View: SYSFKCONSTRAINTS
   Corresponding SQLBase System Catalog SYSFKCONSTRAINTS

   Description:
	 Lists each foreign key constraint.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSFKCONSTRAINTS]'))
DROP VIEW [dbo].[SYSFKCONSTRAINTS]
GO
CREATE VIEW [dbo].[SYSFKCONSTRAINTS]
AS
SELECT
UPPER(t.constraint_schema collate Latin1_General_CS_AS) AS CREATOR, 
UPPER(OBJECT_NAME(f.parent_object_id) collate Latin1_General_CS_AS) AS [NAME],
UPPER(t.constraint_name collate Latin1_General_CS_AS) AS [CONSTRAINT],
f.constraint_column_id AS FKCOLSEQNUM,
UPPER(c1.name collate Latin1_General_CS_AS) AS REFSCOLUMN,
UPPER(OBJECT_NAME(f.referenced_object_id) collate Latin1_General_CS_AS) AS REFDTBNAME,
UPPER(c2.name collate Latin1_General_CS_AS) AS REFDCOLUMN
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS t 
JOIN sys.foreign_key_columns f ON
t.constraint_name = OBJECT_NAME(f.constraint_object_id) 
JOIN sys.columns c1 ON
f.parent_object_id = c1.object_id AND f.parent_column_id = c1.column_id
JOIN sys.columns c2 ON
f.referenced_object_id = c2.object_id AND f.referenced_column_id = c2.column_id
GO

/* =============================================
   View: SYSUSERAUTH
   Corresponding SQLBase System Catalog SYSUSERAUTH

   Description:
	 Lists each user's database authority level.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSUSERAUTH]'))
DROP VIEW [dbo].[SYSUSERAUTH]
GO
CREATE VIEW [dbo].[SYSUSERAUTH]
AS
SELECT 
UPPER(name collate Latin1_General_CS_AS) AS NAME
FROM sys.database_principals 
WHERE type = 'S'
GO

/* =============================================
   View: SYSKEYS
   Corresponding SQLBase System Catalog SYSKEYES

   Description:
	 Lists each column in every index.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSKEYS]'))
DROP VIEW [dbo].[SYSKEYS]
GO
CREATE VIEW [dbo].[SYSKEYS]
AS
SELECT 
UPPER(name collate Latin1_General_CS_AS) AS IXNAME, 
UPPER(c1.column_name collate Latin1_General_CS_AS) AS COLNAME,
column_id AS COLNO,
index_column_id AS COLSEQ,
CASE WHEN is_descending_key = 0 THEN 'A' ELSE 'D' END AS ORDERING
FROM sys.indexes i
JOIN sys.index_columns c
ON i.object_id = c.object_id AND i.index_id = c.index_id
JOIN INFORMATION_SCHEMA.COLUMNS c1
ON object_id(table_name) = c.object_id AND c1.ordinal_position = c.column_id
WHERE i.type <> 0
GO

--/* =============================================
--   View: SYSINDEXES
--   Corresponding SQLBase System Catalog SYSINDEXES

--   Description:
--	 Lists each table's indexes.
--   ============================================= */

--IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSINDEXES]'))
--DROP VIEW [dbo].[SYSINDEXES]
--GO
--CREATE VIEW [dbo].[SYSINDEXES]
--AS
--SELECT 
--UPPER(i.name collate Latin1_General_CS_AS) AS NAME, 
--UPPER(OBJECT_NAME(i.object_id) collate Latin1_General_CS_AS) AS TBNAME,
--CASE WHEN is_unique = 1 THEN 'U' ELSE 'D' END AS UNIQUERULE,
--COUNT(c.object_id) AS COLCOUNT,
--SCHEMA_NAME(o.schema_id) collate Latin1_General_CS_AS AS CREATOR,
--CASE WHEN i.fill_factor = 0 THEN 0 ELSE 100-i.fill_factor END AS PERCENTFREE
--FROM sys.indexes i
--JOIN sys.index_columns c
--ON i.object_id = c.object_id AND i.index_id = c.index_id
--JOIN sys.objects o ON o.object_id = i.object_id
--WHERE i.type <> 0
--GROUP BY i.name, i.object_id, is_unique, c.object_id, o.schema_id, i.fill_factor
--GO

/* =============================================
   View: SYSTABCONSTRAINTS
   Corresponding SQLBase System Catalog SYSTABCONSTRAINTS

   Description:
	 Lists each table constraint.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSTABCONSTRAINTS]'))
DROP VIEW [dbo].[SYSTABCONSTRAINTS]
GO
CREATE VIEW [dbo].[SYSTABCONSTRAINTS]
AS
SELECT
UPPER(constraint_schema collate Latin1_General_CS_AS) AS CREATOR, 
UPPER(table_name collate Latin1_General_CS_AS) AS [NAME],
UPPER(constraint_name collate Latin1_General_CS_AS) AS [CONSTRAINT],
SUBSTRING(constraint_type, 1, 1) collate Latin1_General_CS_AS AS [TYPE],
CASE delete_referential_action WHEN 0 THEN 'R' WHEN 1 THEN 'C' WHEN 2 THEN 'N' END AS DELETERULE
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS t LEFT OUTER JOIN sys.foreign_keys f
ON t.constraint_name = f.name
WHERE CONSTRAINT_TYPE IN ('PRIMARY KEY', 'FOREIGN KEY')
GO

/* =============================================
   View: SYSTABAUTH
   Corresponding SQLBase System Catalog SYSTABAUTH

   Description:
	 Lists each user's table privileges.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[SYSTABAUTH]'))
DROP VIEW [dbo].[SYSTABAUTH]
GO
CREATE VIEW [dbo].[SYSTABAUTH]
AS
SELECT DISTINCT 
A.GRANTEE collate Latin1_General_CS_AS AS GRANTEE,
A.TABLE_NAME collate Latin1_General_CS_AS AS TTNAME,
CASE WHEN D.PRIVILEGE_TYPE = 'DELETE' THEN 'Y' ELSE '' END AS DELETEAUTH,
CASE WHEN I.PRIVILEGE_TYPE = 'INSERT' THEN 'Y' ELSE '' END AS INSERTAUTH,
CASE WHEN S.PRIVILEGE_TYPE = 'SELECT' THEN 'Y' ELSE '' END AS SELECTAUTH,
CASE WHEN U.PRIVILEGE_TYPE = 'UPDATE' THEN 'Y' ELSE '' END AS UPDATEAUTH
FROM INFORMATION_SCHEMA.TABLE_PRIVILEGES A
LEFT JOIN INFORMATION_SCHEMA.TABLE_PRIVILEGES D ON A.TABLE_NAME = D.TABLE_NAME AND D.PRIVILEGE_TYPE = 'DELETE' 
LEFT JOIN INFORMATION_SCHEMA.TABLE_PRIVILEGES I ON A.TABLE_NAME = I.TABLE_NAME AND I.PRIVILEGE_TYPE = 'INSERT'
LEFT JOIN INFORMATION_SCHEMA.TABLE_PRIVILEGES S ON A.TABLE_NAME = S.TABLE_NAME AND S.PRIVILEGE_TYPE = 'SELECT'
LEFT JOIN INFORMATION_SCHEMA.TABLE_PRIVILEGES U ON A.TABLE_NAME = U.TABLE_NAME AND U.PRIVILEGE_TYPE = 'UPDATE'
GO