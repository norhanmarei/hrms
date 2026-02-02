CREATE TABLE WorkScheduleDays (
    Id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ScheduleId INT NOT NULL,
    DayOfWeek SMALLINT NOT NULL,   -- 0=Sunday, 1=Monday, ..., 6=Saturday
    IsWorkingDay BOOLEAN DEFAULT TRUE,
    StartTime TIME,
    EndTime TIME,
    BreakMinutes SMALLINT DEFAULT 0,
    CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT now(),
    UpdatedAt TIMESTAMP WITH TIME ZONE DEFAULT now(),

    CONSTRAINT fk_workscheduledays_schedule
        FOREIGN KEY (ScheduleId)
        REFERENCES WorkSchedules(Id)
        ON DELETE CASCADE,

    CONSTRAINT uq_schedule_day
        UNIQUE (ScheduleId, DayOfWeek)
);
