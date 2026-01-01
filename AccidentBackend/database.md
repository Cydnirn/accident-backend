# sites
id SERIAL PRIMARY KEY
name VARCHAR(200) NOT NULL
site_code VARCHAR(50) UNIQUE
location VARCHAR(400)
site_type VARCHAR(100) -- e.g., "Oil Rig", "Factory"
contact_number VARCHAR(50)
created_at TIMESTAMP DEFAULT now()

# departments
id SERIAL PRIMARY KEY
name VARCHAR(150) NOT NULL
manager_worker_id INT NULL REFERENCES workers(id)
Note: manager_worker_id is nullable to avoid circular FK on worker creation; use application logic or separate HR table.

# shifts
id SERIAL PRIMARY KEY
name VARCHAR(50)
start_time TIME
end_time TIME
description TEXT

# workers
id SERIAL PRIMARY KEY
employee_number VARCHAR(50) UNIQUE
first_name VARCHAR(120)
last_name VARCHAR(120)
dob DATE
hire_date DATE
department_id INT REFERENCES departments(id)
current_site_id INT REFERENCES sites(id)
phone VARCHAR(50)
email VARCHAR(200)
is_contractor BOOLEAN DEFAULT FALSE

# hazard_types (lookup)
id SERIAL PRIMARY KEY
code VARCHAR(50) UNIQUE
name VARCHAR(150)
description TEXT

# accident_causes (lookup)
id SERIAL PRIMARY KEY
code VARCHAR(50) UNIQUE
name VARCHAR(150)
description TEXT

# safety_equipment
id SERIAL PRIMARY KEY
tag_number VARCHAR(100) UNIQUE
name VARCHAR(200)
equipment_type VARCHAR(100)
site_id INT REFERENCES sites(id)
last_inspection_date DATE
status VARCHAR(50)

# accidents (MAIN TRANSACTION)
id BIGSERIAL PRIMARY KEY
accident_number VARCHAR(100) UNIQUE -- e.g., AR-2025-0001
site_id INT REFERENCES sites(id) NOT NULL
occurred_at TIMESTAMP NOT NULL
reported_at TIMESTAMP DEFAULT now()
reported_by_worker_id INT REFERENCES workers(id)
shift_id INT REFERENCES shifts(id)
hazard_type_id INT REFERENCES hazard_types(id)
cause_id INT REFERENCES accident_causes(id)
severity_level SMALLINT -- 1..5 scale
is_fatal BOOLEAN DEFAULT FALSE
description TEXT
root_cause_analysis TEXT
status VARCHAR(50) -- e.g., Open / Closed / Under Investigation
created_at TIMESTAMP DEFAULT now()
updated_at TIMESTAMP
Indexes: create indexes on site_id, occurred_at, severity_level, and status for queries.

# accident_participants
Tracks workers involved (injured, affected, operator).
id SERIAL PRIMARY KEY
accident_id BIGINT REFERENCES accidents(id) ON DELETE CASCADE
worker_id INT REFERENCES workers(id)
role VARCHAR(100) -- e.g., Operator, Supervisor
injured BOOLEAN DEFAULT FALSE
injury_type_id INT NULL -- (optional link to injury types)
notes TEXT

# accident_equipment (many-to-many)
id SERIAL PRIMARY KEY
accident_id BIGINT REFERENCES accidents(id) ON DELETE CASCADE
equipment_id INT REFERENCES safety_equipment(id)
condition_at_time VARCHAR(200)
was_operational BOOLEAN

# witnesses
id SERIAL PRIMARY KEY
accident_id BIGINT REFERENCES accidents(id) ON DELETE CASCADE
worker_id INT NULL REFERENCES workers(id) -- nullable: witness may be a contractor or visitor
name VARCHAR(200)
contact VARCHAR(200)
statement TEXT
recorded_at TIMESTAMP DEFAULT now()

# actions_taken
id SERIAL PRIMARY KEY
accident_id BIGINT REFERENCES accidents(id) ON DELETE CASCADE
action_time TIMESTAMP
performed_by_worker_id INT REFERENCES workers(id)
action_type VARCHAR(150) -- e.g., First Aid, Evacuation, Lockout
notes TEXT

# attachments
id SERIAL PRIMARY KEY
accident_id BIGINT REFERENCES accidents(id) ON DELETE CASCADE
file_name VARCHAR(255)
file_path VARCHAR(1000)
content_type VARCHAR(100)
uploaded_by_worker_id INT REFERENCES workers(id)
uploaded_at TIMESTAMP DEFAULT now()

