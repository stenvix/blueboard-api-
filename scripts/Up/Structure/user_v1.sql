CREATE TABLE users
(
    id         BIGSERIAL PRIMARY KEY,
    created    TIMESTAMP    NOT NULL,
    updated    TIMESTAMP,
    created_by VARCHAR(256) NOT NULL,
    updated_by VARCHAR(256),
    first_name VARCHAR(128),
    last_name  VARCHAR(128),
    username   VARCHAR(128),
    email      VARCHAR(256) NOT NULL,
    phone      VARCHAR(16),
    status     SMALLINT     NOT NULL
);

CREATE UNIQUE INDEX ON users USING btree (username);
CREATE UNIQUE INDEX ON users USING btree (email);
CREATE UNIQUE INDEX ON users USING btree (phone);