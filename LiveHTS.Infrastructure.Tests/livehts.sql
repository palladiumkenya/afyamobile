/* Disable foreign keys */
PRAGMA foreign_keys = 'off';

/* Begin transaction */
BEGIN;

/* Database properties */
PRAGMA auto_vacuum = 0;
PRAGMA encoding = 'UTF-8';
PRAGMA page_size = 4096;

/* Drop table [Action] */
DROP TABLE IF EXISTS [main].[Action];

/* Table structure [Action] */
CREATE TABLE [main].[Action](
    [Name] varchar, 
    [Id] varchar PRIMARY KEY NOT NULL, 
    [Voided] integer);

/* Drop table [Category] */
DROP TABLE IF EXISTS [main].[Category];

/* Table structure [Category] */
CREATE TABLE [main].[Category](
    [Code] varchar, 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);

/* Drop table [CategoryItem] */
DROP TABLE IF EXISTS [main].[CategoryItem];

/* Table structure [CategoryItem] */
CREATE TABLE [main].[CategoryItem](
    [CategoryId] varchar(36), 
    [ItemId] varchar(36), 
    [Display] varchar, 
    [Rank] float, 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);
CREATE INDEX [main].[CategoryItem_CategoryId]
ON [CategoryItem](
    [CategoryId]);
CREATE INDEX [main].[CategoryItem_ItemId]
ON [CategoryItem](
    [ItemId]);

/* Drop table [Concept] */
DROP TABLE IF EXISTS [main].[Concept];

/* Table structure [Concept] */
CREATE TABLE [main].[Concept](
    [Name] varchar, 
    [ConceptTypeId] varchar, 
    [CategoryId] varchar(36), 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);
CREATE INDEX [main].[Concept_ConceptTypeId]
ON [Concept](
    [ConceptTypeId]);
CREATE INDEX [main].[Concept_CategoryId]
ON [Concept](
    [CategoryId]);

/* Drop table [ConceptType] */
DROP TABLE IF EXISTS [main].[ConceptType];

/* Table structure [ConceptType] */
CREATE TABLE [main].[ConceptType](
    [Name] varchar, 
    [Id] varchar PRIMARY KEY NOT NULL, 
    [Voided] integer);

/* Drop table [Condition] */
DROP TABLE IF EXISTS [main].[Condition];

/* Table structure [Condition] */
CREATE TABLE [main].[Condition](
    [Name] varchar, 
    [Id] varchar PRIMARY KEY NOT NULL, 
    [Voided] integer);

/* Drop table [Form] */
DROP TABLE IF EXISTS [main].[Form];

/* Table structure [Form] */
CREATE TABLE [main].[Form](
    [Name] varchar, 
    [Display] varchar, 
    [Description] varchar, 
    [Rank] float, 
    [ModuleId] varchar(36), 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);
CREATE INDEX [main].[Form_ModuleId]
ON [Form](
    [ModuleId]);

/* Drop table [Item] */
DROP TABLE IF EXISTS [main].[Item];

/* Table structure [Item] */
CREATE TABLE [main].[Item](
    [Code] varchar, 
    [Display] varchar, 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);

/* Drop table [Module] */
DROP TABLE IF EXISTS [main].[Module];

/* Table structure [Module] */
CREATE TABLE [main].[Module](
    [Name] varchar, 
    [Display] varchar, 
    [Description] varchar, 
    [Rank] float, 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);

/* Drop table [Question] */
DROP TABLE IF EXISTS [main].[Question];

/* Table structure [Question] */
CREATE TABLE [main].[Question](
    [ConceptId] varchar(36), 
    [Ordinal] varchar, 
    [Display] varchar, 
    [Rank] float, 
    [FormId] varchar(36), 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);
CREATE INDEX [main].[Question_ConceptId]
ON [Question](
    [ConceptId]);
CREATE INDEX [main].[Question_FormId]
ON [Question](
    [FormId]);

/* Drop table [QuestionBranch] */
DROP TABLE IF EXISTS [main].[QuestionBranch];

/* Table structure [QuestionBranch] */
CREATE TABLE [main].[QuestionBranch](
    [ConditionId] varchar, 
    [ResponseType] varchar, 
    [Response] varchar, 
    [ResponseComplex] varchar, 
    [Group] float, 
    [ActionId] varchar, 
    [GotoQuestionId] varchar(36), 
    [QuestionId] varchar(36), 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);
CREATE INDEX [main].[QuestionBranch_GotoQuestionId]
ON [QuestionBranch](
    [GotoQuestionId]);
CREATE INDEX [main].[QuestionBranch_QuestionId]
ON [QuestionBranch](
    [QuestionId]);
CREATE INDEX [main].[QuestionBranch_ActionId]
ON [QuestionBranch](
    [ActionId]);
CREATE INDEX [main].[QuestionBranch_ConditionId]
ON [QuestionBranch](
    [ConditionId]);

/* Drop table [QuestionRemoteTransformation] */
DROP TABLE IF EXISTS [main].[QuestionRemoteTransformation];

/* Table structure [QuestionRemoteTransformation] */
CREATE TABLE [main].[QuestionRemoteTransformation](
    [ConditionId] varchar, 
    [SubjectAttributeId] varchar, 
    [RemoteQuestionId] varchar(36), 
    [SelfQuestionId] varchar(36), 
    [ResponseType] varchar, 
    [Response] varchar, 
    [ResponseComplex] varchar, 
    [Group] float, 
    [ActionId] varchar, 
    [Content] varchar, 
    [AltContent] varchar, 
    [QuestionId] varchar(36), 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);
CREATE INDEX [main].[QuestionRemoteTransformation_QuestionId]
ON [QuestionRemoteTransformation](
    [QuestionId]);
CREATE INDEX [main].[QuestionRemoteTransformation_SubjectAttributeId]
ON [QuestionRemoteTransformation](
    [SubjectAttributeId]);
CREATE INDEX [main].[QuestionRemoteTransformation_RemoteQuestionId]
ON [QuestionRemoteTransformation](
    [RemoteQuestionId]);
CREATE INDEX [main].[QuestionRemoteTransformation_ConditionId]
ON [QuestionRemoteTransformation](
    [ConditionId]);
CREATE INDEX [main].[QuestionRemoteTransformation_SelfQuestionId]
ON [QuestionRemoteTransformation](
    [SelfQuestionId]);
CREATE INDEX [main].[QuestionRemoteTransformation_ActionId]
ON [QuestionRemoteTransformation](
    [ActionId]);

/* Drop table [QuestionReValidation] */
DROP TABLE IF EXISTS [main].[QuestionReValidation];

/* Table structure [QuestionReValidation] */
CREATE TABLE [main].[QuestionReValidation](
    [ConditionId] varchar, 
    [RefQuestionId] varchar(36), 
    [ResponseType] varchar, 
    [Response] varchar, 
    [ResponseComplex] varchar, 
    [Group] float, 
    [ActionId] varchar, 
    [QuestionValidationId] varchar(36), 
    [QuestionId] varchar(36), 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);
CREATE INDEX [main].[QuestionReValidation_ActionId]
ON [QuestionReValidation](
    [ActionId]);
CREATE INDEX [main].[QuestionReValidation_RefQuestionId]
ON [QuestionReValidation](
    [RefQuestionId]);
CREATE INDEX [main].[QuestionReValidation_ConditionId]
ON [QuestionReValidation](
    [ConditionId]);
CREATE INDEX [main].[QuestionReValidation_QuestionValidationId]
ON [QuestionReValidation](
    [QuestionValidationId]);

/* Drop table [QuestionTransformation] */
DROP TABLE IF EXISTS [main].[QuestionTransformation];

/* Table structure [QuestionTransformation] */
CREATE TABLE [main].[QuestionTransformation](
    [ConditionId] varchar, 
    [RefQuestionId] varchar(36), 
    [ResponseType] varchar, 
    [Response] varchar, 
    [ResponseComplex] varchar, 
    [Group] float, 
    [ActionId] varchar, 
    [Content] varchar, 
    [QuestionId] varchar(36), 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);
CREATE INDEX [main].[QuestionTransformation_ConditionId]
ON [QuestionTransformation](
    [ConditionId]);
CREATE INDEX [main].[QuestionTransformation_ActionId]
ON [QuestionTransformation](
    [ActionId]);
CREATE INDEX [main].[QuestionTransformation_QuestionId]
ON [QuestionTransformation](
    [QuestionId]);
CREATE INDEX [main].[QuestionTransformation_RefQuestionId]
ON [QuestionTransformation](
    [RefQuestionId]);

/* Drop table [QuestionValidation] */
DROP TABLE IF EXISTS [main].[QuestionValidation];

/* Table structure [QuestionValidation] */
CREATE TABLE [main].[QuestionValidation](
    [ValidatorId] varchar, 
    [ValidatorTypeId] varchar, 
    [Revision] integer, 
    [MinLimit] varchar, 
    [MaxLimit] varchar, 
    [QuestionId] varchar(36), 
    [Id] varchar(36) PRIMARY KEY NOT NULL, 
    [Voided] integer);
CREATE INDEX [main].[QuestionValidation_ValidatorTypeId]
ON [QuestionValidation](
    [ValidatorTypeId]);
CREATE INDEX [main].[QuestionValidation_ValidatorId]
ON [QuestionValidation](
    [ValidatorId]);
CREATE INDEX [main].[QuestionValidation_QuestionId]
ON [QuestionValidation](
    [QuestionId]);

/* Drop table [SubjectAttribute] */
DROP TABLE IF EXISTS [main].[SubjectAttribute];

/* Table structure [SubjectAttribute] */
CREATE TABLE [main].[SubjectAttribute](
    [Name] varchar, 
    [Id] varchar PRIMARY KEY NOT NULL, 
    [Voided] integer);

/* Drop table [Validator] */
DROP TABLE IF EXISTS [main].[Validator];

/* Table structure [Validator] */
CREATE TABLE [main].[Validator](
    [Name] varchar, 
    [Rank] float, 
    [Id] varchar PRIMARY KEY NOT NULL, 
    [Voided] integer);

/* Drop table [ValidatorType] */
DROP TABLE IF EXISTS [main].[ValidatorType];

/* Table structure [ValidatorType] */
CREATE TABLE [main].[ValidatorType](
    [Name] varchar, 
    [Id] varchar PRIMARY KEY NOT NULL, 
    [Voided] integer);

/* Empty table [Action] */
DELETE FROM
    [main].[Action];

/* Table data [Action] Record count: 4 */
INSERT INTO [Action]([rowid], [Name], [Id], [Voided]) VALUES(1, 'None', 'None', 0);
INSERT INTO [Action]([rowid], [Name], [Id], [Voided]) VALUES(2, 'Rng', 'Rng', 0);
INSERT INTO [Action]([rowid], [Name], [Id], [Voided]) VALUES(3, 'Rm', 'Rm', 0);
INSERT INTO [Action]([rowid], [Name], [Id], [Voided]) VALUES(4, 'Set', 'Set', 0);

/* Empty table [Category] */
DELETE FROM
    [main].[Category];

/* Table data [Category] Record count: 3 */
INSERT INTO [Category]([rowid], [Code], [Id], [Voided]) VALUES(1, 'YesNo', '62040a3e-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Category]([rowid], [Code], [Id], [Voided]) VALUES(2, 'Result', '62040b24-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Category]([rowid], [Code], [Id], [Voided]) VALUES(3, 'Services', '62040c00-6260-11e7-907b-a6006ad3dba0', 0);

/* Empty table [CategoryItem] */
DELETE FROM
    [main].[CategoryItem];

/* Table data [CategoryItem] Record count: 9 */
INSERT INTO [CategoryItem]([rowid], [CategoryId], [ItemId], [Display], [Rank], [Id], [Voided]) VALUES(1, '62040a3e-6260-11e7-907b-a6006ad3dba0', '00c2a902-6246-11e7-907b-a6006ad3dba0', '', 1, '6206b720-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [CategoryItem]([rowid], [CategoryId], [ItemId], [Display], [Rank], [Id], [Voided]) VALUES(2, '62040a3e-6260-11e7-907b-a6006ad3dba0', '00c2aae2-6246-11e7-907b-a6006ad3dba0', '', 2, '6206b8b0-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [CategoryItem]([rowid], [CategoryId], [ItemId], [Display], [Rank], [Id], [Voided]) VALUES(3, '62040b24-6260-11e7-907b-a6006ad3dba0', '00c2abb4-6246-11e7-907b-a6006ad3dba0', '', 1, '6206bd06-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [CategoryItem]([rowid], [CategoryId], [ItemId], [Display], [Rank], [Id], [Voided]) VALUES(4, '62040b24-6260-11e7-907b-a6006ad3dba0', '00c2ac90-6246-11e7-907b-a6006ad3dba0', '', 2, '6206be1e-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [CategoryItem]([rowid], [CategoryId], [ItemId], [Display], [Rank], [Id], [Voided]) VALUES(5, '62040b24-6260-11e7-907b-a6006ad3dba0', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', 3, '6206befa-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [CategoryItem]([rowid], [CategoryId], [ItemId], [Display], [Rank], [Id], [Voided]) VALUES(6, '62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b2f8-6246-11e7-907b-a6006ad3dba0', '', 1, '6206bfcc-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [CategoryItem]([rowid], [CategoryId], [ItemId], [Display], [Rank], [Id], [Voided]) VALUES(7, '62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b3c0-6246-11e7-907b-a6006ad3dba0', '', 2, '6206c0a8-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [CategoryItem]([rowid], [CategoryId], [ItemId], [Display], [Rank], [Id], [Voided]) VALUES(8, '62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b4f8-6246-11e7-907b-a6006ad3dba0', '', 3, '6206c184-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [CategoryItem]([rowid], [CategoryId], [ItemId], [Display], [Rank], [Id], [Voided]) VALUES(9, '62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b488-6246-11e7-907b-a6006ad3dba0', '', 4, '6206c256-6260-11e7-907b-a6006ad3dba0', 0);

/* Empty table [Concept] */
DELETE FROM
    [main].[Concept];

/* Table data [Concept] Record count: 6 */
INSERT INTO [Concept]([rowid], [Name], [ConceptTypeId], [CategoryId], [Id], [Voided]) VALUES(1, 'Consent', 'Single', '62040a3e-6260-11e7-907b-a6006ad3dba0', '00c2a60a-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Concept]([rowid], [Name], [ConceptTypeId], [CategoryId], [Id], [Voided]) VALUES(2, 'Result', 'Single', '62040b24-6260-11e7-907b-a6006ad3dba0', '00c2aa06-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Concept]([rowid], [Name], [ConceptTypeId], [CategoryId], [Id], [Voided]) VALUES(3, 'No of Kits', 'Numeric', null, '00c2b14a-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Concept]([rowid], [Name], [ConceptTypeId], [CategoryId], [Id], [Voided]) VALUES(4, 'Referall', 'Multi', '62040c00-6260-11e7-907b-a6006ad3dba0', '00c2b23a-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Concept]([rowid], [Name], [ConceptTypeId], [CategoryId], [Id], [Voided]) VALUES(5, 'Discordant', 'Single', '62040a3e-6260-11e7-907b-a6006ad3dba0', '6203cad8-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Concept]([rowid], [Name], [ConceptTypeId], [CategoryId], [Id], [Voided]) VALUES(6, 'Remarks', 'Text', null, '00c2b550-6246-11e7-907b-a6006ad3dba0', 0);

/* Empty table [ConceptType] */
DELETE FROM
    [main].[ConceptType];

/* Table data [ConceptType] Record count: 4 */
INSERT INTO [ConceptType]([rowid], [Name], [Id], [Voided]) VALUES(1, 'Single', 'Single', 0);
INSERT INTO [ConceptType]([rowid], [Name], [Id], [Voided]) VALUES(2, 'Numeric', 'Numeric', 0);
INSERT INTO [ConceptType]([rowid], [Name], [Id], [Voided]) VALUES(3, 'Multi', 'Multi', 0);
INSERT INTO [ConceptType]([rowid], [Name], [Id], [Voided]) VALUES(4, 'Text', 'Text', 0);

/* Empty table [Condition] */
DELETE FROM
    [main].[Condition];

/* Table data [Condition] Record count: 2 */
INSERT INTO [Condition]([rowid], [Name], [Id], [Voided]) VALUES(1, 'Post', 'Post', 0);
INSERT INTO [Condition]([rowid], [Name], [Id], [Voided]) VALUES(2, 'Pre', 'Pre', 0);

/* Empty table [Form] */
DELETE FROM
    [main].[Form];

/* Table data [Form] Record count: 2 */
INSERT INTO [Form]([rowid], [Name], [Display], [Description], [Rank], [ModuleId], [Id], [Voided]) VALUES(1, 'HTS Lab Form', 'HTS Lab Form', 'HTS Lab Form', 1, '62040ce6-6260-11e7-907b-a6006ad3dba0', '62040dcc-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Form]([rowid], [Name], [Display], [Description], [Rank], [ModuleId], [Id], [Voided]) VALUES(2, 'HTS Linkage Form', 'HTS Linkage Form', 'HTS Linkage Form', 2, '62040ce6-6260-11e7-907b-a6006ad3dba0', '62040eb2-6260-11e7-907b-a6006ad3dba0', 0);

/* Empty table [Item] */
DELETE FROM
    [main].[Item];

/* Table data [Item] Record count: 9 */
INSERT INTO [Item]([rowid], [Code], [Display], [Id], [Voided]) VALUES(1, 'Y', 'Y', '00c2a902-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Item]([rowid], [Code], [Display], [Id], [Voided]) VALUES(2, 'N', 'N', '00c2aae2-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Item]([rowid], [Code], [Display], [Id], [Voided]) VALUES(3, 'Pos', 'Pos', '00c2abb4-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Item]([rowid], [Code], [Display], [Id], [Voided]) VALUES(4, 'Neg', 'Neg', '00c2ac90-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Item]([rowid], [Code], [Display], [Id], [Voided]) VALUES(5, 'Inc', 'Inc', '00c2ad58-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Item]([rowid], [Code], [Display], [Id], [Voided]) VALUES(6, 'PrEP', 'PrEP', '00c2b2f8-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Item]([rowid], [Code], [Display], [Id], [Voided]) VALUES(7, 'CCC', 'CCC', '00c2b3c0-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Item]([rowid], [Code], [Display], [Id], [Voided]) VALUES(8, 'Counselling', 'Counselling', '00c2b4f8-6246-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Item]([rowid], [Code], [Display], [Id], [Voided]) VALUES(9, 'Compulsory', 'Compulsory', '00c2b488-6246-11e7-907b-a6006ad3dba0', 0);

/* Empty table [Module] */
DELETE FROM
    [main].[Module];

/* Table data [Module] Record count: 1 */
INSERT INTO [Module]([rowid], [Name], [Display], [Description], [Rank], [Id], [Voided]) VALUES(1, 'HTS Module', 'Hiv Testing Services Module', 'Hiv Testing Services Module', 1, '62040ce6-6260-11e7-907b-a6006ad3dba0', 0);

/* Empty table [Question] */
DELETE FROM
    [main].[Question];

/* Table data [Question] Record count: 6 */
INSERT INTO [Question]([rowid], [ConceptId], [Ordinal], [Display], [Rank], [FormId], [Id], [Voided]) VALUES(1, '00c2a60a-6246-11e7-907b-a6006ad3dba0', '1', 'Consent', 1, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206a9a6-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Question]([rowid], [ConceptId], [Ordinal], [Display], [Rank], [FormId], [Id], [Voided]) VALUES(2, '00c2aa06-6246-11e7-907b-a6006ad3dba0', '2', 'Result', 2, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206aa78-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Question]([rowid], [ConceptId], [Ordinal], [Display], [Rank], [FormId], [Id], [Voided]) VALUES(3, '00c2b14a-6246-11e7-907b-a6006ad3dba0', '3', 'No of Kits', 3, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206ab4a-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Question]([rowid], [ConceptId], [Ordinal], [Display], [Rank], [FormId], [Id], [Voided]) VALUES(4, '00c2b23a-6246-11e7-907b-a6006ad3dba0', '4', 'Referall', 4, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206ac1c-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Question]([rowid], [ConceptId], [Ordinal], [Display], [Rank], [FormId], [Id], [Voided]) VALUES(5, '6203cad8-6260-11e7-907b-a6006ad3dba0', '5', 'Discordant', 5, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206acf8-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [Question]([rowid], [ConceptId], [Ordinal], [Display], [Rank], [FormId], [Id], [Voided]) VALUES(6, '00c2b550-6246-11e7-907b-a6006ad3dba0', '6', 'Remarks', 5, '62040dcc-6260-11e7-907b-a6006ad3dba0', '6206b13a-6260-11e7-907b-a6006ad3dba0', 0);

/* Empty table [QuestionBranch] */
DELETE FROM
    [main].[QuestionBranch];

/* Table data [QuestionBranch] Record count: 2 */
INSERT INTO [QuestionBranch]([rowid], [ConditionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [GotoQuestionId], [QuestionId], [Id], [Voided]) VALUES(1, 'Post', '=', '00c2aae2-6246-11e7-907b-a6006ad3dba0', '', 0, 'None', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6206a9a6-6260-11e7-907b-a6006ad3dba0', '6203d8de-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionBranch]([rowid], [ConditionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [GotoQuestionId], [QuestionId], [Id], [Voided]) VALUES(2, 'Post', '=', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', 0, 'None', '6206b13a-6260-11e7-907b-a6006ad3dba0', '6206aa78-6260-11e7-907b-a6006ad3dba0', '6203dd3e-6260-11e7-907b-a6006ad3dba0', 0);

/* Empty table [QuestionRemoteTransformation] */
DELETE FROM
    [main].[QuestionRemoteTransformation];

/* Table data [QuestionRemoteTransformation] Record count: 6 */
INSERT INTO [QuestionRemoteTransformation]([rowid], [ConditionId], [SubjectAttributeId], [RemoteQuestionId], [SelfQuestionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [Content], [AltContent], [QuestionId], [Id], [Voided]) VALUES(1, 'Pre', 'Partner', '6206aa78-6260-11e7-907b-a6006ad3dba0', null, '!=', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', 1, '', '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1b46-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionRemoteTransformation]([rowid], [ConditionId], [SubjectAttributeId], [RemoteQuestionId], [SelfQuestionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [Content], [AltContent], [QuestionId], [Id], [Voided]) VALUES(2, 'Pre', 'Partner', null, '6206aa78-6260-11e7-907b-a6006ad3dba0', '!=', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', 1, '', '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1c72-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionRemoteTransformation]([rowid], [ConditionId], [SubjectAttributeId], [RemoteQuestionId], [SelfQuestionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [Content], [AltContent], [QuestionId], [Id], [Voided]) VALUES(3, 'Pre', 'Partner', '6206aa78-6260-11e7-907b-a6006ad3dba0', null, '=', '00c2abb4-6246-11e7-907b-a6006ad3dba0', '', 2, '', '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1d4e-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionRemoteTransformation]([rowid], [ConditionId], [SubjectAttributeId], [RemoteQuestionId], [SelfQuestionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [Content], [AltContent], [QuestionId], [Id], [Voided]) VALUES(4, 'Pre', 'Partner', null, '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2ac90-6246-11e7-907b-a6006ad3dba0', '', 2, 'Set', '00c2a902-6246-11e7-907b-a6006ad3dba0', '00c2aae2-6246-11e7-907b-a6006ad3dba0', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1e16-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionRemoteTransformation]([rowid], [ConditionId], [SubjectAttributeId], [RemoteQuestionId], [SelfQuestionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [Content], [AltContent], [QuestionId], [Id], [Voided]) VALUES(5, 'Pre', 'Partner', '6206aa78-6260-11e7-907b-a6006ad3dba0', null, '=', '00c2ac90-6246-11e7-907b-a6006ad3dba0', '', 3, '', '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1ede-67a9-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionRemoteTransformation]([rowid], [ConditionId], [SubjectAttributeId], [RemoteQuestionId], [SelfQuestionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [Content], [AltContent], [QuestionId], [Id], [Voided]) VALUES(6, 'Pre', 'Partner', null, '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2abb4-6246-11e7-907b-a6006ad3dba0', '', 3, 'Set', '00c2a902-6246-11e7-907b-a6006ad3dba0', '00c2aae2-6246-11e7-907b-a6006ad3dba0', '6206acf8-6260-11e7-907b-a6006ad3dba0', '9f6e1f9c-67a9-11e7-907b-a6006ad3dba0', 0);

/* Empty table [QuestionReValidation] */
DELETE FROM
    [main].[QuestionReValidation];

/* Table data [QuestionReValidation] Record count: 1 */
INSERT INTO [QuestionReValidation]([rowid], [ConditionId], [RefQuestionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [QuestionValidationId], [QuestionId], [Id], [Voided]) VALUES(1, 'Pre', '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2ad58-6246-11e7-907b-a6006ad3dba0', '', null, 'Rng', '6206a2d0-6260-11e7-907b-a6006ad3dba0', '6206ab4a-6260-11e7-907b-a6006ad3dba0', '6203e068-6260-11e7-907b-a6006ad3dba0', 0);

/* Empty table [QuestionTransformation] */
DELETE FROM
    [main].[QuestionTransformation];

/* Table data [QuestionTransformation] Record count: 2 */
INSERT INTO [QuestionTransformation]([rowid], [ConditionId], [RefQuestionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [Content], [QuestionId], [Id], [Voided]) VALUES(1, 'Pre', '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2abb4-6246-11e7-907b-a6006ad3dba0', '', null, 'Rm', '00c2b2f8-6246-11e7-907b-a6006ad3dba0', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6203de42-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionTransformation]([rowid], [ConditionId], [RefQuestionId], [ResponseType], [Response], [ResponseComplex], [Group], [ActionId], [Content], [QuestionId], [Id], [Voided]) VALUES(2, 'Pre', '6206aa78-6260-11e7-907b-a6006ad3dba0', '=', '00c2ac90-6246-11e7-907b-a6006ad3dba0', '', null, 'Rm', '00c2b3c0-6246-11e7-907b-a6006ad3dba0', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6203df82-6260-11e7-907b-a6006ad3dba0', 0);

/* Empty table [QuestionValidation] */
DELETE FROM
    [main].[QuestionValidation];

/* Table data [QuestionValidation] Record count: 8 */
INSERT INTO [QuestionValidation]([rowid], [ValidatorId], [ValidatorTypeId], [Revision], [MinLimit], [MaxLimit], [QuestionId], [Id], [Voided]) VALUES(1, 'Required', 'None', 0, '', '', '6206a9a6-6260-11e7-907b-a6006ad3dba0', '62069a60-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionValidation]([rowid], [ValidatorId], [ValidatorTypeId], [Revision], [MinLimit], [MaxLimit], [QuestionId], [Id], [Voided]) VALUES(2, 'Required', 'None', 0, '', '', '6206aa78-6260-11e7-907b-a6006ad3dba0', '62069b3c-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionValidation]([rowid], [ValidatorId], [ValidatorTypeId], [Revision], [MinLimit], [MaxLimit], [QuestionId], [Id], [Voided]) VALUES(3, 'Required', 'None', 0, '', '', '6206ab4a-6260-11e7-907b-a6006ad3dba0', '62069fe2-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionValidation]([rowid], [ValidatorId], [ValidatorTypeId], [Revision], [MinLimit], [MaxLimit], [QuestionId], [Id], [Voided]) VALUES(4, 'Required', 'None', 0, '', '', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6206a10e-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionValidation]([rowid], [ValidatorId], [ValidatorTypeId], [Revision], [MinLimit], [MaxLimit], [QuestionId], [Id], [Voided]) VALUES(5, 'Required', 'None', 0, '', '', '6206acf8-6260-11e7-907b-a6006ad3dba0', '6206a1f4-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionValidation]([rowid], [ValidatorId], [ValidatorTypeId], [Revision], [MinLimit], [MaxLimit], [QuestionId], [Id], [Voided]) VALUES(6, 'Range', 'Numeric', 0, '1', '5', '6206ab4a-6260-11e7-907b-a6006ad3dba0', '6206a2d0-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionValidation]([rowid], [ValidatorId], [ValidatorTypeId], [Revision], [MinLimit], [MaxLimit], [QuestionId], [Id], [Voided]) VALUES(7, 'Range', 'Count', 0, '2', '', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6206a3a2-6260-11e7-907b-a6006ad3dba0', 0);
INSERT INTO [QuestionValidation]([rowid], [ValidatorId], [ValidatorTypeId], [Revision], [MinLimit], [MaxLimit], [QuestionId], [Id], [Voided]) VALUES(8, 'Range', 'Numeric', 1, '3', '5', '6206ac1c-6260-11e7-907b-a6006ad3dba0', '6206a4a7-6260-11e7-907b-a6006ad3dba0', 0);

/* Empty table [SubjectAttribute] */
DELETE FROM
    [main].[SubjectAttribute];

/* Table data [SubjectAttribute] Record count: 1 */
INSERT INTO [SubjectAttribute]([rowid], [Name], [Id], [Voided]) VALUES(1, 'Partner', 'Partner', 0);

/* Empty table [Validator] */
DELETE FROM
    [main].[Validator];

/* Table data [Validator] Record count: 2 */
INSERT INTO [Validator]([rowid], [Name], [Rank], [Id], [Voided]) VALUES(1, 'Required', 0, 'Required', 0);
INSERT INTO [Validator]([rowid], [Name], [Rank], [Id], [Voided]) VALUES(2, 'Range', 0, 'Range', 0);

/* Empty table [ValidatorType] */
DELETE FROM
    [main].[ValidatorType];

/* Table data [ValidatorType] Record count: 3 */
INSERT INTO [ValidatorType]([rowid], [Name], [Id], [Voided]) VALUES(1, 'None', 'None', 0);
INSERT INTO [ValidatorType]([rowid], [Name], [Id], [Voided]) VALUES(2, 'Numeric', 'Numeric', 0);
INSERT INTO [ValidatorType]([rowid], [Name], [Id], [Voided]) VALUES(3, 'Count', 'Count', 0);

/* Commit transaction */
COMMIT;

/* Enable foreign keys */
PRAGMA foreign_keys = 'on';
