/*
 Navicat SQLite Data Transfer

 Source Server         : livehts
 Source Server Type    : SQLite
 Source Server Version : 3017000
 Source Schema         : main

 Target Server Type    : SQLite
 Target Server Version : 3017000
 File Encoding         : 65001

 Date: 22/07/2017 10:23:09
*/

PRAGMA foreign_keys = false;

-- ----------------------------
-- Table structure for Action
-- ----------------------------
DROP TABLE IF EXISTS "Action";
CREATE TABLE "Action" (
  "Name" varchar,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Action"
-- ----------------------------
INSERT INTO "Action" VALUES ('None', 'None', 0);
INSERT INTO "Action" VALUES ('Rng', 'Rng', 0);
INSERT INTO "Action" VALUES ('Rm', 'Rm', 0);
INSERT INTO "Action" VALUES ('Set', 'Set', 0);

-- ----------------------------
-- Table structure for Category
-- ----------------------------
DROP TABLE IF EXISTS "Category";
CREATE TABLE "Category" (
  "Code" varchar,
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Category"
-- ----------------------------
INSERT INTO "Category" VALUES ('YesNo', '62040a3e-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Category" VALUES ('Result', '62040b24-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Category" VALUES ('Services', '62040c00-6260-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for CategoryItem
-- ----------------------------
DROP TABLE IF EXISTS "CategoryItem";
CREATE TABLE "CategoryItem" (
  "CategoryId" varchar(36),
  "ItemId" varchar(36),
  "Display" varchar,
  "Rank" float,
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "CategoryItem"
-- ----------------------------
INSERT INTO "CategoryItem" VALUES ('62040a3e-6260-11e7-907b-a6006ad3dba0', '00c2a902-6246-11e7-907b-a6006ad3dba0', '', 1.0, '6206b720-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "CategoryItem" VALUES ('62040a3e-6260-11e7-907b-a6006ad3dba0', '00c2aae2-6246-11e7-907b-a6006ad3dba0', '', 2.0, '6206b8b0-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "CategoryItem" VALUES ('62040b24-6260-11e7-907b-a6006ad3dba0', '00c2abb4-6246-11e7-907b-a6006ad3dba0', '', 1.0, '6206bd06-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "CategoryItem" VALUES ('62040b24-6260-11e7-907b-a6006ad3dba0', '00c2ac90-6246-11e7-907b-a6006ad3dba0', '', 2.0, '6206be1e-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "CategoryItem" VALUES ('62040b24-6260-11e7-907b-a6006ad3dba0', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', 3.0, '6206befa-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "CategoryItem" VALUES ('62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b2f8-6246-11e7-907b-a6006ad3dba0', '', 1.0, '6206bfcc-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "CategoryItem" VALUES ('62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b3c0-6246-11e7-907b-a6006ad3dba0', '', 2.0, '6206c0a8-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "CategoryItem" VALUES ('62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b4f8-6246-11e7-907b-a6006ad3dba0', '', 3.0, '6206c184-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "CategoryItem" VALUES ('62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b488-6246-11e7-907b-a6006ad3dba0', '', 4.0, '6206c256-6260-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for Client
-- ----------------------------
DROP TABLE IF EXISTS "Client";
CREATE TABLE "Client" (
  "PracticeId" varchar(36),
  "PersonId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Client"
-- ----------------------------
INSERT INTO "Client" VALUES ('7e51629e-6b99-11e7-907b-a6006ad3dba0', '82dfdc68-6c3c-4a39-8f1f-a7b7016df22e', '4547b7e0-98c7-4c6f-9d2a-a7b7016df232', 0);
INSERT INTO "Client" VALUES ('7e51629e-6b99-11e7-907b-a6006ad3dba0', 'e8d87aa0-3970-4467-b2f4-a7b7016df22e', 'd3bf79fe-a049-49fa-b83c-a7b7016df233', 0);

-- ----------------------------
-- Table structure for ClientAttribute
-- ----------------------------
DROP TABLE IF EXISTS "ClientAttribute";
CREATE TABLE "ClientAttribute" (
  "Name" varchar,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "ClientAttribute"
-- ----------------------------
INSERT INTO "ClientAttribute" VALUES ('Partner', 'Partner', 0);

-- ----------------------------
-- Table structure for ClientIdentifier
-- ----------------------------
DROP TABLE IF EXISTS "ClientIdentifier";
CREATE TABLE "ClientIdentifier" (
  "IdentifierTypeId" varchar,
  "Identifier" varchar,
  "Preferred" integer,
  "ClientId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Table structure for Concept
-- ----------------------------
DROP TABLE IF EXISTS "Concept";
CREATE TABLE "Concept" (
  "Name" varchar,
  "ConceptTypeId" varchar,
  "CategoryId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Concept"
-- ----------------------------
INSERT INTO "Concept" VALUES ('Consent', 'Single', '62040a3e-6260-11e7-907b-a6006ad3dba0', '00c2a60a-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Concept" VALUES ('Result', 'Single', '62040b24-6260-11e7-907b-a6006ad3dba0', '00c2aa06-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Concept" VALUES ('No of Kits', 'Numeric', NULL, '00c2b14a-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Concept" VALUES ('Referall', 'Multi', '62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b23a-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Concept" VALUES ('Discordant', 'Single', '62040a3e-6260-11e7-907b-a6006ad3dba0', '6203cad8-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Concept" VALUES ('Remarks', 'Text', NULL, '00c2b550-6246-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for ConceptType
-- ----------------------------
DROP TABLE IF EXISTS "ConceptType";
CREATE TABLE "ConceptType" (
  "Name" varchar,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "ConceptType"
-- ----------------------------
INSERT INTO "ConceptType" VALUES ('Single', 'Single', 0);
INSERT INTO "ConceptType" VALUES ('Numeric', 'Numeric', 0);
INSERT INTO "ConceptType" VALUES ('Multi', 'Multi', 0);
INSERT INTO "ConceptType" VALUES ('Text', 'Text', 0);

-- ----------------------------
-- Table structure for Condition
-- ----------------------------
DROP TABLE IF EXISTS "Condition";
CREATE TABLE "Condition" (
  "Name" varchar,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Condition"
-- ----------------------------
INSERT INTO "Condition" VALUES ('Post', 'Post', 0);
INSERT INTO "Condition" VALUES ('Pre', 'Pre', 0);

-- ----------------------------
-- Table structure for County
-- ----------------------------
DROP TABLE IF EXISTS "County";
CREATE TABLE "County" (
  "Name" varchar,
  "Id" integer NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "County"
-- ----------------------------
INSERT INTO "County" VALUES ('Nairobi', 47, 0);

-- ----------------------------
-- Table structure for Device
-- ----------------------------
DROP TABLE IF EXISTS "Device";
CREATE TABLE "Device" (
  "Serial" varchar,
  "Code" varchar,
  "Name" varchar,
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Device"
-- ----------------------------
INSERT INTO "Device" VALUES (12345, 'V01', 'VCT-01', '7e51658c-6b99-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for Encounter
-- ----------------------------
DROP TABLE IF EXISTS "Encounter";
CREATE TABLE "Encounter" (
  "ClientId" varchar(36),
  "FormId" varchar(36),
  "EncounterTypeId" varchar(36),
  "EncounterDate" datetime,
  "ProviderId" varchar(36),
  "DeviceId" varchar(36),
  "PracticeId" varchar(36),
  "Started" datetime,
  "Stopped" datetime,
  "UserId" varchar(36),
  "IsComplete" integer,
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Encounter"
-- ----------------------------
INSERT INTO "Encounter" VALUES ('4547b7e0-98c7-4c6f-9d2a-a7b7016df232', '62040dcc-6260-11e7-907b-a6006ad3dba0', '7e5164a6-6b99-11e7-907b-a6006ad3dba0', '2017-07-22T00:00:00.000', '158790da-a5c7-4a11-9d49-a7b7016df234', '7e51658c-6b99-11e7-907b-a6006ad3dba0', '7e51629e-6b99-11e7-907b-a6006ad3dba0', '2017-07-22T00:00:00.000', '2017-07-22T00:00:00.000', '00000000-0000-0000-0000-000000000001', 0, 'afc9f878-c187-487d-bd82-a7b7016df23c', 0);
INSERT INTO "Encounter" VALUES ('4547b7e0-98c7-4c6f-9d2a-a7b7016df232', '62040dcc-6260-11e7-907b-a6006ad3dba0', '7e5164a6-6b99-11e7-907b-a6006ad3dba0', '2017-07-23T00:00:00.000', '158790da-a5c7-4a11-9d49-a7b7016df234', '7e51658c-6b99-11e7-907b-a6006ad3dba0', '7e51629e-6b99-11e7-907b-a6006ad3dba0', '2017-07-23T00:00:00.000', '2017-07-23T00:00:00.000', '00000000-0000-0000-0000-000000000002', 1, 'f56d3468-5a84-4b9b-89da-a7b7016df23c', 1);

-- ----------------------------
-- Table structure for EncounterType
-- ----------------------------
DROP TABLE IF EXISTS "EncounterType";
CREATE TABLE "EncounterType" (
  "Name" varchar,
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "EncounterType"
-- ----------------------------
INSERT INTO "EncounterType" VALUES ('HTS Initial', '7e5164a6-6b99-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for Form
-- ----------------------------
DROP TABLE IF EXISTS "Form";
CREATE TABLE "Form" (
  "Name" varchar,
  "Display" varchar,
  "Description" varchar,
  "Rank" float,
  "ModuleId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Form"
-- ----------------------------
INSERT INTO "Form" VALUES ('HTS Lab Form', 'HTS Lab Form', 'HTS Lab Form', 1.0, '62040ce6-6260-11e7-907b-a6006ad3dba0', '62040dcc-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Form" VALUES ('HTS Linkage Form', 'HTS Linkage Form', 'HTS Linkage Form', 2.0, '62040ce6-6260-11e7-907b-a6006ad3dba0', '62040eb2-6260-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for IdentifierType
-- ----------------------------
DROP TABLE IF EXISTS "IdentifierType";
CREATE TABLE "IdentifierType" (
  "Name" varchar,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "IdentifierType"
-- ----------------------------
INSERT INTO "IdentifierType" VALUES ('Serial', 'Serial', 0);

-- ----------------------------
-- Table structure for Item
-- ----------------------------
DROP TABLE IF EXISTS "Item";
CREATE TABLE "Item" (
  "Code" varchar,
  "Display" varchar,
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Item"
-- ----------------------------
INSERT INTO "Item" VALUES ('Y', 'Y', '00c2a902-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Item" VALUES ('N', 'N', '00c2aae2-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Item" VALUES ('Pos', 'Pos', '00c2abb4-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Item" VALUES ('Neg', 'Neg', '00c2ac90-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Item" VALUES ('Inc', 'Inc', '00c2ad58-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Item" VALUES ('PrEP', 'PrEP', '00c2b2f8-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Item" VALUES ('CCC', 'CCC', '00c2b3c0-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Item" VALUES ('Counselling', 'Counselling', '00c2b4f8-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Item" VALUES ('Compulsory', 'Compulsory', '00c2b488-6246-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for Module
-- ----------------------------
DROP TABLE IF EXISTS "Module";
CREATE TABLE "Module" (
  "Name" varchar,
  "Display" varchar,
  "Description" varchar,
  "Rank" float,
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Module"
-- ----------------------------
INSERT INTO "Module" VALUES ('HTS Module', 'Hiv Testing Services Module', 'Hiv Testing Services Module', 1.0, '62040ce6-6260-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for Obs
-- ----------------------------
DROP TABLE IF EXISTS "Obs";
CREATE TABLE "Obs" (
  "QuestionId" varchar(36),
  "ObsDate" datetime,
  "ValueText" varchar,
  "ValueNumeric" float,
  "ValueCoded" varchar(36),
  "ValueMultiCoded" varchar,
  "ValueDateTime" datetime,
  "EncounterId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Obs"
-- ----------------------------
INSERT INTO "Obs" VALUES ('6206a9a6-6260-11e7-907b-a6006ad3dba0', '2017-07-22T01:12:22.171', 'ValueText1', 1.0, '00c2a902-6246-11e7-907b-a6006ad3dba0', 'ValueMultiCoded1', '2017-07-22T00:00:00.000', 'afc9f878-c187-487d-bd82-a7b7016df23c', '664c8f39-7db6-4cd3-bda0-a7b7016df23d', 0);
INSERT INTO "Obs" VALUES ('6206aa78-6260-11e7-907b-a6006ad3dba0', '2017-07-22T01:12:22.171', 'ValueText2', 2.0, '00000000-0000-0000-0000-000000000002', 'ValueMultiCoded2', '2017-07-23T00:00:00.000', 'afc9f878-c187-487d-bd82-a7b7016df23c', 'e651d7d2-f252-4a85-8b4f-a7b7016df23d', 1);
INSERT INTO "Obs" VALUES ('6206ab4a-6260-11e7-907b-a6006ad3dba0', '2017-07-22T01:12:22.171', 'ValueText3', 3.0, '00000000-0000-0000-0000-000000000003', 'ValueMultiCoded3', '2017-07-24T00:00:00.000', 'afc9f878-c187-487d-bd82-a7b7016df23c', 'da65db29-ce13-42ed-b387-a7b7016df23d', 0);
INSERT INTO "Obs" VALUES ('6206ac1c-6260-11e7-907b-a6006ad3dba0', '2017-07-22T01:12:22.171', 'ValueText4', 4.0, '00000000-0000-0000-0000-000000000004', 'ValueMultiCoded4', '2017-07-25T00:00:00.000', 'afc9f878-c187-487d-bd82-a7b7016df23c', '5d7b3ff7-8c94-4fea-a72a-a7b7016df23d', 1);
INSERT INTO "Obs" VALUES ('6206a9a6-6260-11e7-907b-a6006ad3dba0', '2017-07-22T01:12:22.172', 'ValueText1', 1.0, '00c2a902-6246-11e7-907b-a6006ad3dba0', 'ValueMultiCoded1', '2017-07-22T00:00:00.000', 'f56d3468-5a84-4b9b-89da-a7b7016df23c', 'f36834d4-9796-4248-924e-a7b7016df23e', 0);
INSERT INTO "Obs" VALUES ('6206aa78-6260-11e7-907b-a6006ad3dba0', '2017-07-22T01:12:22.172', 'ValueText2', 2.0, '00000000-0000-0000-0000-000000000002', 'ValueMultiCoded2', '2017-07-23T00:00:00.000', 'f56d3468-5a84-4b9b-89da-a7b7016df23c', '55e9414c-c8f9-4ab7-bf9d-a7b7016df23e', 1);
INSERT INTO "Obs" VALUES ('6206ab4a-6260-11e7-907b-a6006ad3dba0', '2017-07-22T01:12:22.172', 'ValueText3', 3.0, '00000000-0000-0000-0000-000000000003', 'ValueMultiCoded3', '2017-07-24T00:00:00.000', 'f56d3468-5a84-4b9b-89da-a7b7016df23c', '033c780f-f488-447d-8d0f-a7b7016df23e', 0);
INSERT INTO "Obs" VALUES ('6206ac1c-6260-11e7-907b-a6006ad3dba0', '2017-07-22T01:12:22.172', 'ValueText4', 4.0, '00000000-0000-0000-0000-000000000004', 'ValueMultiCoded4', '2017-07-25T00:00:00.000', 'f56d3468-5a84-4b9b-89da-a7b7016df23c', 'f8ff38b0-efba-4b0c-af67-a7b7016df23e', 1);

-- ----------------------------
-- Table structure for Person
-- ----------------------------
DROP TABLE IF EXISTS "Person";
CREATE TABLE "Person" (
  "FirstName" varchar,
  "MiddleName" varchar,
  "LastName" varchar,
  "Gender" varchar,
  "BirthDate" datetime,
  "BirthDateEstimated" integer,
  "Email" varchar,
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Person"
-- ----------------------------
INSERT INTO "Person" VALUES ('FirstName1', 'MiddleName1', 'LastName1', 'Gender1', '2017-07-22T00:00:00.000', 0, 'Email1', '82dfdc68-6c3c-4a39-8f1f-a7b7016df22e', 0);
INSERT INTO "Person" VALUES ('FirstName2', 'MiddleName2', 'LastName2', 'Gender2', '2017-07-23T00:00:00.000', 1, 'Email2', 'e8d87aa0-3970-4467-b2f4-a7b7016df22e', 1);
INSERT INTO "Person" VALUES ('FirstName1', 'MiddleName1', 'LastName1', 'Gender1', '2017-07-22T00:00:00.000', 0, 'Email1', 'b4d18679-ed7e-4e02-8cc5-a7b7016df233', 0);
INSERT INTO "Person" VALUES ('FirstName1', 'MiddleName1', 'LastName1', 'Gender1', '2017-07-22T00:00:00.000', 0, 'Email1', '1fa07f17-d5fe-4daf-9eee-a7b7016df234', 0);

-- ----------------------------
-- Table structure for PersonAddress
-- ----------------------------
DROP TABLE IF EXISTS "PersonAddress";
CREATE TABLE "PersonAddress" (
  "Landmark" varchar,
  "CountyId" varchar(36),
  "Preferred" integer,
  "PersonId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Table structure for PersonContact
-- ----------------------------
DROP TABLE IF EXISTS "PersonContact";
CREATE TABLE "PersonContact" (
  "Phone" integer,
  "Preferred" integer,
  "PersonId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Table structure for PracticeType
-- ----------------------------
DROP TABLE IF EXISTS "PracticeType";
CREATE TABLE "PracticeType" (
  "Name" varchar,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "PracticeType"
-- ----------------------------
INSERT INTO "PracticeType" VALUES ('Facility Based', 'Facility', 0);

-- ----------------------------
-- Table structure for Provider
-- ----------------------------
DROP TABLE IF EXISTS "Provider";
CREATE TABLE "Provider" (
  "ProviderTypeId" varchar,
  "Code" varchar,
  "PracticeId" varchar(36),
  "PersonId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Provider"
-- ----------------------------
INSERT INTO "Provider" VALUES ('HW', 'Code1', '7e51629e-6b99-11e7-907b-a6006ad3dba0', '1fa07f17-d5fe-4daf-9eee-a7b7016df234', '158790da-a5c7-4a11-9d49-a7b7016df234', 0);

-- ----------------------------
-- Table structure for ProviderType
-- ----------------------------
DROP TABLE IF EXISTS "ProviderType";
CREATE TABLE "ProviderType" (
  "Name" varchar,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "ProviderType"
-- ----------------------------
INSERT INTO "ProviderType" VALUES ('Health Worker', 'HW', 0);

-- ----------------------------
-- Table structure for Question
-- ----------------------------
DROP TABLE IF EXISTS "Question";
CREATE TABLE "Question" (
  "ConceptId" varchar(36),
  "Ordinal" varchar,
  "Display" varchar,
  "Rank" float,
  "FormId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Question"
-- ----------------------------
INSERT INTO "Question" VALUES ('00c2a60a-6246-11e7-907b-a6006ad3dba0', 1, 'Consent', 1.0, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206a9a6-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Question" VALUES ('00c2aa06-6246-11e7-907b-a6006ad3dba0', 2, 'Result', 2.0, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206aa78-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Question" VALUES ('00c2b14a-6246-11e7-907b-a6006ad3dba0', 3, 'No of Kits', 3.0, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206ab4a-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Question" VALUES ('00c2b23a-6246-11e7-907b-a6006ad3dba0', 4, 'Referall', 4.0, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206ac1c-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Question" VALUES ('6203cad8-6260-11e7-907b-a6006ad3dba0', 5, 'Discordant', 5.0, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206acf8-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "Question" VALUES ('00c2b550-6246-11e7-907b-a6006ad3dba0', 6, 'Remarks', 6.0, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206b13a-6260-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for QuestionBranch
-- ----------------------------
DROP TABLE IF EXISTS "QuestionBranch";
CREATE TABLE "QuestionBranch" (
  "ConditionId" varchar,
  "RefQuestionId" varchar(36),
  "ResponseType" varchar,
  "Response" varchar,
  "ResponseComplex" varchar,
  "Group" float,
  "ActionId" varchar,
  "GotoQuestionId" varchar(36),
  "QuestionId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "QuestionBranch"
-- ----------------------------
INSERT INTO "QuestionBranch" VALUES ('Post', NULL, '=', '00c2aae2-6246-11e7-907b-a6006ad3dba0', '', 0.0, 'None', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6206a9a6-6260-11e7-907b-a6006ad3dba0', '6203d8de-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionBranch" VALUES ('Post', NULL, '=', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', 0.0, 'None', '6206b13a-6260-11e7-907b-a6006ad3dba0', '6206aa78-6260-11e7-907b-a6006ad3dba0', '6203dd3e-6260-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for QuestionReValidation
-- ----------------------------
DROP TABLE IF EXISTS "QuestionReValidation";
CREATE TABLE "QuestionReValidation" (
  "ConditionId" varchar,
  "RefQuestionId" varchar(36),
  "ResponseType" varchar,
  "Response" varchar,
  "ResponseComplex" varchar,
  "Group" float,
  "ActionId" varchar,
  "QuestionValidationId" varchar(36),
  "QuestionId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "QuestionReValidation"
-- ----------------------------
INSERT INTO "QuestionReValidation" VALUES ('Pre', '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', NULL, 'Rng', '6206a2d0-6260-11e7-907b-a6006ad3dba0', '6206ab4a-6260-11e7-907b-a6006ad3dba0', '6203e068-6260-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for QuestionRemoteTransformation
-- ----------------------------
DROP TABLE IF EXISTS "QuestionRemoteTransformation";
CREATE TABLE "QuestionRemoteTransformation" (
  "ConditionId" varchar,
  "ClientAttributeId" varchar,
  "RemoteQuestionId" varchar(36),
  "SelfQuestionId" varchar(36),
  "ResponseType" varchar,
  "Response" varchar,
  "ResponseComplex" varchar,
  "Group" float,
  "ActionId" varchar,
  "Content" varchar,
  "AltContent" varchar,
  "QuestionId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "QuestionRemoteTransformation"
-- ----------------------------
INSERT INTO "QuestionRemoteTransformation" VALUES ('Pre', 'Partner', '6206aa78-6260-11e7-907b-a6006ad3dba0', NULL, '!=', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', 1.0, '', '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1b46-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionRemoteTransformation" VALUES ('Pre', 'Partner', NULL, '6206aa78-6260-11e7-907b-a6006ad3dba0', '!=', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', 1.0, '', '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1c72-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionRemoteTransformation" VALUES ('Pre', 'Partner', '6206aa78-6260-11e7-907b-a6006ad3dba0', NULL, '=', '00c2abb4-6246-11e7-907b-a6006ad3dba0', '', 2.0, '', '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1d4e-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionRemoteTransformation" VALUES ('Pre', 'Partner', NULL, '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2ac90-6246-11e7-907b-a6006ad3dba0', '', 2.0, 'Set', '00c2a902-6246-11e7-907b-a6006ad3dba0', '00c2aae2-6246-11e7-907b-a6006ad3dba0', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1e16-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionRemoteTransformation" VALUES ('Pre', 'Partner', '6206aa78-6260-11e7-907b-a6006ad3dba0', NULL, '=', '00c2ac90-6246-11e7-907b-a6006ad3dba0', '', 3.0, '', '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1ede-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionRemoteTransformation" VALUES ('Pre', 'Partner', NULL, '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2abb4-6246-11e7-907b-a6006ad3dba0', '', 3.0, 'Set', '00c2a902-6246-11e7-907b-a6006ad3dba0', '00c2aae2-6246-11e7-907b-a6006ad3dba0', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1f9c-67a9-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for QuestionTransformation
-- ----------------------------
DROP TABLE IF EXISTS "QuestionTransformation";
CREATE TABLE "QuestionTransformation" (
  "ConditionId" varchar,
  "RefQuestionId" varchar(36),
  "ResponseType" varchar,
  "Response" varchar,
  "ResponseComplex" varchar,
  "Group" float,
  "ActionId" varchar,
  "Content" varchar,
  "QuestionId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "QuestionTransformation"
-- ----------------------------
INSERT INTO "QuestionTransformation" VALUES ('Pre', '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2abb4-6246-11e7-907b-a6006ad3dba0', '', NULL, 'Rm', '00c2b2f8-6246-11e7-907b-a6006ad3dba0', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6203de42-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionTransformation" VALUES ('Pre', '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2ac90-6246-11e7-907b-a6006ad3dba0', '', NULL, 'Rm', '00c2b3c0-6246-11e7-907b-a6006ad3dba0', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6203df82-6260-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for QuestionValidation
-- ----------------------------
DROP TABLE IF EXISTS "QuestionValidation";
CREATE TABLE "QuestionValidation" (
  "ValidatorId" varchar,
  "ValidatorTypeId" varchar,
  "Revision" integer,
  "MinLimit" varchar,
  "MaxLimit" varchar,
  "QuestionId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "QuestionValidation"
-- ----------------------------
INSERT INTO "QuestionValidation" VALUES ('Required', 'None', 0, '', '', '6206a9a6-6260-11e7-907b-a6006ad3dba0', '62069a60-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionValidation" VALUES ('Required', 'None', 0, '', '', '6206aa78-6260-11e7-907b-a6006ad3dba0', '62069b3c-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionValidation" VALUES ('Required', 'None', 0, '', '', '6206ab4a-6260-11e7-907b-a6006ad3dba0', '62069fe2-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionValidation" VALUES ('Required', 'None', 0, '', '', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6206a10e-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionValidation" VALUES ('Required', 'None', 0, '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '6206a1f4-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionValidation" VALUES ('Range', 'Numeric', 0, 1, 5, '6206ab4a-6260-11e7-907b-a6006ad3dba0', '6206a2d0-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionValidation" VALUES ('Range', 'Count', 0, 2, '', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6206a3a2-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO "QuestionValidation" VALUES ('Range', 'Numeric', 1, 3, 5, '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6206a4a7-6260-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for RelationshipType
-- ----------------------------
DROP TABLE IF EXISTS "RelationshipType";
CREATE TABLE "RelationshipType" (
  "Name" varchar,
  "Description" varchar,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "RelationshipType"
-- ----------------------------
INSERT INTO "RelationshipType" VALUES ('Partner', 'Partner', 'Partner', 0);

-- ----------------------------
-- Table structure for SubCounty
-- ----------------------------
DROP TABLE IF EXISTS "SubCounty";
CREATE TABLE "SubCounty" (
  "Name" varchar,
  "CountyId" integer,
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "SubCounty"
-- ----------------------------
INSERT INTO "SubCounty" VALUES ('Kibera', 47, '7e516014-6b99-11e7-907b-a6006ad3dba0', 0);

-- ----------------------------
-- Table structure for User
-- ----------------------------
DROP TABLE IF EXISTS "User";
CREATE TABLE "User" (
  "UserName" varchar,
  "Password" varchar,
  "PracticeId" varchar(36),
  "PersonId" varchar(36),
  "Id" varchar(36) NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "User"
-- ----------------------------
INSERT INTO "User" VALUES ('UserName1', 'Password1', '7e51629e-6b99-11e7-907b-a6006ad3dba0', 'b4d18679-ed7e-4e02-8cc5-a7b7016df233', '61a9e04c-2ed0-414a-9387-a7b7016df233', 0);

-- ----------------------------
-- Table structure for Validator
-- ----------------------------
DROP TABLE IF EXISTS "Validator";
CREATE TABLE "Validator" (
  "Name" varchar,
  "Rank" float,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "Validator"
-- ----------------------------
INSERT INTO "Validator" VALUES ('Required', 0.0, 'Required', 0);
INSERT INTO "Validator" VALUES ('Range', 0.0, 'Range', 0);

-- ----------------------------
-- Table structure for ValidatorType
-- ----------------------------
DROP TABLE IF EXISTS "ValidatorType";
CREATE TABLE "ValidatorType" (
  "Name" varchar,
  "Id" varchar NOT NULL,
  "Voided" integer,
  PRIMARY KEY ("Id")
);

-- ----------------------------
-- Records of "ValidatorType"
-- ----------------------------
INSERT INTO "ValidatorType" VALUES ('None', 'None', 0);
INSERT INTO "ValidatorType" VALUES ('Numeric', 'Numeric', 0);
INSERT INTO "ValidatorType" VALUES ('Count', 'Count', 0);

-- ----------------------------
-- Indexes structure for table CategoryItem
-- ----------------------------
CREATE INDEX "CategoryItem_CategoryId"
ON "CategoryItem" (
  "CategoryId" ASC
);
CREATE INDEX "CategoryItem_ItemId"
ON "CategoryItem" (
  "ItemId" ASC
);

-- ----------------------------
-- Indexes structure for table Client
-- ----------------------------
CREATE INDEX "Client_PersonId"
ON "Client" (
  "PersonId" ASC
);
CREATE INDEX "Client_PracticeId"
ON "Client" (
  "PracticeId" ASC
);

-- ----------------------------
-- Indexes structure for table ClientIdentifier
-- ----------------------------
CREATE INDEX "ClientIdentifier_ClientId"
ON "ClientIdentifier" (
  "ClientId" ASC
);
CREATE INDEX "ClientIdentifier_IdentifierTypeId"
ON "ClientIdentifier" (
  "IdentifierTypeId" ASC
);

-- ----------------------------
-- Indexes structure for table Concept
-- ----------------------------
CREATE INDEX "Concept_CategoryId"
ON "Concept" (
  "CategoryId" ASC
);
CREATE INDEX "Concept_ConceptTypeId"
ON "Concept" (
  "ConceptTypeId" ASC
);

-- ----------------------------
-- Indexes structure for table Encounter
-- ----------------------------
CREATE INDEX "Encounter_ClientId"
ON "Encounter" (
  "ClientId" ASC
);
CREATE INDEX "Encounter_DeviceId"
ON "Encounter" (
  "DeviceId" ASC
);
CREATE INDEX "Encounter_EncounterTypeId"
ON "Encounter" (
  "EncounterTypeId" ASC
);
CREATE INDEX "Encounter_FormId"
ON "Encounter" (
  "FormId" ASC
);
CREATE INDEX "Encounter_PracticeId"
ON "Encounter" (
  "PracticeId" ASC
);
CREATE INDEX "Encounter_ProviderId"
ON "Encounter" (
  "ProviderId" ASC
);
CREATE INDEX "Encounter_UserId"
ON "Encounter" (
  "UserId" ASC
);

-- ----------------------------
-- Indexes structure for table Form
-- ----------------------------
CREATE INDEX "Form_ModuleId"
ON "Form" (
  "ModuleId" ASC
);

-- ----------------------------
-- Indexes structure for table Obs
-- ----------------------------
CREATE INDEX "Obs_EncounterId"
ON "Obs" (
  "EncounterId" ASC
);

-- ----------------------------
-- Indexes structure for table PersonAddress
-- ----------------------------
CREATE INDEX "PersonAddress_CountyId"
ON "PersonAddress" (
  "CountyId" ASC
);
CREATE INDEX "PersonAddress_PersonId"
ON "PersonAddress" (
  "PersonId" ASC
);

-- ----------------------------
-- Indexes structure for table PersonContact
-- ----------------------------
CREATE INDEX "PersonContact_PersonId"
ON "PersonContact" (
  "PersonId" ASC
);

-- ----------------------------
-- Indexes structure for table Provider
-- ----------------------------
CREATE INDEX "Provider_PersonId"
ON "Provider" (
  "PersonId" ASC
);
CREATE INDEX "Provider_PracticeId"
ON "Provider" (
  "PracticeId" ASC
);
CREATE INDEX "Provider_ProviderTypeId"
ON "Provider" (
  "ProviderTypeId" ASC
);

-- ----------------------------
-- Indexes structure for table Question
-- ----------------------------
CREATE INDEX "Question_ConceptId"
ON "Question" (
  "ConceptId" ASC
);
CREATE INDEX "Question_FormId"
ON "Question" (
  "FormId" ASC
);

-- ----------------------------
-- Indexes structure for table QuestionBranch
-- ----------------------------
CREATE INDEX "QuestionBranch_ActionId"
ON "QuestionBranch" (
  "ActionId" ASC
);
CREATE INDEX "QuestionBranch_ConditionId"
ON "QuestionBranch" (
  "ConditionId" ASC
);
CREATE INDEX "QuestionBranch_GotoQuestionId"
ON "QuestionBranch" (
  "GotoQuestionId" ASC
);
CREATE INDEX "QuestionBranch_QuestionId"
ON "QuestionBranch" (
  "QuestionId" ASC
);
CREATE INDEX "QuestionBranch_RefQuestionId"
ON "QuestionBranch" (
  "RefQuestionId" ASC
);

-- ----------------------------
-- Indexes structure for table QuestionReValidation
-- ----------------------------
CREATE INDEX "QuestionReValidation_ActionId"
ON "QuestionReValidation" (
  "ActionId" ASC
);
CREATE INDEX "QuestionReValidation_ConditionId"
ON "QuestionReValidation" (
  "ConditionId" ASC
);
CREATE INDEX "QuestionReValidation_QuestionValidationId"
ON "QuestionReValidation" (
  "QuestionValidationId" ASC
);
CREATE INDEX "QuestionReValidation_RefQuestionId"
ON "QuestionReValidation" (
  "RefQuestionId" ASC
);

-- ----------------------------
-- Indexes structure for table QuestionRemoteTransformation
-- ----------------------------
CREATE INDEX "QuestionRemoteTransformation_ActionId"
ON "QuestionRemoteTransformation" (
  "ActionId" ASC
);
CREATE INDEX "QuestionRemoteTransformation_ClientAttributeId"
ON "QuestionRemoteTransformation" (
  "ClientAttributeId" ASC
);
CREATE INDEX "QuestionRemoteTransformation_ConditionId"
ON "QuestionRemoteTransformation" (
  "ConditionId" ASC
);
CREATE INDEX "QuestionRemoteTransformation_QuestionId"
ON "QuestionRemoteTransformation" (
  "QuestionId" ASC
);
CREATE INDEX "QuestionRemoteTransformation_RemoteQuestionId"
ON "QuestionRemoteTransformation" (
  "RemoteQuestionId" ASC
);
CREATE INDEX "QuestionRemoteTransformation_SelfQuestionId"
ON "QuestionRemoteTransformation" (
  "SelfQuestionId" ASC
);

-- ----------------------------
-- Indexes structure for table QuestionTransformation
-- ----------------------------
CREATE INDEX "QuestionTransformation_ActionId"
ON "QuestionTransformation" (
  "ActionId" ASC
);
CREATE INDEX "QuestionTransformation_ConditionId"
ON "QuestionTransformation" (
  "ConditionId" ASC
);
CREATE INDEX "QuestionTransformation_QuestionId"
ON "QuestionTransformation" (
  "QuestionId" ASC
);
CREATE INDEX "QuestionTransformation_RefQuestionId"
ON "QuestionTransformation" (
  "RefQuestionId" ASC
);

-- ----------------------------
-- Indexes structure for table QuestionValidation
-- ----------------------------
CREATE INDEX "QuestionValidation_QuestionId"
ON "QuestionValidation" (
  "QuestionId" ASC
);
CREATE INDEX "QuestionValidation_ValidatorId"
ON "QuestionValidation" (
  "ValidatorId" ASC
);
CREATE INDEX "QuestionValidation_ValidatorTypeId"
ON "QuestionValidation" (
  "ValidatorTypeId" ASC
);

-- ----------------------------
-- Indexes structure for table SubCounty
-- ----------------------------
CREATE INDEX "SubCounty_CountyId"
ON "SubCounty" (
  "CountyId" ASC
);

-- ----------------------------
-- Indexes structure for table User
-- ----------------------------
CREATE INDEX "User_PersonId"
ON "User" (
  "PersonId" ASC
);
CREATE INDEX "User_PracticeId"
ON "User" (
  "PracticeId" ASC
);

PRAGMA foreign_keys = true;
