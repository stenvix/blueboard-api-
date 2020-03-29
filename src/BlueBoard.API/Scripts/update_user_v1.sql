DROP FUNCTION IF EXISTS "update_user_v1";

CREATE OR REPLACE FUNCTION "update_user_v1"(id_in bigint, first_name_in varchar(128), last_name_in varchar(128),
                                            username_in varchar(128), email_in varchar(256), phone_in varchar(16))
    RETURNS SETOF users
    LANGUAGE 'sql'
AS
$BODY$

UPDATE users
SET updated    = (now() at time zone 'utc'),
    updated_by = email_in,
    first_name = first_name_in,
    last_name  = last_name_in,
    username   = username_in,
    email      = email_in,
    phone      = phone_in
WHERE id = id_in
RETURNING *;

$BODY$