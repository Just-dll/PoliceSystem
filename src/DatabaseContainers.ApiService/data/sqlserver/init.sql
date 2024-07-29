IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'main')
BEGIN
  CREATE DATABASE main;
END;
GO

USE main;
GO	

CREATE TABLE [case_file_connection_type] (
    [id] int NOT NULL IDENTITY,
    [value] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_case_file_connection_type] PRIMARY KEY ([id])
);
GO


CREATE TABLE [case_file_type] (
    [id] int NOT NULL IDENTITY,
    [value] nvarchar(max) NULL,
    CONSTRAINT [PK_case_file_type] PRIMARY KEY ([id])
);
GO


CREATE TABLE [user] (
    [id] int NOT NULL IDENTITY,
    [IdentityId] int NOT NULL,
    CONSTRAINT [PK__user__3213E83F956FB020] PRIMARY KEY ([id])
);
GO


CREATE TABLE [case_file] (
    [id] int NOT NULL IDENTITY,
    [initation_date] date NOT NULL,
    [case_file_type_id] int NOT NULL,
    CONSTRAINT [PK_case_file_3002032] PRIMARY KEY ([id]),
    CONSTRAINT [FK_case_caseType_] FOREIGN KEY ([case_file_type_id]) REFERENCES [case_file_type] ([id])
);
GO


CREATE TABLE [decision] (
    [id] int NOT NULL IDENTITY,
    [judge_id] int NOT NULL,
    [description] nvarchar(max) NULL,
    [issue_date] date NOT NULL,
    CONSTRAINT [PK_decision] PRIMARY KEY ([id]),
    CONSTRAINT [FK_decision_user_judge_id] FOREIGN KEY ([judge_id]) REFERENCES [user] ([id])
);
GO


CREATE TABLE [driving_license] (
    [id] int NOT NULL IDENTITY,
    [driver_id] int NOT NULL,
    [issue_date] date NOT NULL,
    [expiration_date] date NOT NULL,
    CONSTRAINT [PK__driving___3213E83F5F21BAB8] PRIMARY KEY ([id]),
    CONSTRAINT [FK_driver_id] FOREIGN KEY ([driver_id]) REFERENCES [user] ([id])
);
GO


CREATE TABLE [registered_car] (
    [id] int NOT NULL IDENTITY,
    [plate_number] varchar(50) NOT NULL,
    [model] varchar(50) NOT NULL,
    [color] varchar(50) NOT NULL,
    [year] date NOT NULL,
    [car_owner_id] int NOT NULL,
    CONSTRAINT [PK__register__3213E83F2999EE23] PRIMARY KEY ([id]),
    CONSTRAINT [FK_car_owner_ref] FOREIGN KEY ([car_owner_id]) REFERENCES [user] ([id])
);
GO


CREATE TABLE [requests] (
    [id] int NOT NULL IDENTITY,
    [requester_id] int NOT NULL,
    [initiation_date] date NOT NULL,
    [status] int NOT NULL,
    [description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_requests] PRIMARY KEY ([id]),
    CONSTRAINT [FK_requests_user_requester_id] FOREIGN KEY ([requester_id]) REFERENCES [user] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [case_file_assignation] (
    [id] int NOT NULL IDENTITY,
    [CaseFileId] int NOT NULL,
    [PersonId] int NOT NULL,
    [CaseFileConnectionTypeId] int NOT NULL,
    CONSTRAINT [PK_case_file_assignation] PRIMARY KEY ([id]),
    CONSTRAINT [FK_case_file_assignation_case_file_CaseFileId] FOREIGN KEY ([CaseFileId]) REFERENCES [case_file] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_case_file_assignation_case_file_connection_type_CaseFileConnectionTypeId] FOREIGN KEY ([CaseFileConnectionTypeId]) REFERENCES [case_file_connection_type] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_case_file_assignation_user_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [user] ([id]) ON DELETE CASCADE
);
GO


CREATE TABLE [report] (
    [id] int NOT NULL IDENTITY,
    [date_of_issuing] date NOT NULL,
    [report_file_location] char(250) NULL,
    [issuer_id] int NOT NULL,
    [description] nvarchar(max) NULL,
    [case_file_id] int NULL,
    CONSTRAINT [PK__report__3213E83F90184F01] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Issuer_Report_ref] FOREIGN KEY ([issuer_id]) REFERENCES [user] ([id]),
    CONSTRAINT [FK_report_case_file_case_file_id] FOREIGN KEY ([case_file_id]) REFERENCES [case_file] ([id])
);
GO


CREATE TABLE [warrant] (
    [id] int NOT NULL,
    [suspect_id] int NOT NULL,
    [case_file_id] int NOT NULL,
    CONSTRAINT [PK_warrant] PRIMARY KEY ([id]),
    CONSTRAINT [FK_warrant_case_file_case_file_id] FOREIGN KEY ([case_file_id]) REFERENCES [case_file] ([id]),
    CONSTRAINT [FK_warrant_decision_id] FOREIGN KEY ([id]) REFERENCES [decision] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_warrant_user_suspect_id] FOREIGN KEY ([suspect_id]) REFERENCES [user] ([id])
);
GO


CREATE TABLE [ticket] (
    [id] int NOT NULL IDENTITY,
    [report_id] int NOT NULL,
    [violator_id] int NOT NULL,
    [fine] decimal(18,2) NOT NULL,
    CONSTRAINT [PK__ticket__3213E83FE0964E0E] PRIMARY KEY ([id]),
    CONSTRAINT [FK_ticket_report_id] FOREIGN KEY ([report_id]) REFERENCES [report] ([id]),
    CONSTRAINT [FK_ticket_violator] FOREIGN KEY ([violator_id]) REFERENCES [user] ([id])
);
GO


CREATE INDEX [IX_case_file_case_file_type_id] ON [case_file] ([case_file_type_id]);
GO


CREATE INDEX [IX_case_file_assignation_CaseFileConnectionTypeId] ON [case_file_assignation] ([CaseFileConnectionTypeId]);
GO


CREATE INDEX [IX_case_file_assignation_CaseFileId] ON [case_file_assignation] ([CaseFileId]);
GO


CREATE INDEX [IX_case_file_assignation_PersonId] ON [case_file_assignation] ([PersonId]);
GO


CREATE INDEX [IX_decision_judge_id] ON [decision] ([judge_id]);
GO


CREATE UNIQUE INDEX [UQ__driving___A411C5BCEA9DB9D2] ON [driving_license] ([driver_id]);
GO


CREATE INDEX [IX_registered_car_car_owner_id] ON [registered_car] ([car_owner_id]);
GO


CREATE UNIQUE INDEX [UQ__register__87EF9F5903CD33E6] ON [registered_car] ([plate_number]);
GO


CREATE INDEX [IX_report_case_file_id] ON [report] ([case_file_id]);
GO


CREATE INDEX [IX_report_issuer_id] ON [report] ([issuer_id]);
GO


CREATE INDEX [IX_requests_requester_id] ON [requests] ([requester_id]);
GO


CREATE INDEX [IX_ticket_report_id] ON [ticket] ([report_id]);
GO


CREATE INDEX [IX_ticket_violator_id] ON [ticket] ([violator_id]);
GO


CREATE UNIQUE INDEX [IX_user_IdentityId] ON [user] ([IdentityId]);
GO


CREATE INDEX [IX_warrant_case_file_id] ON [warrant] ([case_file_id]);
GO


CREATE INDEX [IX_warrant_suspect_id] ON [warrant] ([suspect_id]);
GO


IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'identitydb')
BEGIN
  CREATE DATABASE identitydb;
END;
GO

USE identitydb;
GO	


CREATE TABLE [AspNetRoles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [AspNetUsers] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [AspNetUserTokens] (
    [UserId] int NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO


CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO


CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO


CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO


CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO


CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO


CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

