/*
Navicat SQL Server Data Transfer

Source Server         : .@SQLServer
Source Server Version : 105000
Source Host           : .:1433
Source Database       : Colder.Fx.Core.AdminLTE
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 105000
File Encoding         : 65001

Date: 2019-06-04 21:57:50
*/


-- ----------------------------
-- Table structure for Base_AppSecret
-- ----------------------------
CREATE TABLE [Base_AppSecret] (
[Id] varchar(50) NOT NULL ,
[AppId] varchar(50) NULL ,
[AppSecret] varchar(50) NULL ,
[AppName] varchar(255) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'应用密钥'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'应用密钥'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'AppId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'应用Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'应用Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'AppSecret')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'应用密钥'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppSecret'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'应用密钥'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppSecret'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_AppSecret', 
'COLUMN', N'AppName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'应用名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'应用名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_AppSecret'
, @level2type = 'COLUMN', @level2name = N'AppName'
GO

-- ----------------------------
-- Records of Base_AppSecret
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_AppSecret] ([Id], [AppId], [AppSecret], [AppName]) VALUES (N'039e41170bc72-b89139b1-f3f4-430e-aed7-36b193d256dc', N'AppAdmin', N'7344a9c5-4f8c-4725-bde5-3fb99716f457', N'超级权限')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_DatabaseLink
-- ----------------------------
CREATE TABLE [Base_DatabaseLink] (
[Id] varchar(50) NOT NULL ,
[LinkName] varchar(50) NULL ,
[ConnectionStr] varchar(1000) NULL ,
[DbType] varchar(50) NULL ,
[SortNum] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'数据库连接'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'数据库连接'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'LinkName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'连接名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'LinkName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'连接名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'LinkName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'ConnectionStr')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'连接字符串'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'ConnectionStr'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'连接字符串'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'ConnectionStr'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'DbType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'数据库类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'DbType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'数据库类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'DbType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_DatabaseLink', 
'COLUMN', N'SortNum')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'排序编号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'SortNum'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'排序编号'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_DatabaseLink'
, @level2type = 'COLUMN', @level2name = N'SortNum'
GO

-- ----------------------------
-- Records of Base_DatabaseLink
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_DatabaseLink] ([Id], [LinkName], [ConnectionStr], [DbType], [SortNum]) VALUES (N'039e900bc6bbb-a0070d5c-1fc7-4cf0-a177-e3aebc4633c5', N'SqlServer', N'Data Source=.;Initial Catalog=Colder.Fx.Core.AdminLTE;Integrated Security=True', N'SqlServer', N'aa')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_PermissionAppId
-- ----------------------------
CREATE TABLE [Base_PermissionAppId] (
[Id] varchar(50) NOT NULL ,
[AppId] varchar(50) NULL ,
[PermissionValue] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionAppId', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'AppId权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'AppId权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionAppId', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionAppId', 
'COLUMN', N'AppId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'AppId'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'AppId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'AppId'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'AppId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionAppId', 
'COLUMN', N'PermissionValue')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionAppId'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
GO

-- ----------------------------
-- Records of Base_PermissionAppId
-- ----------------------------
BEGIN TRANSACTION
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_PermissionRole
-- ----------------------------
CREATE TABLE [Base_PermissionRole] (
[Id] varchar(50) NOT NULL ,
[RoleId] varchar(50) NULL ,
[PermissionValue] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionRole', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'角色权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'角色权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionRole', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionRole', 
'COLUMN', N'RoleId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'角色主键Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'RoleId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'角色主键Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'RoleId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionRole', 
'COLUMN', N'PermissionValue')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权限值'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionRole'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
GO

-- ----------------------------
-- Records of Base_PermissionRole
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_PermissionRole] ([Id], [RoleId], [PermissionValue]) VALUES (N'1133345848604889088', N'1133011663516209152', N'sysuser.search'), (N'1133345848604889089', N'1133011663516209152', N'sysuser.manage'), (N'1133345848604889090', N'1133011663516209152', N'sysrole.search'), (N'1133345848604889091', N'1133011663516209152', N'sysrole.manage')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_PermissionUser
-- ----------------------------
CREATE TABLE [Base_PermissionUser] (
[Id] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[PermissionValue] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionUser', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户权限表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionUser', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionUser', 
'COLUMN', N'UserId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户主键Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'UserId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户主键Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'UserId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_PermissionUser', 
'COLUMN', N'PermissionValue')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'权限'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'权限'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_PermissionUser'
, @level2type = 'COLUMN', @level2name = N'PermissionValue'
GO

-- ----------------------------
-- Records of Base_PermissionUser
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_PermissionUser] ([Id], [UserId], [PermissionValue]) VALUES (N'1133345814723301376', N'1133345545746780160', N'sysLog.search')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_SysLog
-- ----------------------------
CREATE TABLE [Base_SysLog] (
[Id] varchar(50) NOT NULL ,
[LogType] varchar(255) NULL ,
[LogContent] varchar(MAX) NULL ,
[OpUserName] varchar(255) NULL ,
[OpTime] datetime NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统日志表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统日志表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'LogType')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'LogType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志类型'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'LogType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'LogContent')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'LogContent'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志内容'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'LogContent'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'OpUserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'操作员用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'OpUserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'操作员用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'OpUserName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysLog', 
'COLUMN', N'OpTime')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志记录时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'OpTime'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志记录时间'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysLog'
, @level2type = 'COLUMN', @level2name = N'OpTime'
GO

-- ----------------------------
-- Records of Base_SysLog
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_SysLog] ([Id], [LogType], [LogContent], [OpUserName], [OpTime]) VALUES (N'1134061523694653440', N'系统用户管理', N'修改用户:312321', N'超级管理员', N'2019-05-30 19:38:31.750'), (N'1134084489014808576', N'系统用户管理', N'修改用户:312321', N'超级管理员', N'2019-05-30 21:09:47.110'), (N'1134629240688480256', N'系统用户管理', N'修改用户:312321', N'超级管理员', N'2019-06-01 09:14:26.030'), (N'1134629639390629888', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-01 09:16:01.087'), (N'1134629682306748416', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-01 09:16:11.320'), (N'1134629761109331968', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-01 09:16:30.107'), (N'1134630005599506432', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-01 09:17:28.400'), (N'1134630141855666176', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-01 09:18:00.887'), (N'1135549917687844864', N'系统异常', N'<br />1层错误:<br />&nbsp;&nbsp;消息:<br />&nbsp;&nbsp;&nbsp;&nbsp;不支持此操作!<br />&nbsp;&nbsp;位置:<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; at Coldairarrow.DataRepository.DbRepository.Delete_Sql[T](Expression`1 condition) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdminLTE\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 368<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; at Coldairarrow.Business.Base_SysManage.Base_SysRoleBusiness.SavePermission(String roleId, List`1 permissions) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdminLTE\src\Coldairarrow.Business\Business\Base_SysManage\Base_SysRoleBusiness.cs:line 90<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; at Coldairarrow.Util.Interceptor.Intercept(IInvocation invocation) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdminLTE\src\Coldairarrow.Util\DI\Interceptor.cs:line 24<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; at Coldairarrow.Web.Areas.Base_SysManage.Controllers.Base_SysRoleController.SavePermission(String roleId, String permissions) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdminLTE\src\Coldairarrow.Web\Areas\Base_SysManage\Controllers\Base_SysRoleController.cs:line 109<br /><br />', N'超级管理员', N'2019-06-03 22:12:52.533'), (N'1135550316536795136', N'系统异常', N'<br />1层错误:<br />&nbsp;&nbsp;消息:<br />&nbsp;&nbsp;&nbsp;&nbsp;不支持此操作!<br />&nbsp;&nbsp;位置:<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; at Coldairarrow.DataRepository.DbRepository.Delete_Sql[T](Expression`1 condition) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdminLTE\src\Coldairarrow.DataRepository\Repository\DbRepository.cs:line 368<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; at Coldairarrow.Business.Base_SysManage.PermissionManage.SetUserPermission(String userId, List`1 permissions) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdminLTE\src\Coldairarrow.Business\Business\Base_SysManage\PermissionManage.cs:line 257<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; at Coldairarrow.Web.Areas.Base_SysManage.Controllers.Base_UserController.SavePermission(String userId, String permissions) in D:\文档\0软件项目\GitHub\Colder.Fx.Core.AdminLTE\src\Coldairarrow.Web\Areas\Base_SysManage\Controllers\Base_UserController.cs:line 128<br /><br />', N'超级管理员', N'2019-06-03 22:14:27.627'), (N'1135905764288892928', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-04 21:46:52.973'), (N'1135905778398531584', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-04 21:46:56.337'), (N'1135905794324303872', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-04 21:47:00.133'), (N'1135905823822843904', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-04 21:47:07.167'), (N'1135905844412682240', N'系统用户管理', N'修改用户:xiaoming', N'超级管理员', N'2019-06-04 21:47:12.077')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_SysRole
-- ----------------------------
CREATE TABLE [Base_SysRole] (
[Id] varchar(50) NOT NULL ,
[RoleName] nvarchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysRole', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统角色'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统角色'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysRole', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_SysRole', 
'COLUMN', N'RoleName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'角色名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
, @level2type = 'COLUMN', @level2name = N'RoleName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'角色名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_SysRole'
, @level2type = 'COLUMN', @level2name = N'RoleName'
GO

-- ----------------------------
-- Records of Base_SysRole
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_SysRole] ([Id], [RoleName]) VALUES (N'1133011623854870528', N'超级管理员'), (N'1133011663516209152', N'部门管理员')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_UnitTest
-- ----------------------------
CREATE TABLE [Base_UnitTest] (
[Id] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[UserName] varchar(50) NULL ,
[Age] int NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest'
, @level2type = 'COLUMN', @level2name = N'Id'
GO

-- ----------------------------
-- Records of Base_UnitTest
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_UnitTest] ([Id], [UserId], [UserName], [Age]) VALUES (N'10', null, null, null), (N'1135907004167098368', N'1135907004167098369', N'超级管理员', N'22'), (N'13c290da-0830-435b-9b1e-b0510a842173', N'Admin', N'超级管理员', N'22')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_UnitTest_0
-- ----------------------------
CREATE TABLE [Base_UnitTest_0] (
[Id] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[UserName] varchar(50) NULL ,
[Age] int NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest_0', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_0'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_0'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest_0', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_0'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_0'
, @level2type = 'COLUMN', @level2name = N'Id'
GO

-- ----------------------------
-- Records of Base_UnitTest_0
-- ----------------------------
BEGIN TRANSACTION
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_UnitTest_1
-- ----------------------------
CREATE TABLE [Base_UnitTest_1] (
[Id] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[UserName] varchar(50) NULL ,
[Age] int NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest_1', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_1'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_1'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest_1', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_1'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_1'
, @level2type = 'COLUMN', @level2name = N'Id'
GO

-- ----------------------------
-- Records of Base_UnitTest_1
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_UnitTest_1] ([Id], [UserId], [UserName], [Age]) VALUES (N'affbf4a5-ce84-4a91-bb37-2a9a957c3967', N'Admin', N'1135907013855940608', N'22')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_UnitTest_2
-- ----------------------------
CREATE TABLE [Base_UnitTest_2] (
[Id] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[UserName] varchar(50) NULL ,
[Age] int NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest_2', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_2'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'单元测试表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_2'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_UnitTest_2', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_2'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_UnitTest_2'
, @level2type = 'COLUMN', @level2name = N'Id'
GO

-- ----------------------------
-- Records of Base_UnitTest_2
-- ----------------------------
BEGIN TRANSACTION
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_User
-- ----------------------------
CREATE TABLE [Base_User] (
[Id] varchar(50) NOT NULL ,
[UserName] varchar(255) NULL ,
[Password] varchar(255) NULL ,
[RealName] varchar(50) NULL ,
[Sex] int NULL ,
[Birthday] date NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'系统，用户表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'系统，用户表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'代理主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'UserName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'UserName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'用户名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'UserName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Password')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'密码'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Password'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'密码'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Password'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'RealName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'真实姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'RealName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'真实姓名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'RealName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Sex')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'性别(1为男，0为女)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Sex'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'性别(1为男，0为女)'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Sex'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Base_User', 
'COLUMN', N'Birthday')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'出生日期'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Birthday'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'出生日期'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Base_User'
, @level2type = 'COLUMN', @level2name = N'Birthday'
GO

-- ----------------------------
-- Records of Base_User
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Base_User] ([Id], [UserName], [Password], [RealName], [Sex], [Birthday]) VALUES (N'1133345545746780160', N'xiaoming', N'e10adc3949ba59abbe56e057f20f883e', N'xiaoming', N'1', null), (N'Admin', N'Admin', N'e10adc3949ba59abbe56e057f20f883e', N'超级管理员', N'1', N'2017-12-15')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Base_UserRoleMap
-- ----------------------------
CREATE TABLE [Base_UserRoleMap] (
[Id] varchar(50) NOT NULL ,
[UserId] varchar(50) NULL ,
[RoleId] varchar(50) NULL 
)


GO

-- ----------------------------
-- Records of Base_UserRoleMap
-- ----------------------------
BEGIN TRANSACTION
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Dev_Project
-- ----------------------------
CREATE TABLE [Dev_Project] (
[Id] varchar(50) NOT NULL ,
[ProjectId] varchar(50) NOT NULL ,
[ProjectName] varchar(255) NOT NULL ,
[ProjectTypeId] varchar(50) NULL ,
[ProjectManagerId] varchar(50) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'ProjectId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'ProjectName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectName'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'ProjectTypeId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目类型Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目类型Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_Project', 
'COLUMN', N'ProjectManagerId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目经理Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectManagerId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目经理Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_Project'
, @level2type = 'COLUMN', @level2name = N'ProjectManagerId'
GO

-- ----------------------------
-- Records of Dev_Project
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Dev_Project] ([Id], [ProjectId], [ProjectName], [ProjectTypeId], [ProjectManagerId]) VALUES (N'a', N'a', N'a', N'sadsa', N'a')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Table structure for Dev_ProjectType
-- ----------------------------
CREATE TABLE [Dev_ProjectType] (
[Id] varchar(50) NOT NULL ,
[ProjectTypeId] varchar(50) NULL ,
[ProjectTypeName] varchar(255) NULL 
)


GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_ProjectType', 
NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目类型表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目类型表'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_ProjectType', 
'COLUMN', N'Id')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'Id'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'自然主键'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'Id'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_ProjectType', 
'COLUMN', N'ProjectTypeId')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目类型Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeId'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目类型Id'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeId'
GO
IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
'SCHEMA', N'dbo', 
'TABLE', N'Dev_ProjectType', 
'COLUMN', N'ProjectTypeName')) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'项目类型名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeName'
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'项目类型名'
, @level0type = 'SCHEMA', @level0name = N'dbo'
, @level1type = 'TABLE', @level1name = N'Dev_ProjectType'
, @level2type = 'COLUMN', @level2name = N'ProjectTypeName'
GO

-- ----------------------------
-- Records of Dev_ProjectType
-- ----------------------------
BEGIN TRANSACTION
GO
INSERT INTO [Dev_ProjectType] ([Id], [ProjectTypeId], [ProjectTypeName]) VALUES (N'1133722179070988288', N'sadsa', N'sdsadasdsa')
GO
GO
COMMIT TRANSACTION
GO

-- ----------------------------
-- Procedure structure for spCloneTableStructure
-- ----------------------------
CREATE PROCEDURE [spCloneTableStructure]

@SourceTable            nvarchar(255),
@DestinationTable       nvarchar(255),
@PartionField           nvarchar(255) = '',
@SourceSchema           nvarchar(255) = 'dbo',  
@DestinationSchema      nvarchar(255) = 'dbo',    
@RecreateIfExists       bit = 1

AS
BEGIN

DECLARE @msg  nvarchar(200), @PartionScript nvarchar(255), @sql NVARCHAR(MAX)

    IF EXISTS(Select s.name As SchemaName, t.name As TableName
                        From sys.tables t
                        Inner Join sys.schemas s On t.schema_id = s.schema_id
                        Inner Join sys.partitions p on p.object_id = t.object_id
                        Where p.index_id In (0, 1) and t.name = @SourceTable
                        Group By s.name, t.name
                        Having Count(*) > 1)

        SET @PartionScript = ' ON [PS_PartitionByCompanyId]([' + @PartionField + '])'
    else
        SET @PartionScript = ''

SET NOCOUNT ON;
BEGIN TRY   
    SET @msg ='  CloneTable  ' + @DestinationTable + ' - Step 1, Drop table if exists. Timestamp: '  + CONVERT(NVARCHAR(50),GETDATE(),108)
     RAISERROR( @msg,0,1) WITH NOWAIT
    --drop the table
    if EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @DestinationTable)
    BEGIN
        if @RecreateIfExists = 1
            BEGIN
                exec('DROP TABLE [' + @DestinationSchema + '].[' + @DestinationTable + ']')
            END
        ELSE
            RETURN
    END

    SET @msg ='  CloneTable  ' + @DestinationTable + ' - Step 2, Create table. Timestamp: '  + CONVERT(NVARCHAR(50),GETDATE(),108)
    RAISERROR( @msg,0,1) WITH NOWAIT
    --create the table
    exec('SELECT TOP (0) * INTO [' + @DestinationTable + '] FROM [' + @SourceTable + ']')       

    --create primary key
    SET @msg ='  CloneTable  ' + @DestinationTable + ' - Step 3, Create primary key. Timestamp: '  + CONVERT(NVARCHAR(50),GETDATE(),108)
    RAISERROR( @msg,0,1) WITH NOWAIT
    DECLARE @PKSchema nvarchar(255), @PKName nvarchar(255),@count   INT
    SELECT TOP 1 @PKSchema = CONSTRAINT_SCHEMA, @PKName = CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_SCHEMA = @SourceSchema AND TABLE_NAME = @SourceTable AND CONSTRAINT_TYPE = 'PRIMARY KEY'
    IF NOT @PKSchema IS NULL AND NOT @PKName IS NULL
    BEGIN
        DECLARE @PKColumns nvarchar(MAX)
        SET @PKColumns = ''

        SELECT @PKColumns = @PKColumns + '[' + COLUMN_NAME + '],'
            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
            where TABLE_NAME = @SourceTable and TABLE_SCHEMA = @SourceSchema AND CONSTRAINT_SCHEMA = @PKSchema AND CONSTRAINT_NAME= @PKName
            ORDER BY ORDINAL_POSITION

        SET @PKColumns = LEFT(@PKColumns, LEN(@PKColumns) - 1)

        exec('ALTER TABLE [' + @DestinationSchema + '].[' + @DestinationTable + '] ADD  CONSTRAINT [PK_' + @DestinationTable + '] PRIMARY KEY CLUSTERED (' + @PKColumns + ')' + @PartionScript);
    END

    --create other indexes
    SET @msg ='  CloneTable  ' + @DestinationTable + ' - Step 4, Create Indexes. Timestamp: '  + CONVERT(NVARCHAR(50),GETDATE(),108)
    RAISERROR( @msg,0,1) WITH NOWAIT
    DECLARE @IndexId int, @IndexName nvarchar(255), @IsUnique bit, @IsUniqueConstraint bit, @FilterDefinition nvarchar(max), @type int

    set @count=0
    DECLARE indexcursor CURSOR FOR
    SELECT index_id, name, is_unique, is_unique_constraint, filter_definition, type FROM sys.indexes WHERE is_primary_key = 0 and object_id = object_id('[' + @SourceSchema + '].[' + @SourceTable + ']')
    OPEN indexcursor;
    FETCH NEXT FROM indexcursor INTO @IndexId, @IndexName, @IsUnique, @IsUniqueConstraint, @FilterDefinition, @type
    WHILE @@FETCH_STATUS = 0
       BEGIN
            set @count =@count +1
            DECLARE @Unique nvarchar(255)
            SET @Unique = CASE WHEN @IsUnique = 1 THEN ' UNIQUE ' ELSE '' END

            DECLARE @KeyColumns nvarchar(max), @IncludedColumns nvarchar(max)
            SET @KeyColumns = ''
            SET @IncludedColumns = ''

            select @KeyColumns = @KeyColumns + '[' + c.name + '] ' + CASE WHEN is_descending_key = 1 THEN 'DESC' ELSE 'ASC' END + ',' from sys.index_columns ic
            inner join sys.columns c ON c.object_id = ic.object_id and c.column_id = ic.column_id
            where index_id = @IndexId and ic.object_id = object_id('[' + @SourceSchema + '].[' + @SourceTable + ']') and key_ordinal > 0
            order by index_column_id

            select @IncludedColumns = @IncludedColumns + '[' + c.name + '],' from sys.index_columns ic
            inner join sys.columns c ON c.object_id = ic.object_id and c.column_id = ic.column_id
            where index_id = @IndexId and ic.object_id = object_id('[' + @SourceSchema + '].[' + @SourceTable + ']') and key_ordinal = 0
            order by index_column_id

            IF LEN(@KeyColumns) > 0
                SET @KeyColumns = LEFT(@KeyColumns, LEN(@KeyColumns) - 1)

            IF LEN(@IncludedColumns) > 0
            BEGIN
                SET @IncludedColumns = ' INCLUDE (' + LEFT(@IncludedColumns, LEN(@IncludedColumns) - 1) + ')'
            END

            IF @FilterDefinition IS NULL
                SET @FilterDefinition = ''
            ELSE
                SET @FilterDefinition = 'WHERE ' + @FilterDefinition + ' '

            SET @msg ='  CloneTable  ' + @DestinationTable + ' - Step 4.' + CONVERT(NVARCHAR(5),@count) + ', Create Index ' + @IndexName + '. Timestamp: '  + CONVERT(NVARCHAR(50),GETDATE(),108)
            RAISERROR( @msg,0,1) WITH NOWAIT

            if @type = 2
                SET @sql = 'CREATE ' + @Unique + ' NONCLUSTERED INDEX [' + @IndexName + '] ON [' + @DestinationSchema + '].[' + @DestinationTable + '] (' + @KeyColumns + ')' + @IncludedColumns + @FilterDefinition  + @PartionScript
            ELSE
                BEGIN
                    SET @sql = 'CREATE ' + @Unique + ' CLUSTERED INDEX [' + @IndexName + '] ON [' + @DestinationSchema + '].[' + @DestinationTable + '] (' + @KeyColumns + ')' + @IncludedColumns + @FilterDefinition + @PartionScript
                END
            EXEC (@sql)
            FETCH NEXT FROM indexcursor INTO @IndexId, @IndexName, @IsUnique, @IsUniqueConstraint, @FilterDefinition, @type
       END
    CLOSE indexcursor
    DEALLOCATE indexcursor

    --create constraints
    SET @msg ='  CloneTable  ' + @DestinationTable + ' - Step 5, Create constraints. Timestamp: '  + CONVERT(NVARCHAR(50),GETDATE(),108)
    RAISERROR( @msg,0,1) WITH NOWAIT
    DECLARE @ConstraintName nvarchar(max), @CheckClause nvarchar(max), @ColumnName NVARCHAR(255)
    DECLARE const_cursor CURSOR FOR
        SELECT
            REPLACE(dc.name, @SourceTable, @DestinationTable),[definition], c.name
        FROM sys.default_constraints dc
            INNER JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
        WHERE OBJECT_NAME(parent_object_id) =@SourceTable               
    OPEN const_cursor
    FETCH NEXT FROM const_cursor INTO @ConstraintName, @CheckClause, @ColumnName
    WHILE @@FETCH_STATUS = 0
       BEGIN
            exec('ALTER TABLE [' + @DestinationTable + '] ADD CONSTRAINT [' + @ConstraintName + '] DEFAULT ' + @CheckClause + ' FOR ' + @ColumnName)
            FETCH NEXT FROM const_cursor INTO @ConstraintName, @CheckClause, @ColumnName
       END;
    CLOSE const_cursor
    DEALLOCATE const_cursor                 


END TRY
    BEGIN CATCH
        IF (SELECT CURSOR_STATUS('global','indexcursor')) >= -1
        BEGIN
         DEALLOCATE indexcursor
        END

        IF (SELECT CURSOR_STATUS('global','const_cursor')) >= -1
        BEGIN
         DEALLOCATE const_cursor
        END


        PRINT 'Error Message: ' + ERROR_MESSAGE(); 
    END CATCH

END

GO

-- ----------------------------
-- Indexes structure for table Base_AppSecret
-- ----------------------------
CREATE CLUSTERED INDEX [AppId] ON [Base_AppSecret]
([AppId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_AppSecret
-- ----------------------------
ALTER TABLE [Base_AppSecret] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_DatabaseLink
-- ----------------------------
CREATE CLUSTERED INDEX [LinkName] ON [Base_DatabaseLink]
([LinkName] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_DatabaseLink
-- ----------------------------
ALTER TABLE [Base_DatabaseLink] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_PermissionAppId
-- ----------------------------
CREATE CLUSTERED INDEX [RoleId] ON [Base_PermissionAppId]
([AppId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_PermissionAppId
-- ----------------------------
ALTER TABLE [Base_PermissionAppId] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_PermissionRole
-- ----------------------------
CREATE CLUSTERED INDEX [RoleId] ON [Base_PermissionRole]
([RoleId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_PermissionRole
-- ----------------------------
ALTER TABLE [Base_PermissionRole] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_PermissionUser
-- ----------------------------
CREATE CLUSTERED INDEX [UserId] ON [Base_PermissionUser]
([UserId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_PermissionUser
-- ----------------------------
ALTER TABLE [Base_PermissionUser] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_SysLog
-- ----------------------------
CREATE CLUSTERED INDEX [OpTime] ON [Base_SysLog]
([OpTime] ASC) 
GO
CREATE INDEX [LogType] ON [Base_SysLog]
([LogType] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Base_SysLog
-- ----------------------------
ALTER TABLE [Base_SysLog] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_SysRole
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_SysRole
-- ----------------------------
ALTER TABLE [Base_SysRole] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Indexes structure for table Base_UnitTest
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE [Base_UnitTest] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Uniques structure for table Base_UnitTest
-- ----------------------------
ALTER TABLE [Base_UnitTest] ADD UNIQUE ([UserId] ASC)
GO

-- ----------------------------
-- Indexes structure for table Base_UnitTest_0
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UnitTest_0
-- ----------------------------
ALTER TABLE [Base_UnitTest_0] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Uniques structure for table Base_UnitTest_0
-- ----------------------------
ALTER TABLE [Base_UnitTest_0] ADD UNIQUE ([UserId] ASC)
GO

-- ----------------------------
-- Indexes structure for table Base_UnitTest_1
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UnitTest_1
-- ----------------------------
ALTER TABLE [Base_UnitTest_1] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Uniques structure for table Base_UnitTest_1
-- ----------------------------
ALTER TABLE [Base_UnitTest_1] ADD UNIQUE ([UserId] ASC)
GO

-- ----------------------------
-- Indexes structure for table Base_UnitTest_2
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UnitTest_2
-- ----------------------------
ALTER TABLE [Base_UnitTest_2] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Uniques structure for table Base_UnitTest_2
-- ----------------------------
ALTER TABLE [Base_UnitTest_2] ADD UNIQUE ([UserId] ASC)
GO

-- ----------------------------
-- Indexes structure for table Base_User
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_User
-- ----------------------------
ALTER TABLE [Base_User] ADD PRIMARY KEY NONCLUSTERED ([Id])
GO

-- ----------------------------
-- Uniques structure for table Base_User
-- ----------------------------
ALTER TABLE [Base_User] ADD UNIQUE ([UserName] ASC)
GO

-- ----------------------------
-- Indexes structure for table Base_UserRoleMap
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Base_UserRoleMap
-- ----------------------------
ALTER TABLE [Base_UserRoleMap] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Dev_Project
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Dev_Project
-- ----------------------------
ALTER TABLE [Dev_Project] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Dev_ProjectType
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Dev_ProjectType
-- ----------------------------
ALTER TABLE [Dev_ProjectType] ADD PRIMARY KEY ([Id])
GO
