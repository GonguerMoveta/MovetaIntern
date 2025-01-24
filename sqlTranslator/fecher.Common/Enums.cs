using System;

namespace fecher.Common
{
    public enum Tokens
    {
        Keyword = 1,	
        SysKeyword = 2,	
        DataTypeKeyword = 3,

        MathOperator = 10, // +, - , * , /
        RelatOperator = 11, // <, >, <=, >=, =, !=, <>
        BoolOperator = 12, // |, &
        LeftParant = 13, // (
        RightParant = 14, // )
        Concatenate = 15, // ||
        JoinOperator = 16, // (+)
        LeftBracket = 17,
        RightBracket = 18,

        NumericConst = 20,
        StringConst = 21,
        DatetimeConst = 22,

        Identifier = 30,
        BindVariable = 31,
        Function = 32,
        UserDefinedFunction = 33,

        Comma = 40,
        Backslash = 50, // used to continue the statement on the next line
        Semicolon = 60, // used to separate INSERT statements
        Colon = 70, // used to declare variables
        Dot = 71,
        Null = 99, //end of string 

        Root = 100,
        Expression = 101,
        Name = 102,
        Column = 103,
        Table = 104,
        Correlation = 105,
        JoinSpecif = 106,
        SearchCond = 107,
        Predicate = 108,
        Select = 109,
        Insert = 110,
        Update = 111,
        Delete = 112,
        Cursor = 113,
        Asterisk = 114,
        Create = 115,
        DataType = 116,
        KeyName = 117,
        Database = 118,
        View = 119,
        Index = 120,
        Row = 121,
        Commit = 122,
        Drop = 123,
        Rollback = 124,
        Savepoint = 125,
        CheckDatabase = 126,
        Alter = 127,
        Revoke = 128,
        Grant = 129,
        StoreCommand = 130,
        Lock = 131,

        Parameter = 200,
        Variable = 201,

        Block = 300,
        Statement = 301,
        Comment = 302,

        StringIndexer = 400
    }

    public enum DatabaseBrand
    {
        SqlBaseOld = 0, //SqlBase versions older than 9.0
        SqlBase = 1,    //SqlBase version 9.0 or newer
        SqlServer = 2,
        Oracle = 3,
        Informix = 4,
        Access = 5
    }

    public enum CursorStatus
    {
        None = 0,
        Declared = 1,
        Opened = 2,
        Deallocated = 3,
        Closed = 4
    }

    public enum BlockRegion
    {
        Unspecified = 0,
        OnProcedureFetch = 1,
        WhenSqlError = 2,
    }

    public enum RowidType
    {
        Timestamp = 0,
        UniqueIdentifier = 1
    }

    public enum ParseErrorAction
    {
        Ignore = 0,
        ReturnSource = 1,
        ThrowException = 2
    }

    public enum Casing
    {
        Proper = 0,
        Lower = 1,
        Upper = 2
    }

    public enum Statement
    {
        None = 0,
        If = 1,
        ElseIf = 2,
        Else = 3,
        While = 4,
        Loop = 5,
        WhenSqlError = 6
    }
}
