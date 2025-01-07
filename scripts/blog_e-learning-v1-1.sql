-- Tabla Users que será relacionada con la tabla de usuarios de Identity
CREATE TABLE Users (
    Id INT IDENTITY(1,1) NOT NULL,
    FirstName NVARCHAR(200) NOT NULL,
    MiddleName NVARCHAR(200) NULL,
    FirstSurname NVARCHAR(200) NOT NULL,
    SecondSurname NVARCHAR(200) NULL,
    CONSTRAINT PK_Users PRIMARY KEY (Id)
);

-- Tabla Permissions que será relacionada con la tabla de roles de Identity
CREATE TABLE Permissions (
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(300) NOT NULL,
    [Key] NVARCHAR(200) NOT NULL, 
    CONSTRAINT PK_Permissions PRIMARY KEY (Id)
);

-- Tabla Roles que extenderá la tabla de roles de Identity
CREATE TABLE Roles (
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(300) NOT NULL,
    [Key] NVARCHAR(200) NOT NULL,
    CONSTRAINT PK_Roles PRIMARY KEY (Id)
);

-- Tabla UserPermissions (Clave compuesta y restricción UNIQUE)
CREATE TABLE UserPermissions (
    UserId INT NOT NULL,
    PermissionId INT NOT NULL,
    CONSTRAINT PK_UserPermissions PRIMARY KEY (UserId, PermissionId), 
    CONSTRAINT FK_UserPermissions_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_UserPermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES Permissions(Id),
    CONSTRAINT UC_UserPermissions UNIQUE (UserId, PermissionId)
);

-- Tabla RolePermissions (Clave compuesta y restricción UNIQUE)
CREATE TABLE RolePermissions (
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    CONSTRAINT PK_RolePermissions PRIMARY KEY (RoleId, PermissionId),
    CONSTRAINT FK_RolePermissions_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id),
    CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES Permissions(Id),
    CONSTRAINT UC_RolePermissions UNIQUE (RoleId, PermissionId)
);

-- Tabla Posts
CREATE TABLE Posts (
    Id INT IDENTITY(1,1) NOT NULL,
    Title NVARCHAR(500) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Nombre corregido a CreatedAt
    UserId INT NOT NULL,
    CONSTRAINT PK_Posts PRIMARY KEY (Id),
    CONSTRAINT FK_Posts_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Tabla Comments (Modificada para permitir respuestas)
CREATE TABLE Comments (
    Id INT IDENTITY(1,1) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UserId INT NOT NULL,
    PostId INT NOT NULL,
    ParentCommentId INT NULL,
    CONSTRAINT PK_Comments PRIMARY KEY (Id),
    CONSTRAINT FK_Comments_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_Comments_Posts FOREIGN KEY (PostId) REFERENCES Posts(Id),
    CONSTRAINT FK_Comments_Comments FOREIGN KEY (ParentCommentId) REFERENCES Comments(Id)
);

-- Tabla InteractionTypes
CREATE TABLE InteractionTypes (
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(300) NOT NULL,
    Code NVARCHAR(100) NOT NULL UNIQUE, -- Nombre de columna corregido a Code
    CONSTRAINT PK_InteractionTypes PRIMARY KEY (Id)
);

-- Tabla PostInteractions
CREATE TABLE PostInteractions (
    Id INT IDENTITY(1,1) NOT NULL,
    [Value] NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UserId INT NOT NULL,
    PostId INT NOT NULL,
    InteractionTypeId INT NOT NULL,
    CONSTRAINT PK_PostInteractions PRIMARY KEY (Id),
    CONSTRAINT FK_PostInteractions_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_PostInteractions_Posts FOREIGN KEY (PostId) REFERENCES Posts(Id),
    CONSTRAINT FK_PostInteractions_InteractionTypes FOREIGN KEY (InteractionTypeId) REFERENCES InteractionTypes(Id)
);

-- Tabla CommentInteractions
CREATE TABLE CommentInteractions (
    Id INT IDENTITY(1,1) NOT NULL,
    [Value] NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UserId INT NOT NULL,
    CommentId INT NOT NULL,
    InteractionTypeId INT NOT NULL,
    CONSTRAINT PK_CommentInteractions PRIMARY KEY (Id),
    CONSTRAINT FK_CommentInteractions_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_CommentInteractions_Comments FOREIGN KEY (CommentId) REFERENCES Comments(Id),
    CONSTRAINT FK_CommentInteractions_InteractionTypes FOREIGN KEY (InteractionTypeId) REFERENCES InteractionTypes(Id)
);

CREATE INDEX IX_UserPermissions_UserId ON UserPermissions (UserId);
CREATE INDEX IX_UserPermissions_PermissionId ON UserPermissions (PermissionId);
CREATE INDEX IX_RolePermissions_RoleId ON RolePermissions (RoleId);
CREATE INDEX IX_RolePermissions_PermissionId ON RolePermissions (PermissionId);
CREATE INDEX IX_Posts_UserId ON Posts (UserId);
CREATE INDEX IX_Comments_UserId ON Comments (UserId);
CREATE INDEX IX_Comments_PostId ON Comments (PostId);
CREATE INDEX IX_Comments_ParentCommentId ON Comments (ParentCommentId);
CREATE INDEX IX_PostInteractions_UserId ON PostInteractions (UserId);
CREATE INDEX IX_PostInteractions_PostId ON PostInteractions (PostId);
CREATE INDEX IX_PostInteractions_InteractionTypeId ON PostInteractions (InteractionTypeId);
CREATE INDEX IX_CommentInteractions_UserId ON CommentInteractions (UserId);
CREATE INDEX IX_CommentInteractions_CommentId ON CommentInteractions (CommentId);
CREATE INDEX IX_CommentInteractions_InteractionTypeId ON CommentInteractions (InteractionTypeId);