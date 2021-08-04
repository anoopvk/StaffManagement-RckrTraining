CREATE TABLE [dbo].[TeachingStaffToSubjectMapping] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [TeachingStaffId] INT NULL,
    [SubjectId]       INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([Id]),
    FOREIGN KEY ([TeachingStaffId]) REFERENCES [dbo].[TeachingStaffDetails] ([Id])
);

