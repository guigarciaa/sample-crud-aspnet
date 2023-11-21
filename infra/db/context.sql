CREATE TABLE "User" (
    "Id" uuid NOT NULL,
    "Email" text NOT NULL,
    "Password" text NOT NULL,
    "Roles" text[] NOT NULL,
    CONSTRAINT "PK_User" PRIMARY KEY ("Id")
);

CREATE TABLE "Person" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "Nickname" character varying(32) NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Email" text NOT NULL,
    "Birthday" date NOT NULL,
    "Stack" text[] NOT NULL,
    CONSTRAINT "PK_Person" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Person_User_Id" FOREIGN KEY ("Id") REFERENCES "User" ("Id") ON DELETE CASCADE
);