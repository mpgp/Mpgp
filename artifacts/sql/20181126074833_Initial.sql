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
