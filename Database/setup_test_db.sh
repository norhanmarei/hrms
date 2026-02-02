#!/bin/bash

# ============================
# Config
# ============================
DB_NAME="HRMS_Test"
DB_USER="postgres"
DB_HOST="localhost"
DB_PORT="5432"

MIGRATIONS_DIR="./migrations"      # folder containing 001_*.sql, 002_*.sql, etc.
SEED_DIR="./seed"                  # optional folder for seed scripts

# ============================
# Delete existing test database
# ============================
echo "Dropping existing database '$DB_NAME' (if exists)..."
dropdb -h $DB_HOST -p $DB_PORT -U $DB_USER --if-exists $DB_NAME

# ============================
# Create new test database
# ============================
echo "Creating database '$DB_NAME'..."
createdb -h $DB_HOST -p $DB_PORT -U $DB_USER $DB_NAME

# ============================
# Run migration scripts
# ============================
echo "Running migration scripts..."
for script in $(ls $MIGRATIONS_DIR/*.sql | sort); do
    echo "Executing $script..."
    psql -h $DB_HOST -p $DB_PORT -U $DB_USER -d $DB_NAME -f $script
done

# ============================
# Run seed scripts (if any)
# ============================
if [ -d "$SEED_DIR" ]; then
    echo "Running seed scripts..."
    for seed in $(ls $SEED_DIR/*.sql | sort); do
        echo "Seeding $seed..."
        psql -h $DB_HOST -p $DB_PORT -U $DB_USER -d $DB_NAME -f $seed
    done
fi

echo "Test database '$DB_NAME' is ready!"
