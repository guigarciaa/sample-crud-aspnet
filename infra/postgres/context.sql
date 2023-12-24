CREATE TABLE "Person" (
    "Id" uuid NOT NULL,
    "Nickname" character varying(32) NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Email" text NOT NULL,
    "Birthday" date NOT NULL,
    "Stack" text[] NOT NULL,
    "searchable" TEXT GENERATED ALWAYS AS (
        LOWER("Name" || "Nickname" || "Email")
    ) STORED
);

CREATE EXTENSION pg_trgm;

CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_persons_searchable ON public.Person USING gist (searchable public.gist_trgm_ops (siglen='64'));