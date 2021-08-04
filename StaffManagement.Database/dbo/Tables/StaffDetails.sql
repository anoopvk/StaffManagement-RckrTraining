CREATE TABLE [dbo].[StaffDetails] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NULL,
    [StaffTypeId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [StaffTypeId] FOREIGN KEY ([StaffTypeId]) REFERENCES [dbo].[StaffTypes] ([Id])
);



