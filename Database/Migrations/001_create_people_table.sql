CREATE TABLE People (
    Id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    FirstName   VARCHAR(50) NOT NULL,
    SecondName  VARCHAR(50) NOT NULL,
    ThirdName   VARCHAR(50),
    LastName    VARCHAR(50) NOT NULL,
    PhoneNumber VARCHAR(20),
    Email       VARCHAR(255),
    BirthDate   DATE,
    Gender      SMALLINT DEFAULT 0, -- 0=Unknown, 1=Female, 2=Male
    Address     VARCHAR(255),
    City        VARCHAR(100),
    Country     VARCHAR(100),
    IsActive    BOOLEAN DEFAULT TRUE,
    CreatedAt   TIMESTAMP WITH TIME ZONE DEFAULT now(),
    UpdatedAt   TIMESTAMP WITH TIME ZONE DEFAULT now()
);
