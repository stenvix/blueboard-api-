CREATE TABLE trips
(
    id          BIGSERIAL PRIMARY KEY,
    created     TIMESTAMP    NOT NULL,
    updated     TIMESTAMP,
    created_by  BIGINT NOT NULL,
    updated_by  BIGINT,
    name        VARCHAR(128) NOT NULL,
    description VARCHAR(256),
    start_date  TIMESTAMP    NOT NULL,
    end_date    TIMESTAMP    NOT NULL,
    status      SMALLINT     NOT NULL,
    FOREIGN KEY (created_by) REFERENCES users(id),
    FOREIGN KEY (updated_by) REFERENCES users(id)
);