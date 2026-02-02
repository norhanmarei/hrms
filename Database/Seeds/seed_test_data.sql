-- =========================
-- Seed Departments
-- =========================
INSERT INTO Departments (Name, Description, ParentDepartmentId, IsActive)
VALUES
('IT', 'Information Technology', NULL, TRUE),
('HR', 'Human Resources', NULL, TRUE);

-- =========================
-- Seed JobTitles
-- =========================
INSERT INTO JobTitles (Name, Description, DepartmentId, IsActive)
VALUES
('Software Engineer', 'Develops software', 1, TRUE),
('HR Manager', 'Manages HR operations', 2, TRUE);

-- =========================
-- Seed WorkSchedules
-- =========================
INSERT INTO WorkSchedules (Name, Description, IsActive)
VALUES
('Full Time', 'Standard 9am-5pm schedule', TRUE);

-- =========================
-- Seed WorkScheduleDays
-- =========================
-- Sunday (0) to Saturday (6)
INSERT INTO WorkScheduleDays (ScheduleId, DayOfWeek, IsWorkingDay, StartTime, EndTime, BreakMinutes)
VALUES
(1, 0, FALSE, NULL, NULL, 0),  -- Sunday off
(1, 1, TRUE, '09:00', '17:00', 60),  -- Monday
(1, 2, TRUE, '09:00', '17:00', 60),  -- Tuesday
(1, 3, TRUE, '09:00', '17:00', 60),  -- Wednesday
(1, 4, TRUE, '09:00', '17:00', 60),  -- Thursday
(1, 5, TRUE, '09:00', '17:00', 60),  -- Friday
(1, 6, FALSE, NULL, NULL, 0);  -- Saturday off

-- =========================
-- Seed People
-- =========================
INSERT INTO People (FirstName, SecondName, LastName, Email, PhoneNumber, BirthDate, Gender, IsActive)
VALUES
('John', 'A.', 'Doe', 'john.doe@example.com', '555-1234', '1990-01-01', 2, TRUE),
('Jane', 'B.', 'Smith', 'jane.smith@example.com', '555-5678', '1992-02-02', 1, TRUE);

-- =========================
-- Seed Employees
-- =========================
INSERT INTO Employees (PersonId, DepartmentId, JobTitleId, WorkScheduleId, Salary, StartDate, EmployeeNumber, ManagerId, EmploymentType, IsActive)
VALUES
(1, 1, 1, 1, 5000.00, '2025-01-01', 'EMP001', NULL, 'Full-Time', TRUE),
(2, 2, 2, 1, 6000.00, '2025-02-01', 'EMP002', 1, 'Full-Time', TRUE);
