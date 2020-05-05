CREATE OR REPLACE FUNCTION create_participant_v1(user_id_in BIGINT, trip_id_in BIGINT, created_by_in BIGINT)
    RETURNS SETOF participants AS
$BODY$

INSERT INTO participants(created, created_by, user_id, trip_id)
VALUES ((now() at time zone 'utc'), created_by_in, user_id_in, trip_id_in)
RETURNING *

$BODY$ LANGUAGE sql;



CREATE OR REPLACE FUNCTION find_participant_by_trip_v1(trip_id_in BIGINT)
    RETURNS SETOF participants AS
$BODY$

SELECT *
FROM participants
WHERE trip_id = trip_id_in;

$BODY$ LANGUAGE sql;



CREATE OR REPLACE FUNCTION find_participant_by_trip_and_user_v1(trip_id_in BIGINT, user_id_in BIGINT)
    RETURNS SETOF participants AS
$BODY$

SELECT *
FROM participants
WHERE trip_id = trip_id_in
  AND user_id = user_id_in;

$BODY$ LANGUAGE sql;



CREATE OR REPLACE FUNCTION participant_exists_v1(trip_id_in BIGINT, user_id_in BIGINT)
    RETURNS BOOLEAN
AS
$BODY$

SELECT EXISTS(SELECT true FROM participants WHERE trip_id = trip_id_in AND user_id = user_id_in)

$BODY$ LANGUAGE sql;


CREATE OR REPLACE FUNCTION remove_participant_v1(id_in BIGINT)
    RETURNS VOID
AS
$BODY$

DELETE
FROM participants
WHERE id = id_in;

$BODY$ LANGUAGE sql;