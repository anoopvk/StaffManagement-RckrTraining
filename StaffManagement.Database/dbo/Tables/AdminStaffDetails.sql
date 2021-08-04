CREATE TABLE [dbo].[AdminStaffDetails] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [StaffId] INT           NULL,
    [Section] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([StaffId]) REFERENCES [dbo].[StaffDetails] ([Id])
);

