CREATE TABLE Employees (
    Id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    PersonId INT NOT NULL,
    DepartmentId INT,
    JobTitleId INT,
    WorkScheduleId INT,
    Salary NUMERIC(10,2) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    EmployeeNumber VARCHAR(20) UNIQUE,
    ManagerId INT,
    EmploymentType VARCHAR(20),

    IsActive BOOLEAN DEFAULT TRUE,
    CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT now(),
    UpdatedAt TIMESTAMP WITH TIME ZONE DEFAULT now(),

    CONSTRAINT fk_employees_person
        FOREIGN KEY (PersonId) REFERENCES People(Id) ON DELETE CASCADE,

    CONSTRAINT fk_employees_department
        FOREIGN KEY (DepartmentId) REFERENCES Departments(Id) ON DELETE SET NULL,

    CONSTRAINT fk_employees_jobtitle
        FOREIGN KEY (JobTitleId) REFERENCES JobTitles(Id) ON DELETE SET NULL,

    CONSTRAINT fk_employees_schedule
        FOREIGN KEY (WorkScheduleId) REFERENCES WorkSchedules(Id) ON DELETE SET NULL,

    CONSTRAINT fk_employees_manager
        FOREIGN KEY (ManagerId) REFERENCES Employees(Id) ON DELETE SET NULL
);
