ALTER TABLE "Accounts" RENAME COLUMN "AuthToken" TO "Role";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20180708042212_Jwt', '2.1.1-rtm-30846');

