DROP FUNCTION IF EXISTS "create_user_v1";

CREATE OR REPLACE FUNCTION "create_user_v1"(email_in varchar(256), status_in smallint)
    RETURNS SETOF "users"
    LANGUAGE 'sql'
AS
$BODY$
INSERT INTO "users"("created", "created_by", "email", "status")
VALUES ((now() at time zone 'utc'), email_in, email_in, status_in)
RETURNING *

$BODY$