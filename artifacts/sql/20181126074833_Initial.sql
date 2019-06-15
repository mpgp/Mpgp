CREATE TABLE "Accounts" (
    "Id" serial NOT NULL,
    "Role" varchar(64) DEFAULT 'User' NOT NULL,
    "Avatar" varchar(249) DEFAULT '1.jpg' NOT NULL,
    "Languages" varchar(249) NULL,
    "LastOnline" timestamp without time zone DEFAULT NOW() NOT NULL,
    "Login" varchar(20) NOT NULL,
    "Nickname" varchar(20) NOT NULL,
    "Password" varchar(64) NOT NULL,
    "RegisterDate" timestamp without time zone DEFAULT NOW() NOT NULL,
    "StatusInfo" varchar(249) NULL,
    CONSTRAINT "PK_Accounts" PRIMARY KEY ("Id")
);

CREATE UNIQUE INDEX "IX_Accounts_Login" ON "Accounts" ("Login");

CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId"    varchar(150) not null constraint "PK___EFMigrationsHistory" primary key,
    "ProductVersion" varchar(32)  not null
);

INSERT INTO "__EFMigrationsHistory"
VALUES ('20181126074833_Initial', '2.2.4-servicing-10062');