DROP FUNCTION IF EXISTS "find_user_by_email_v1";

CREATE OR REPLACE FUNCTION "find_user_by_email_v1"(email_in varchar(256))
    RETURNS SETOF "users"
    LANGUAGE 'sql'
AS
$BODY$

SELECT *
FROM "users"
WHERE email = email_in

$BODY$