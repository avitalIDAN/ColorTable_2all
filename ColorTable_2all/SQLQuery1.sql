--for create TABLE Colors  in Database = ColorsDB   

CREATE TABLE Colors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ColorName NVARCHAR(50) NOT NULL,
    Price FLOAT NOT NULL,
    OrderIndex INT NOT NULL,
    InStock BIT NOT NULL,
    ColorCode NVARCHAR(7) NOT NULL
);