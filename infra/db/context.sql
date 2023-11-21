CREATE TABLE "Person" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "Nickname" character varying(32) NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Email" text NOT NULL,
    "Birthday" date NOT NULL,
    "Stack" text[] NOT NULL,
    CONSTRAINT "PK_Person" PRIMARY KEY ("Id")
);