CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "Accounts" (
    "AccountId" serial NOT NULL,
    "AuthToken" varchar(64) NULL,
    "Avatar" varchar(249) NULL,
    "Languages" varchar(249) NULL,
    "LastOnline" timestamp NOT NULL,
    "Login" varchar(20) NOT NULL,
    "Nickname" varchar(20) NOT NULL,
    "Password" varchar(64) NOT NULL,
    "RegisterDate" timestamp NOT NULL,
    "StatusInfo" varchar(249) NULL,
    CONSTRAINT "PK_Accounts" PRIMARY KEY ("AccountId")
);

CREATE UNIQUE INDEX "IX_Accounts_Login" ON "Accounts" ("Login");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20180527135114_Initial', '2.0.2-rtm-10011');

