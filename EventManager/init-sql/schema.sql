CREATE TABLE IF NOT EXISTS locations(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	latitude  NUMERIC(9, 6) NOT NULL,
    longitude NUMERIC(9, 6) NOT NULL,
    description TEXT,
    CONSTRAINT uq_location_coordinates UNIQUE (latitude, longitude)
);


CREATE TABLE IF NOT EXISTS categories(
	category_id   BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    category_name VARCHAR(50) NOT NULL UNIQUE,
    CONSTRAINT category_name_len CHECK (LENGTH(category_name) >= 1 AND LENGTH(category_name) <= 50)
);

CREATE TABLE IF NOT EXISTS roles(
	role_id smallint generated always as identity primary key,
	role_name VARCHAR(100) NOT NULL UNIQUE,
	description TEXT
);

CREATE TABLE IF NOT EXISTS users(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	telegram_id BIGINT NOT NULL unique,
	email VARCHAR(100) UNIQUE CHECK (email ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$'),
	role_id smallint NOT NULL DEFAULT 1,
	FOREIGN KEY (role_id) REFERENCES roles(role_id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS events(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	title VARCHAR(150) NOT NULL,
	description TEXT,
	location_id BIGINT NOT NULL,
	created_on TIMESTAMP NOT NULL DEFAULT NOW(),
	event_date TIMESTAMP NOT NULL,
	is_paid BOOLEAN DEFAULT false,
	participant_limit INTEGER NOT NULL DEFAULT 10,
	category_id BIGINT NOT NULL,
	state SMALLINT NOT NULL,
	initiator_id BIGINT NOT NULL,
	FOREIGN KEY (initiator_id) REFERENCES users(id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (category_id) REFERENCES categories(category_id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (location_id) REFERENCES locations(id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT event_date_constraint CHECK (event_date > now() + INTERVAL '24 hours')
);

CREATE TABLE IF NOT EXISTS requests
(
    request_id   BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    requester_id BIGINT  NOT NULL,
    event_id     BIGINT  NOT NULL,
    status       SMALLINT NOT NULL,
    created      TIMESTAMP NOT NULL DEFAULT NOW(),
    FOREIGN KEY (requester_id) REFERENCES users(id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (event_id) REFERENCES events(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT uq_event_requestor UNIQUE (requester_id, event_id)
);

CREATE TABLE IF NOT EXISTS comments(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	author_id BIGINT NOT NULL,
	event_id BIGINT NOT NULL,
	created TIMESTAMP NOT NULL DEFAULT NOW(),
	is_positive BOOLEAN NOT NULL default TRUE,
	title VARCHAR(100) NOT NULL,
	description VARCHAR(700) NOT NULL,
	FOREIGN KEY (author_id) REFERENCES users(id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (event_id) REFERENCES events(id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT uq_comment UNIQUE (author_id, event_id)
);

CREATE TABLE IF NOT EXISTS images(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	event_id BIGINT NOT NULL,
	path VARCHAR(500) NOT NULL,
	FOREIGN KEY (event_id) REFERENCES events(id) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE OR REPLACE FUNCTION check_photo_limit()
RETURNS trigger AS $$
BEGIN
    IF (SELECT COUNT(*) FROM images WHERE event_id = NEW.event_id) >= 10 THEN
        RAISE EXCEPTION 'Нельзя добавить больше 10 фото для одного event_id';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trigger_photo_limit
BEFORE INSERT ON images
FOR EACH ROW
EXECUTE FUNCTION check_photo_limit();
