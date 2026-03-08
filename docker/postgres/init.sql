CREATE USER sportdesk_admin WITH PASSWORD 'shum_pass_i_fort';

CREATE DATABASE sportdesk_db
OWNER sportdesk_admin
ENCODING 'UTF8';

\c sportdesk_db

CREATE TABLE tenants (
    id UUID PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE users (
    id UUID PRIMARY KEY,
    name TEXT NOT NULL,
    surname TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL,
    tel TEXT,
    role TEXT NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id UUID NOT NULL,
    is_active BOOLEAN DEFAULT TRUE NOT NULL,
    CONSTRAINT fk_user_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE sports (
    id UUID PRIMARY KEY,
    name TEXT NOT NULL,
    description TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id UUID NOT NULL,
    is_active BOOLEAN DEFAULT TRUE NOT NULL,
    CONSTRAINT fk_sport_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE coach_sports (
    id UUID PRIMARY KEY,
    coach_id UUID NOT NULL,
    sport_id UUID NOT NULL,
    tenant_id UUID NOT NULL,
    is_active BOOLEAN DEFAULT TRUE NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    CONSTRAINT fk_coachsports_coach FOREIGN KEY (coach_id) REFERENCES users (id) ON DELETE CASCADE,
    CONSTRAINT fk_coachsports_sport FOREIGN KEY (sport_id) REFERENCES sports (id) ON DELETE CASCADE,
    CONSTRAINT fk_coachsports_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE,
    CONSTRAINT uc_coach_sport UNIQUE (coach_id, sport_id)
);

CREATE TABLE monthly_sessions (
    id UUID PRIMARY KEY,
    coach_id UUID NOT NULL,
    sport_id UUID NOT NULL,
    fee NUMERIC(10, 2) NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id UUID NOT NULL,
    is_active BOOLEAN DEFAULT TRUE NOT NULL,
    CONSTRAINT fk_session_coach FOREIGN KEY (coach_id) REFERENCES users (id) ON DELETE CASCADE,
    CONSTRAINT fk_session_sport FOREIGN KEY (sport_id) REFERENCES sports (id) ON DELETE CASCADE,
CONSTRAINT fk_session_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE athletes (
    id UUID PRIMARY KEY,
    name TEXT NOT NULL,
    surname TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL,
    tel TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id UUID NOT NULL,
    is_active BOOLEAN DEFAULT TRUE NOT NULL,
    CONSTRAINT fk_athlete_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE athlete_enrollments (
     id UUID PRIMARY KEY,
     coach_id UUID NOT NULL,
     sport_id UUID NOT NULL,
     athlete_id UUID NOT NULL,
     created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
     updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
     tenant_id UUID NOT NULL,
     is_active BOOLEAN DEFAULT TRUE NOT NULL,
     CONSTRAINT fk_enrollments_coach FOREIGN KEY (coach_id) REFERENCES users (id) ON DELETE CASCADE,
     CONSTRAINT fk_enrollments_sport FOREIGN KEY (sport_id) REFERENCES sports (id) ON DELETE CASCADE,
     CONSTRAINT fk_enrollments_athlete FOREIGN KEY (athlete_id) REFERENCES athletes (id) ON DELETE CASCADE,
     CONSTRAINT fk_enrollments_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE,
     CONSTRAINT uc_athlete_enrollment UNIQUE (coach_id, sport_id, athlete_id)
);

CREATE TABLE payments (
    id UUID PRIMARY KEY,
    session_id UUID NOT NULL,
    athlete_enrollment_id UUID NOT NULL,
    expires_at DATE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id UUID NOT NULL,
    is_active BOOLEAN DEFAULT TRUE NOT NULL,
    CONSTRAINT fk_payment_session FOREIGN KEY (session_id) REFERENCES monthly_sessions (id) ON DELETE CASCADE,
    CONSTRAINT fk_payment_enrollment FOREIGN KEY (athlete_enrollment_id) REFERENCES athlete_enrollments (id) ON DELETE CASCADE,
    CONSTRAINT fk_payment_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);