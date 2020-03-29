DROP FUNCTION IF EXISTS "find_user_by_id_v1";

CREATE OR REPLACE FUNCTION "find_user_by_id_v1"(id_in bigint)
    RETURNS SETOF "users"
    LANGUAGE 'sql'
AS
$BODY$

SELECT *
FROM "users"
WHERE id = id_in

$BODY$