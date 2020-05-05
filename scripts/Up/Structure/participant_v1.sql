CREATE TABLE participants
(
    id         BIGSERIAL PRIMARY KEY,
    created    TIMESTAMP NOT NULL,
    updated    TIMESTAMP,
    created_by BIGINT    NOT NULL,
    updated_by BIGINT,
    user_id    BIGINT    NOT NULL,
    trip_id    BIGINT    NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users (id),
    FOREIGN KEY (trip_id) REFERENCES trips (id)
);

CREATE UNIQUE INDEX ON participants USING btree (user_id, trip_id);