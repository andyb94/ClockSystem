CREATE TABLE [dbo].[ClockRecord] (
    [ClockRecordId] INT      NOT NULL,
    [ClockTime]     DATETIME NOT NULL,
    [ClockedIn]     BIT      NOT NULL,
    [ClockTypeId]   INT      NOT NULL,
    [UserId]        INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([ClockRecordId] ASC),
    FOREIGN KEY ([ClockTypeId]) REFERENCES [dbo].[ClockType] ([ClockTypeId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);

