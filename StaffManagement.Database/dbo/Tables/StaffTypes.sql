CREATE TABLE [dbo].[StaffTypes] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [StaffType] VARCHAR (255) NOT NULL,
    CONSTRAINT [PK_StaffTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

