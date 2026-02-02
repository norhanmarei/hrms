CREATE TABLE Departments (
    Id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(255),

    ParentDepartmentId INT,
    IsActive BOOLEAN DEFAULT TRUE,

    CreatedAt TIMESTAMP WITH TIME ZONE DEFAULT now(),
    UpdatedAt TIMESTAMP WITH TIME ZONE DEFAULT now(),

    CONSTRAINT fk_departments_parent
        FOREIGN KEY (ParentDepartmentId)
        REFERENCES Departments(Id)
        ON DELETE SET NULL,

    CONSTRAINT uq_departments_parent_name
        UNIQUE (ParentDepartmentId, Name)
);
