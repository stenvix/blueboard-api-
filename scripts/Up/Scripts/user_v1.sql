CREATE OR REPLACE FUNCTION "create_user_v1"(email_in varchar(256), status_in smallint)
    RETURNS SETOF "users"
    LANGUAGE 'sql'
AS
$BODY$

INSERT INTO "users"("created", "created_by", "email", "status")
VALUES ((now() at time zone 'utc'), email_in, email_in, status_in)
RETURNING *

$BODY$;



CREATE OR REPLACE FUNCTION "find_user_by_email_v1"(email_in varchar(256))
    RETURNS SETOF "users"
    LANGUAGE 'sql'
AS
$BODY$

SELECT *
FROM "users"
WHERE email = email_in

$BODY$;



CREATE OR REPLACE FUNCTION "find_user_by_id_v1"(id_in bigint)
    RETURNS SETOF "users"
    LANGUAGE 'sql'
AS
$BODY$

SELECT *
FROM "users"
WHERE id = id_in

$BODY$;



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

$BODY$;



CREATE OR REPLACE FUNCTION "user_exists_v1"(email_in varchar(256))
    RETURNS bool
    LANGUAGE 'sql'
AS
$BODY$

SELECT EXISTS(SELECT true FROM "users" WHERE "email" = email_in)

$BODY$;