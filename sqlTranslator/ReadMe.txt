
---------------Content of Master solution----------------------


1. fecher.Common 								- common library used by SqlTranslator and SpTranslator;

2. fecher.MSSQL.DataProvider		- custom data provider for SqlServer; it calls the SqlTranslator before executing a statement;

3. fecher.OracleDB.DataProvider - custom data provider for Oracle; it calls the SqlTranslator before executing a statement;

4. fecher.SpTranslator					- translator for stored procedures; it's used in the process of porting a database;

5. fecher.SqlTranslator					- translator for sql statements from SqlBase to SqlServer or Oracle;

6. fecher.sqlTranslatorTest			- small application for testing the SqlTranslator and SpTranslator;

7. fecher.sqlWrapper						- C++ project for creating a wrapper for SqlTranslator so that it can be used from Gupta applications.