CREATE EXTENSION IF NOT EXISTS pg_trgm;

CREATE OR REPLACE FUNCTION generate_searchable(_name VARCHAR, _nickname VARCHAR, _stack JSON)
    RETURNS TEXT AS $$
    BEGIN
    RETURN _name || _nickname || _stack;
    END;
$$ LANGUAGE plpgsql IMMUTABLE;

CREATE TABLE IF NOT EXISTS Person (
    Id uuid DEFAULT UNIQUE NOT NULL,
    Nickname TEXT UNIQUE NOT NULL,
    Name TEXT NOT NULL,
    Email TEXT NOT NULL,
    Birthday DATE NOT NULL,
    Stack JSON,
    CONSTRAINT "PK_Person" PRIMARY KEY ("Id"),
    searchable text GENERATED ALWAYS AS (generate_searchable(Name, Nickname, Stack)) STORED
);

CREATE INDEX IF NOT EXISTS idx_persons_searchable ON public.Person USING gist (searchable public.gist_trgm_ops (siglen='64'));

CREATE UNIQUE INDEX IF NOT EXISTS persons_nickname_index ON public.Person USING btree (nickname);