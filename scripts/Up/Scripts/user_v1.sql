CREATE OR REPLACE FUNCTION create_user_v1(email_in VARCHAR(256), status_in SMALLINT, created_by_in BIGINT)
    RETURNS SETOF users AS
$BODY$

INSERT INTO users ("created", "created_by", "email", "status")
VALUES ((now() at time zone 'utc'), created_by_in, email_in, status_in)
RETURNING *;

$BODY$ LANGUAGE sql;



CREATE OR REPLACE FUNCTION "find_user_by_email_v1"(email_in VARCHAR(256))
    RETURNS SETOF users
    LANGUAGE 'sql'
AS
$BODY$

SELECT *
FROM users
WHERE email = email_in

$BODY$;



CREATE OR REPLACE FUNCTION "find_user_by_id_v1"(id_in BIGINT)
    RETURNS SETOF "users"
    LANGUAGE 'sql'
AS
$BODY$

SELECT *
FROM "users"
WHERE id = id_in

$BODY$;



CREATE OR REPLACE FUNCTION "update_user_v1"(id_in BIGINT, first_name_in VARCHAR(128), last_name_in VARCHAR(128),
                                            username_in VARCHAR(128), email_in VARCHAR(256), phone_in VARCHAR(16), updated_by_in BIGINT)
    RETURNS SETOF users
    LANGUAGE 'sql'
AS
$BODY$

UPDATE users
SET updated    = (now() at time zone 'utc'),
    updated_by = updated_by_in,
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