CREATE TABLE [dbo].[Users] (
    [UserId]     INT          NOT NULL,
    [FirstName]  VARCHAR (20) NOT NULL,
    [SecondName] VARCHAR (20) NOT NULL,
    [Email]      VARCHAR (50) NOT NULL,
    [RoleId]     INT          NOT NULL,
    [Password]   VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId])
);

