CREATE TABLE `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE DATABASE IF NOT EXISTS `MementoScraperDatabase`;

CREATE TABLE `MementoScraperDatabase`.`Mementos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `MementoId` int NOT NULL,
    `COMMENT` longtext NULL,
    `OWNER` longtext NOT NULL,
    `TYPE` longtext NOT NULL,
    `PHRASE` longtext NOT NULL,
    `CREATION` datetime NOT NULL,
    CONSTRAINT `MEMENTO_PK` PRIMARY KEY (`Id`)
);

CREATE TABLE `MementoScraperDatabase`.`Users` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext NULL,
    `LastName` longtext NULL,
    `Username` longtext NULL,
    `PasswordHash` longblob NULL,
    `PasswordSalt` longblob NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
);

CREATE TABLE `MementoScraperDatabase`.`Memories` (
    `MemoryId` bigint NULL,
    `Id` int NOT NULL AUTO_INCREMENT,
    `MementoForeignKey` int NOT NULL,
    `MEDIA_URL` longtext NULL,
    `MEDIA_URL_HTTPS` longtext NULL,
    `URL` longtext NULL,
    `DISPLAY_URL` longtext NULL,
    `EXPANDED_URL` longtext NULL,
    `MEDIA_TYPE` longtext NOT NULL,
    `CREATION` datetime NOT NULL,
    CONSTRAINT `MEMORY_PK` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Memories_Mementos_MementoForeignKey` FOREIGN KEY (`MementoForeignKey`) REFERENCES `MementoScraperDatabase`.`Mementos` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `MementoScraperDatabase`.`CronDetails` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `Frequency` longtext NULL,
    `Hashtag` longtext NULL,
    `Facebook` bit NOT NULL,
    `Twitter` bit NOT NULL,
    `Instagram` bit NOT NULL,
    `Modification` datetime(6) NOT NULL,
    `Creation` datetime(6) NOT NULL,
    CONSTRAINT `PK_CronDetails` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CronDetails_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `MementoScraperDatabase`.`Users` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_CronDetails_UserId` ON `MementoScraperDatabase`.`CronDetails` (`UserId`);

CREATE INDEX `IX_Memories_MementoForeignKey` ON `MementoScraperDatabase`.`Memories` (`MementoForeignKey`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20181022030855_foreign-keys-trouble-11', '2.2.2-servicing-10034');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20181022031539_foreign-keys-checking-for-magic', '2.2.2-servicing-10034');

