CREATE
USER sportdesk_admin WITH PASSWORD 'shum_pass_i_fort';

CREATE
DATABASE sportdesk_db
OWNER sportdesk_admin
ENCODING 'UTF8';

\c
sportdesk_db

CREATE TABLE tenants
(
    id   UUID PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE users
(
    id         UUID PRIMARY KEY,
    name       TEXT                                               NOT NULL,
    surname    TEXT                                               NOT NULL,
    email      TEXT UNIQUE                                        NOT NULL,
    tel        TEXT,
    role       TEXT                                               NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id  UUID                                               NOT NULL,
    CONSTRAINT fk_user_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE sports
(
    id          UUID PRIMARY KEY,
    name        TEXT                                               NOT NULL,
    description TEXT,
    created_at  TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at  TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id   UUID                                               NOT NULL,
    CONSTRAINT fk_sport_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE coach_sports
(
    id        UUID PRIMARY KEY,
    coach_id  UUID NOT NULL,
    sport_id  UUID NOT NULL,
    tenant_id UUID NOT NULL,
    CONSTRAINT fk_coachsports_coach FOREIGN KEY (coach_id) REFERENCES users (id) ON DELETE CASCADE,
    CONSTRAINT fk_coachsports_sport FOREIGN KEY (sport_id) REFERENCES sports (id) ON DELETE CASCADE,
    CONSTRAINT fk_coachsports_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE,
    CONSTRAINT uc_coach_sport UNIQUE (coach_id, sport_id)
);

CREATE TABLE enrollments
(
    id             UUID PRIMARY KEY,
    coach_sport_id UUID                                               NOT NULL,
    fee            NUMERIC(10, 2)                                     NOT NULL,
    created_at     TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at     TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id      UUID                                               NOT NULL,
    CONSTRAINT fk_enrollment_coach_sport FOREIGN KEY (coach_sport_id) REFERENCES coach_sports (id) ON DELETE CASCADE,
    CONSTRAINT fk_enrollment_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE athletes
(
    id         UUID PRIMARY KEY,
    name       TEXT                                               NOT NULL,
    surname    TEXT                                               NOT NULL,
    email      TEXT UNIQUE                                        NOT NULL,
    tel        TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id  UUID                                               NOT NULL,
    CONSTRAINT fk_athlete_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE athlete_enrollments
(
    id            UUID PRIMARY KEY,
    enrollment_id UUID    NOT NULL,
    athlete_id    UUID    NOT NULL,
    months        INTEGER NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id     UUID    NOT NULL,
    CONSTRAINT fk_athlete_enrollments_enrollment FOREIGN KEY (enrollment_id) REFERENCES enrollments (id) ON DELETE CASCADE,
    CONSTRAINT fk_athlete_enrollments_athlete FOREIGN KEY (athlete_id) REFERENCES athletes (id) ON DELETE CASCADE,
    CONSTRAINT fk_athlete_enrollments_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);

CREATE TABLE payments
(
    id                    UUID PRIMARY KEY,
    athlete_enrollment_id UUID                                               NOT NULL,
    months                INTEGER                                            NOT NULL,
    created_at            TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updated_at            TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP NOT NULL,
    tenant_id             UUID                                               NOT NULL,
    CONSTRAINT fk_payment_enrollment FOREIGN KEY (athlete_enrollment_id) REFERENCES athlete_enrollments (id) ON DELETE CASCADE,
    CONSTRAINT fk_payment_tenant FOREIGN KEY (tenant_id) REFERENCES tenants (id) ON DELETE CASCADE
);