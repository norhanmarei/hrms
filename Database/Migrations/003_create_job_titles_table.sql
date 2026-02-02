CREATE TABLE JobTitles (
    Id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(255),

    DepartmentId INT,
    IsActive BOOLEAN DEFAULT TRUE,

    CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT now(),
    UpdatedAt TIMESTAMP WITH TIME ZONE DEFAULT now(),

    CONSTRAINT fk_jobtitles_department
        FOREIGN KEY (DepartmentId)
        REFERENCES Departments(Id)
        ON DELETE SET NULL,

    CONSTRAINT uq_jobtitles_department_name
        UNIQUE (DepartmentId, Name)
);
