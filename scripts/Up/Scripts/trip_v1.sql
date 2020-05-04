CREATE OR REPLACE FUNCTION create_trip_v1(user_id_in BIGINT,
                                          name_in VARCHAR(128),
                                          description_in VARCHAR(256),
                                          start_date_in TIMESTAMP WITHOUT TIME ZONE,
                                          end_date_in TIMESTAMP WITHOUT TIME ZONE,
                                          status_in SMALLINT)
    RETURNS SETOF trips
AS
$BODY$

INSERT INTO trips(created, created_by, name, description, start_date, end_date, status)
VALUES ((now() at time zone 'utc'), user_id_in, name_in, description_in, start_date_in, end_date_in, status_in)

RETURNING *;

$BODY$ LANGUAGE 'sql';


CREATE OR REPLACE FUNCTION update_trip_v1(id_in bigint,
                                          user_id_in BIGINT,
                                          name_in VARCHAR(128),
                                          description_in VARCHAR(256),
                                          start_date_in TIMESTAMP WITHOUT TIME ZONE,
                                          end_date_in TIMESTAMP WITHOUT TIME ZONE)
    RETURNS SETOF trips
AS
$BODY$

UPDATE trips
SET updated     =(now() at time zone 'utc'),
    updated_by  = user_id_in,
    name        = name_in,
    description = description_in,
    start_date  = start_date_in,
    end_date    = end_date_in
WHERE id = id_in
RETURNING *

$BODY$ LANGUAGE 'sql';



CREATE OR REPLACE FUNCTION find_trip_by_id_v1(id_in bigint)
    RETURNS SETOF trips
AS
$BODY$

SELECT *
FROM trips
WHERE id = id_in;

$BODY$ LANGUAGE 'sql';



CREATE OR REPLACE FUNCTION find_trips_by_user_v1(user_id_in BIGINT)
    RETURNS SETOF trips
AS
$BODY$

SELECT *
FROM trips
WHERE created_by = user_id_in
ORDER BY created;

$BODY$ LANGUAGE 'sql';
