CREATE TABLE trips
(
    id          BIGSERIAL PRIMARY KEY,
    created     TIMESTAMP    NOT NULL,
    updated     TIMESTAMP,
    created_by  VARCHAR(256) NOT NULL,
    updated_by  VARCHAR(256),
    name        VARCHAR(128) NOT NULL,
    description VARCHAR(256),
    start_date  TIMESTAMP    NOT NULL,
    end_date    TIMESTAMP    NOT NULL,
    status      SMALLINT     NOT NULL
);