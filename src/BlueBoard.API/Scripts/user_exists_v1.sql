DROP FUNCTION IF EXISTS "user_exists_v1";

CREATE OR REPLACE FUNCTION "user_exists_v1"(email varchar(256))
    RETURNS bool
    LANGUAGE 'sql'
AS
$BODY$
SELECT EXISTS(SELECT true FROM "users" WHERE "email" = email)
$BODY$