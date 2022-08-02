﻿PRINT 'Insert PermissionTemplates'
GO

SET IDENTITY_INSERT [PermissionTemplates] ON

INSERT INTO [PermissionTemplates]
    ([PermissionTemplateId], [CreateDate], [ModifyDate], [Name]          , [PermissionsString])
VALUES
    (1                     , GETDATE()   , GETDATE()   , N'Всички права'            , N'OperationalMapAdminPermissions, SapInterfacePermissions,CanImport;MonitoringPermissions,CanRead;NewsPermissions,CanPublish;GuidancePermissions,CanCreate;CompanyPermissions,CanRead;CompanyPermissions,CanWrite;UserAdminPermissions,CanAdministrate;UserAdminPermissions,CanControl;RegistrationPermissions,CanRead;ContractRegistrationPermissions,CanRead;ActionLogPermissions,CanRead;SapInterfacePermissions,CanImport;InterfacesPermissions,CanExport;CertAuthorityCheckPermissions,CanRead;CertAuthorityCheckPermissions,CanWrite|1,OperationalMapPermissions,CanRead;1,OperationalMapPermissions,CanWrite;1,ProcedurePermissions,CanRead;1,ProcedurePermissions,CanWrite;1,ProcedurePermissions,CanCheck;1,ProcedurePermissions,CanDelete;1,ProjectPermissions,CanRead;1,ProjectPermissions,CanRegister;1,ContractPermissions,CanRead;1,ProjectPermissions,CanWithdraw;1,ContractPermissions,CanWrite;1,ContractPermissions,CanCheck;1,ContractCommunicationPermissions,CanRead;1,ContractCommunicationPermissions,CanWrite;1,ContractCommunicationPermissions,CanWrite;1,IndicatorPermissions,CanRead;1,IndicatorPermissions,CanWrite;1,SpotCheckPermissions,CanRead;1,SpotCheckPermissions,CanWrite;2,OperationalMapPermissions,CanRead;2,OperationalMapPermissions,CanWrite;2,ProcedurePermissions,CanRead;2,ProcedurePermissions,CanWrite;2,ProcedurePermissions,CanCheck;2,ProcedurePermissions,CanDelete;2,ProjectPermissions,CanRead;2,ProjectPermissions,CanRegister;2,ProjectPermissions,CanWithdraw;2,ContractPermissions,CanRead;2,ContractPermissions,CanWrite;2,ContractPermissions,CanCheck;2,ContractCommunicationPermissions,CanRead;2,ContractCommunicationPermissions,CanWrite;2,ContractCommunicationPermissions,CanWrite;2,IndicatorPermissions,CanRead;2,IndicatorPermissions,CanWrite;2,SpotCheckPermissions,CanRead;2,SpotCheckPermissions,CanWrite;3,OperationalMapPermissions,CanRead;3,OperationalMapPermissions,CanWrite;3,ProcedurePermissions,CanRead;3,ProcedurePermissions,CanWrite;3,ProcedurePermissions,CanCheck;3,ProcedurePermissions,CanDelete;3,ProjectPermissions,CanRead;3,ProjectPermissions,CanRegister;3,ProjectPermissions,CanWithdraw;3,ContractPermissions,CanRead;3,ContractPermissions,CanWrite;3,ContractPermissions,CanCheck;3,ContractCommunicationPermissions,CanRead;3,ContractCommunicationPermissions,CanWrite;3,ContractCommunicationPermissions,CanWrite;3,IndicatorPermissions,CanRead;3,IndicatorPermissions,CanWrite;3,SpotCheckPermissions,CanRead;3,SpotCheckPermissions,CanWrite;4,OperationalMapPermissions,CanRead;4,OperationalMapPermissions,CanWrite;4,ProcedurePermissions,CanRead;4,ProcedurePermissions,CanWrite;4,ProcedurePermissions,CanCheck;4,ProcedurePermissions,CanDelete;4,ProjectPermissions,CanRead;4,ProjectPermissions,CanRegister;4,ProjectPermissions,CanWithdraw;4,ContractPermissions,CanRead;4,ContractPermissions,CanWrite;4,ContractPermissions,CanCheck;4,ContractCommunicationPermissions,CanRead;4,ContractCommunicationPermissions,CanWrite;4,ContractCommunicationPermissions,CanWrite;4,IndicatorPermissions,CanRead;4,IndicatorPermissions,CanWrite;4,SpotCheckPermissions,CanRead;4,SpotCheckPermissions,CanWrite;5,OperationalMapPermissions,CanRead;5,OperationalMapPermissions,CanWrite;5,ProcedurePermissions,CanRead;5,ProcedurePermissions,CanWrite;5,ProcedurePermissions,CanCheck;5,ProcedurePermissions,CanDelete;5,ProjectPermissions,CanRead;5,ProjectPermissions,CanRegister;5,ProjectPermissions,CanWithdraw;5,ContractPermissions,CanRead;5,ContractPermissions,CanWrite;5,ContractPermissions,CanCheck;5,ContractCommunicationPermissions,CanRead;5,ContractCommunicationPermissions,CanWrite;5,ContractCommunicationPermissions,CanWrite;5,IndicatorPermissions,CanRead;5,IndicatorPermissions,CanWrite;5,SpotCheckPermissions,CanRead;5,SpotCheckPermissions,CanWrite;6,OperationalMapPermissions,CanRead;6,OperationalMapPermissions,CanWrite;6,ProcedurePermissions,CanRead;6,ProcedurePermissions,CanWrite;6,ProcedurePermissions,CanCheck;6,ProcedurePermissions,CanDelete;6,ProjectPermissions,CanRead;6,ProjectPermissions,CanRegister;6,ProjectPermissions,CanWithdraw;6,ContractPermissions,CanRead;6,ContractPermissions,CanWrite;6,ContractPermissions,CanCheck;6,ContractCommunicationPermissions,CanRead;6,ContractCommunicationPermissions,CanWrite;6,ContractCommunicationPermissions,CanWrite;6,IndicatorPermissions,CanRead;6,IndicatorPermissions,CanWrite;6,SpotCheckPermissions,CanRead;6,SpotCheckPermissions,CanWrite;7,OperationalMapPermissions,CanRead;7,OperationalMapPermissions,CanWrite;7,ProcedurePermissions,CanRead;7,ProcedurePermissions,CanWrite;7,ProcedurePermissions,CanCheck;7,ProcedurePermissions,CanDelete;7,ProjectPermissions,CanRead;7,ProjectPermissions,CanRegister;7,ProjectPermissions,CanWithdraw;7,ContractPermissions,CanRead;7,ContractPermissions,CanWrite;7,ContractPermissions,CanCheck;7,ContractCommunicationPermissions,CanRead;7,ContractCommunicationPermissions,CanWrite;7,IndicatorPermissions,CanRead;7,IndicatorPermissions,CanWrite;1,ContractReportPermissions,CanRead;1,ContractReportPermissions,CanWrite;1,ContractReportPermissions,CanCheck;2,ContractReportPermissions,CanRead;2,ContractReportPermissions,CanWrite;2,ContractReportPermissions,CanCheck;3,ContractReportPermissions,CanRead;3,ContractReportPermissions,CanWrite;3,ContractReportPermissions,CanCheck;4,ContractReportPermissions,CanRead;4,ContractReportPermissions,CanWrite;4,ContractReportPermissions,CanCheck;5,ContractReportPermissions,CanRead;5,ContractReportPermissions,CanWrite;5,ContractReportPermissions,CanCheck;6,ContractReportPermissions,CanRead;6,ContractReportPermissions,CanWrite;6,ContractReportPermissions,CanCheck;7,ContractReportPermissions,CanRead;7,ContractReportPermissions,CanWrite;7,ContractReportPermissions,CanCheck;7,ContractCommunicationPermissions,CanWrite;7,SpotCheckPermissions,CanRead;1,AuditPermissions,CanRead;1,AuditPermissions,CanWrite;2,AuditPermissions,CanRead;2,AuditPermissions,CanWrite;3,AuditPermissions,CanRead;3,AuditPermissions,CanWrite;4,AuditPermissions,CanRead;4,AuditPermissions,CanWrite;5,AuditPermissions,CanRead;5,AuditPermissions,CanWrite;6,AuditPermissions,CanRead;6,AuditPermissions,CanWrite;7,AuditPermissions,CanRead;7,AuditPermissions,CanWrite;1,MonitoringFinancialControlPermissions,CanRead;1,MonitoringFinancialControlPermissions,CanWriteFinancial;1,MonitoringFinancialControlPermissions,CanWriteTechnical;2,MonitoringFinancialControlPermissions,CanRead;2,MonitoringFinancialControlPermissions,CanWriteFinancial;2,MonitoringFinancialControlPermissions,CanWriteTechnical;3,MonitoringFinancialControlPermissions,CanRead;3,MonitoringFinancialControlPermissions,CanWriteFinancial;3,MonitoringFinancialControlPermissions,CanWriteTechnical;4,MonitoringFinancialControlPermissions,CanRead;4,MonitoringFinancialControlPermissions,CanWriteFinancial;4,MonitoringFinancialControlPermissions,CanWriteTechnical;5,MonitoringFinancialControlPermissions,CanRead;5,MonitoringFinancialControlPermissions,CanWriteFinancial;5,MonitoringFinancialControlPermissions,CanWriteTechnical;6,MonitoringFinancialControlPermissions,CanRead;6,MonitoringFinancialControlPermissions,CanWriteFinancial;6,MonitoringFinancialControlPermissions,CanWriteTechnical;7,MonitoringFinancialControlPermissions,CanRead;7,MonitoringFinancialControlPermissions,CanWriteFinancial;7,MonitoringFinancialControlPermissions,CanWriteTechnical;1,IrregularitySignalPermissions,CanRead;1,IrregularitySignalPermissions,CanWrite;2,IrregularitySignalPermissions,CanRead;2,IrregularitySignalPermissions,CanWrite;3,IrregularitySignalPermissions,CanRead;3,IrregularitySignalPermissions,CanWrite;4,IrregularitySignalPermissions,CanRead;4,IrregularitySignalPermissions,CanWrite;5,IrregularitySignalPermissions,CanRead;5,IrregularitySignalPermissions,CanWrite;6,IrregularitySignalPermissions,CanRead;6,IrregularitySignalPermissions,CanWrite;7,IrregularitySignalPermissions,CanRead;7,IrregularitySignalPermissions,CanWrite;1,IrregularityPermissions,CanRead;1,IrregularityPermissions,CanWrite;2,IrregularityPermissions,CanRead;2,IrregularityPermissions,CanWrite;3,IrregularityPermissions,CanRead;3,IrregularityPermissions,CanWrite;4,IrregularityPermissions,CanRead;4,IrregularityPermissions,CanWrite;5,IrregularityPermissions,CanRead;5,IrregularityPermissions,CanWrite;6,IrregularityPermissions,CanRead;6,IrregularityPermissions,CanWrite;7,IrregularityPermissions,CanRead;7,IrregularityPermissions,CanWrite;1,CertificationPermissions,CanRead;1,CertificationPermissions,CanWrite;2,CertificationPermissions,CanRead;2,CertificationPermissions,CanWrite;3,CertificationPermissions,CanRead;3,CertificationPermissions,CanWrite;4,CertificationPermissions,CanRead;4,CertificationPermissions,CanWrite;5,CertificationPermissions,CanRead;5,CertificationPermissions,CanWrite;6,CertificationPermissions,CanRead;6,CertificationPermissions,CanWrite;7,CertificationPermissions,CanRead;7,CertificationPermissions,CanWrite;1,EuReimbursedAmountPermissions,CanRead;1,EuReimbursedAmountPermissions,CanWrite;2,EuReimbursedAmountPermissions,CanRead;2,EuReimbursedAmountPermissions,CanWrite;3,EuReimbursedAmountPermissions,CanRead;3,EuReimbursedAmountPermissions,CanWrite;4,EuReimbursedAmountPermissions,CanRead;4,EuReimbursedAmountPermissions,CanWrite;5,EuReimbursedAmountPermissions,CanRead;5,EuReimbursedAmountPermissions,CanWrite;6,EuReimbursedAmountPermissions,CanRead;6,EuReimbursedAmountPermissions,CanWrite;7,EuReimbursedAmountPermissions,CanRead;7,EuReimbursedAmountPermissions,CanWrite;1,CertAuthorityCommunicationPermissions,CanRead;1,CertAuthorityCommunicationPermissions,CanWrite;2,CertAuthorityCommunicationPermissions,CanRead;2,CertAuthorityCommunicationPermissions,CanWrite;3,CertAuthorityCommunicationPermissions,CanRead;3,CertAuthorityCommunicationPermissions,CanWrite;4,CertAuthorityCommunicationPermissions,CanRead;4,CertAuthorityCommunicationPermissions,CanWrite;5,CertAuthorityCommunicationPermissions,CanRead;5,CertAuthorityCommunicationPermissions,CanWrite;6,CertAuthorityCommunicationPermissions,CanRead;6,CertAuthorityCommunicationPermissions,CanWrite,CanWrite;7,CertAuthorityCommunicationPermissions,CanRead;7,CertAuthorityCommunicationPermissions,CanWrite;1,AuditAuthorityCommunicationPermissions,CanRead;1,AuditAuthorityCommunicationPermissions,CanWrite;2,AuditAuthorityCommunicationPermissions,CanRead;2,AuditAuthorityCommunicationPermissions,CanWrite;3,AuditAuthorityCommunicationPermissions,CanRead;3,AuditAuthorityCommunicationPermissions,CanWrite;4,AuditAuthorityCommunicationPermissions,CanRead;4,AuditAuthorityCommunicationPermissions,CanWrite;5,AuditAuthorityCommunicationPermissions,CanRead;5,AuditAuthorityCommunicationPermissions,CanWrite;6,AuditAuthorityCommunicationPermissions,CanRead;6,AuditAuthorityCommunicationPermissions,CanWrite;7,AuditAuthorityCommunicationPermissions,CanRead;7,AuditAuthorityCommunicationPermissions,CanWrite;1,ProjectDossierPermissions,CanRead;2,ProjectDossierPermissions,CanRead;3,ProjectDossierPermissions,CanRead;4,ProjectDossierPermissions,CanRead;5,ProjectDossierPermissions,CanRead;6,ProjectDossierPermissions,CanRead;7,ProjectDossierPermissions,CanRead'),
    (2                     , GETDATE()   , GETDATE()   , N'Администратор от ОПДУ'   , N'SapInterfacePermissions,CanImport;MonitoringPermissions,CanRead;NewsPermissions,CanPublish;GuidancePermissions,CanCreate;CompanyPermissions,CanRead;CompanyPermissions,CanWrite;UserAdminPermissions,CanAdministrate;UserAdminPermissions,CanControl;CertAuthorityCheckPermissions,CanRead;CertAuthorityCheckPermissions,CanWrite|1,OperationalMapPermissions,CanRead;1,OperationalMapPermissions,CanWrite;1,ProcedurePermissions,CanRead;1,ProcedurePermissions,CanWrite;1,ProcedurePermissions,CanCheck;1,ProcedurePermissions,CanDelete;1,ProjectPermissions,CanRead;1,ProjectPermissions,CanRegister;1,ContractPermissions,CanRead;1,ContractPermissions,CanWrite;1,ProjectPermissions,CanWithdraw;1,ContractPermissions,CanCheck;1,ContractCommunicationPermissions,CanRead;1,ContractCommunicationPermissions,CanWrite;1,ContractReportPermissions,CanRead;1,ContractReportPermissions,CanWrite;1,ContractReportPermissions,CanCheck;1,SpotCheckPermissions,CanRead;1,SpotCheckPermissions,CanWrite;1,AuditPermissions,CanRead;1,AuditPermissions,CanWrite;1,MonitoringFinancialControlPermissions,CanRead;1,MonitoringFinancialControlPermissions,CanWriteFinancial;1,MonitoringFinancialControlPermissions,CanWriteTechnical;1,IrregularitySignalPermissions,CanRead;1,IrregularitySignalPermissions,CanWrite;1,IrregularityPermissions,CanRead;1,IrregularityPermissions,CanWrite;1,CertificationPermissions,CanRead;1,CertificationPermissions,CanWrite;1,EuReimbursedAmountPermissions,CanRead;1,EuReimbursedAmountPermissions,CanWrite;1,CertAuthorityCommunicationPermissions,CanRead;1,CertAuthorityCommunicationPermissions,CanWrite;1,AuditAuthorityCommunicationPermissions,CanRead;1,AuditAuthorityCommunicationPermissions,CanWrite;1,ProjectDossierPermissions,CanRead'),
    (3                     , GETDATE()   , GETDATE()   , N'Администратор от ОПТТИ'  , N'SapInterfacePermissions,CanImport;MonitoringPermissions,CanRead;NewsPermissions,CanPublish;GuidancePermissions,CanCreate;CompanyPermissions,CanRead;CompanyPermissions,CanWrite;UserAdminPermissions,CanAdministrate;UserAdminPermissions,CanControl;CertAuthorityCheckPermissions,CanRead;CertAuthorityCheckPermissions,CanWrite|2,OperationalMapPermissions,CanRead;2,OperationalMapPermissions,CanWrite;2,ProcedurePermissions,CanRead;2,ProcedurePermissions,CanWrite;2,ProcedurePermissions,CanCheck;2,ProcedurePermissions,CanDelete;2,ProjectPermissions,CanRead;2,ProjectPermissions,CanRegister;2,ContractPermissions,CanRead;2,ContractPermissions,CanWrite;2,ProjectPermissions,CanWithdraw;2,ContractPermissions,CanCheck;2,ContractCommunicationPermissions,CanRead;2,ContractCommunicationPermissions,CanWrite;2,ContractReportPermissions,CanRead;2,ContractReportPermissions,CanWrite;2,ContractReportPermissions,CanCheck;2,SpotCheckPermissions,CanRead;2,SpotCheckPermissions,CanWrite;2,AuditPermissions,CanRead;2,AuditPermissions,CanWrite;2,MonitoringFinancialControlPermissions,CanRead;2,MonitoringFinancialControlPermissions,CanWriteFinancial;2,MonitoringFinancialControlPermissions,CanWriteTechnical;2,IrregularitySignalPermissions,CanRead;2,IrregularitySignalPermissions,CanWrite;2,IrregularityPermissions,CanRead;2,IrregularityPermissions,CanWrite;2,CertificationPermissions,CanRead;2,CertificationPermissions,CanWrite;2,EuReimbursedAmountPermissions,CanRead;2,EuReimbursedAmountPermissions,CanWrite;2,CertAuthorityCommunicationPermissions,CanRead;2,CertAuthorityCommunicationPermissions,CanWrite;2,AuditAuthorityCommunicationPermissions,CanRead;2,AuditAuthorityCommunicationPermissions,CanWrite;2,ProjectDossierPermissions,CanRead'),
    (4                     , GETDATE()   , GETDATE()   , N'Администратор от ОПРР'   , N'SapInterfacePermissions,CanImport;MonitoringPermissions,CanRead;NewsPermissions,CanPublish;GuidancePermissions,CanCreate;CompanyPermissions,CanRead;CompanyPermissions,CanWrite;UserAdminPermissions,CanAdministrate;UserAdminPermissions,CanControl;CertAuthorityCheckPermissions,CanRead;CertAuthorityCheckPermissions,CanWrite|3,OperationalMapPermissions,CanRead;3,OperationalMapPermissions,CanWrite;3,ProcedurePermissions,CanRead;3,ProcedurePermissions,CanWrite;3,ProcedurePermissions,CanCheck;3,ProcedurePermissions,CanDelete;3,ProjectPermissions,CanRead;3,ProjectPermissions,CanRegister;3,ContractPermissions,CanRead;3,ContractPermissions,CanWrite;3,ProjectPermissions,CanWithdraw;3,ContractPermissions,CanCheck;3,ContractCommunicationPermissions,CanRead;3,ContractCommunicationPermissions,CanWrite;3,ContractReportPermissions,CanRead;3,ContractReportPermissions,CanWrite;3,ContractReportPermissions,CanCheck;3,SpotCheckPermissions,CanRead;3,SpotCheckPermissions,CanWrite;3,AuditPermissions,CanRead;3,AuditPermissions,CanWrite;3,MonitoringFinancialControlPermissions,CanRead;3,MonitoringFinancialControlPermissions,CanWriteFinancial;3,MonitoringFinancialControlPermissions,CanWriteTechnical;3,IrregularitySignalPermissions,CanRead;3,IrregularitySignalPermissions,CanWrite;3,IrregularityPermissions,CanRead;3,IrregularityPermissions,CanWrite;3,CertificationPermissions,CanRead;3,CertificationPermissions,CanWrite;3,EuReimbursedAmountPermissions,CanRead;3,EuReimbursedAmountPermissions,CanWrite;3,CertAuthorityCommunicationPermissions,CanRead;3,CertAuthorityCommunicationPermissions,CanWrite;3,AuditAuthorityCommunicationPermissions,CanRead;3,AuditAuthorityCommunicationPermissions,CanWrite;3,ProjectDossierPermissions,CanRead'),
    (5                     , GETDATE()   , GETDATE()   , N'Администратор от ОПРЧР'  , N'SapInterfacePermissions,CanImport;MonitoringPermissions,CanRead;NewsPermissions,CanPublish;GuidancePermissions,CanCreate;CompanyPermissions,CanRead;CompanyPermissions,CanWrite;UserAdminPermissions,CanAdministrate;UserAdminPermissions,CanControl;CertAuthorityCheckPermissions,CanRead;CertAuthorityCheckPermissions,CanWrite|4,OperationalMapPermissions,CanRead;4,OperationalMapPermissions,CanWrite;4,ProcedurePermissions,CanRead;4,ProcedurePermissions,CanWrite;4,ProcedurePermissions,CanCheck;4,ProcedurePermissions,CanDelete;4,ProjectPermissions,CanRead;4,ProjectPermissions,CanRegister;4,ContractPermissions,CanRead;4,ContractPermissions,CanWrite;4,ProjectPermissions,CanWithdraw;4,ContractPermissions,CanCheck;4,ContractCommunicationPermissions,CanRead;4,ContractCommunicationPermissions,CanWrite;4,ContractReportPermissions,CanRead;4,ContractReportPermissions,CanWrite;4,ContractReportPermissions,CanCheck;4,SpotCheckPermissions,CanRead;4,SpotCheckPermissions,CanWrite;4,AuditPermissions,CanRead;4,AuditPermissions,CanWrite;4,MonitoringFinancialControlPermissions,CanRead;4,MonitoringFinancialControlPermissions,CanWriteFinancial;4,MonitoringFinancialControlPermissions,CanWriteTechnical;4,IrregularitySignalPermissions,CanRead;4,IrregularitySignalPermissions,CanWrite;4,IrregularityPermissions,CanRead;4,IrregularityPermissions,CanWrite;4,CertificationPermissions,CanRead;4,CertificationPermissions,CanWrite;4,EuReimbursedAmountPermissions,CanRead;4,EuReimbursedAmountPermissions,CanWrite;4,CertAuthorityCommunicationPermissions,CanRead;4,CertAuthorityCommunicationPermissions,CanWrite;4,AuditAuthorityCommunicationPermissions,CanRead;4,AuditAuthorityCommunicationPermissions,CanWrite;4,ProjectDossierPermissions,CanRead'),
    (6                     , GETDATE()   , GETDATE()   , N'Администратор от ОПИК'   , N'SapInterfacePermissions,CanImport;MonitoringPermissions,CanRead;NewsPermissions,CanPublish;GuidancePermissions,CanCreate;CompanyPermissions,CanRead;CompanyPermissions,CanWrite;UserAdminPermissions,CanAdministrate;UserAdminPermissions,CanControl;CertAuthorityCheckPermissions,CanRead;CertAuthorityCheckPermissions,CanWrite|5,OperationalMapPermissions,CanRead;5,OperationalMapPermissions,CanWrite;5,ProcedurePermissions,CanRead;5,ProcedurePermissions,CanWrite;5,ProcedurePermissions,CanCheck;5,ProcedurePermissions,CanDelete;5,ProjectPermissions,CanRead;5,ProjectPermissions,CanRegister;5,ContractPermissions,CanRead;5,ContractPermissions,CanWrite;5,ProjectPermissions,CanWithdraw;5,ContractPermissions,CanCheck;5,ContractCommunicationPermissions,CanRead;5,ContractCommunicationPermissions,CanWrite;5,ContractReportPermissions,CanRead;5,ContractReportPermissions,CanWrite;5,ContractReportPermissions,CanCheck;5,SpotCheckPermissions,CanRead;5,SpotCheckPermissions,CanWrite;5,AuditPermissions,CanRead;5,AuditPermissions,CanWrite;5,MonitoringFinancialControlPermissions,CanRead;5,MonitoringFinancialControlPermissions,CanWriteFinancial;5,MonitoringFinancialControlPermissions,CanWriteTechnical;5,IrregularitySignalPermissions,CanRead;5,IrregularitySignalPermissions,CanWrite;5,IrregularityPermissions,CanRead;5,IrregularityPermissions,CanWrite;5,CertificationPermissions,CanRead;5,CertificationPermissions,CanWrite;5,EuReimbursedAmountPermissions,CanRead;5,EuReimbursedAmountPermissions,CanWrite;5,CertAuthorityCommunicationPermissions,CanRead;5,CertAuthorityCommunicationPermissions,CanWrite;5,AuditAuthorityCommunicationPermissions,CanRead;5,AuditAuthorityCommunicationPermissions,CanWrite;5,ProjectDossierPermissions,CanRead'),
    (7                     , GETDATE()   , GETDATE()   , N'Администратор от ОПОС'   , N'SapInterfacePermissions,CanImport;MonitoringPermissions,CanRead;NewsPermissions,CanPublish;GuidancePermissions,CanCreate;CompanyPermissions,CanRead;CompanyPermissions,CanWrite;UserAdminPermissions,CanAdministrate;UserAdminPermissions,CanControl;CertAuthorityCheckPermissions,CanRead;CertAuthorityCheckPermissions,CanWrite|6,OperationalMapPermissions,CanRead;6,OperationalMapPermissions,CanWrite;6,ProcedurePermissions,CanRead;6,ProcedurePermissions,CanWrite;6,ProcedurePermissions,CanCheck;6,ProcedurePermissions,CanDelete;6,ProjectPermissions,CanRead;6,ProjectPermissions,CanRegister;6,ContractPermissions,CanRead;6,ContractPermissions,CanWrite;6,ProjectPermissions,CanWithdraw;6,ContractPermissions,CanCheck;6,ContractCommunicationPermissions,CanRead;6,ContractCommunicationPermissions,CanWrite;6,ContractReportPermissions,CanRead;6,ContractReportPermissions,CanWrite;6,ContractReportPermissions,CanCheck;6,SpotCheckPermissions,CanRead;6,SpotCheckPermissions,CanWrite;6,AuditPermissions,CanRead;6,AuditPermissions,CanWrite;6,MonitoringFinancialControlPermissions,CanRead;6,MonitoringFinancialControlPermissions,CanWriteFinancial;6,MonitoringFinancialControlPermissions,CanWriteTechnical;6,IrregularitySignalPermissions,CanRead;6,IrregularitySignalPermissions,CanWrite;6,IrregularityPermissions,CanRead;6,IrregularityPermissions,CanWrite;6,CertificationPermissions,CanRead;6,CertificationPermissions,CanWrite;6,EuReimbursedAmountPermissions,CanRead;6,EuReimbursedAmountPermissions,CanWrite;6,CertAuthorityCommunicationPermissions,CanRead;6,CertAuthorityCommunicationPermissions,CanWrite;6,AuditAuthorityCommunicationPermissions,CanRead;6,AuditAuthorityCommunicationPermissions,CanWrite;6,ProjectDossierPermissions,CanRead'),
    (8                     , GETDATE()   , GETDATE()   , N'Администратор от ОПНОИР' , N'SapInterfacePermissions,CanImport;MonitoringPermissions,CanRead;NewsPermissions,CanPublish;GuidancePermissions,CanCreate;CompanyPermissions,CanRead;CompanyPermissions,CanWrite;UserAdminPermissions,CanAdministrate;UserAdminPermissions,CanControl;CertAuthorityCheckPermissions,CanRead;CertAuthorityCheckPermissions,CanWrite|7,OperationalMapPermissions,CanRead;7,OperationalMapPermissions,CanWrite;7,ProcedurePermissions,CanRead;7,ProcedurePermissions,CanWrite;7,ProcedurePermissions,CanCheck;7,ProcedurePermissions,CanDelete;7,ProjectPermissions,CanRead;7,ProjectPermissions,CanRegister;7,ContractPermissions,CanRead;7,ContractPermissions,CanWrite;7,ProjectPermissions,CanWithdraw;7,ContractPermissions,CanCheck;7,ContractCommunicationPermissions,CanRead;7,ContractCommunicationPermissions,CanWrite;7,ContractReportPermissions,CanRead;7,ContractReportPermissions,CanWrite;7,ContractReportPermissions,CanCheck;7,SpotCheckPermissions,CanRead;7,SpotCheckPermissions,CanWrite;7,AuditPermissions,CanRead;7,AuditPermissions,CanWrite;7,MonitoringFinancialControlPermissions,CanRead;7,MonitoringFinancialControlPermissions,CanWriteFinancial;7,MonitoringFinancialControlPermissions,CanWriteTechnical;7,IrregularitySignalPermissions,CanRead;7,IrregularitySignalPermissions,CanWrite;7,IrregularityPermissions,CanRead;7,IrregularityPermissions,CanWrite;7,CertificationPermissions,CanRead;7,CertificationPermissions,CanWrite;7,EuReimbursedAmountPermissions,CanRead;7,EuReimbursedAmountPermissions,CanWrite;7,CertAuthorityCommunicationPermissions,CanRead;7,CertAuthorityCommunicationPermissions,CanWrite;7,AuditAuthorityCommunicationPermissions,CanRead;7,AuditAuthorityCommunicationPermissions,CanWrite;7,ProjectDossierPermissions,CanRead'),
    (9                     , GETDATE()   , GETDATE()   , N'Потребител от ОПДУ'      , N'CompanyPermissions,CanRead;CompanyPermissions,CanWrite|1,OperationalMapPermissions,CanRead;1,OperationalMapPermissions,CanWrite;1,ProcedurePermissions,CanRead;1,ProcedurePermissions,CanWrite;1,ProcedurePermissions,CanCheck;1,ProjectPermissions,CanRead;1,ProjectPermissions,CanRegister'),
    (10                    , GETDATE()   , GETDATE()   , N'Потребител от ОПТТИ'     , N'CompanyPermissions,CanRead;CompanyPermissions,CanWrite|2,OperationalMapPermissions,CanRead;2,OperationalMapPermissions,CanWrite;2,ProcedurePermissions,CanRead;2,ProcedurePermissions,CanWrite;2,ProcedurePermissions,CanCheck;2,ProjectPermissions,CanRead;2,ProjectPermissions,CanRegister'),
    (11                    , GETDATE()   , GETDATE()   , N'Потребител от ОПРР'      , N'CompanyPermissions,CanRead;CompanyPermissions,CanWrite|3,OperationalMapPermissions,CanRead;3,OperationalMapPermissions,CanWrite;3,ProcedurePermissions,CanRead;3,ProcedurePermissions,CanWrite;3,ProcedurePermissions,CanCheck;3,ProjectPermissions,CanRead;3,ProjectPermissions,CanRegister'),
    (12                    , GETDATE()   , GETDATE()   , N'Потребител от ОПРЧР'     , N'CompanyPermissions,CanRead;CompanyPermissions,CanWrite|4,OperationalMapPermissions,CanRead;4,OperationalMapPermissions,CanWrite;4,ProcedurePermissions,CanRead;4,ProcedurePermissions,CanWrite;4,ProcedurePermissions,CanCheck;4,ProjectPermissions,CanRead;4,ProjectPermissions,CanRegister'),
    (13                    , GETDATE()   , GETDATE()   , N'Потребител от ОПИК'      , N'CompanyPermissions,CanRead;CompanyPermissions,CanWrite|5,OperationalMapPermissions,CanRead;5,OperationalMapPermissions,CanWrite;5,ProcedurePermissions,CanRead;5,ProcedurePermissions,CanWrite;5,ProcedurePermissions,CanCheck;5,ProjectPermissions,CanRead;5,ProjectPermissions,CanRegister'),
    (14                    , GETDATE()   , GETDATE()   , N'Потребител от ОПОС'      , N'CompanyPermissions,CanRead;CompanyPermissions,CanWrite|6,OperationalMapPermissions,CanRead;6,OperationalMapPermissions,CanWrite;6,ProcedurePermissions,CanRead;6,ProcedurePermissions,CanWrite;6,ProcedurePermissions,CanCheck;6,ProjectPermissions,CanRead;6,ProjectPermissions,CanRegister'),
    (15                    , GETDATE()   , GETDATE()   , N'Потребител от ОПНОИР'    , N'CompanyPermissions,CanRead;CompanyPermissions,CanWrite|7,OperationalMapPermissions,CanRead;7,OperationalMapPermissions,CanWrite;7,ProcedurePermissions,CanRead;7,ProcedurePermissions,CanWrite;7,ProcedurePermissions,CanCheck;7,ProjectPermissions,CanRead;7,ProjectPermissions,CanRegister'),
    (16                    , GETDATE()   , GETDATE()   , N'Админ Сесии'             , N'CompanyPermissions,CanRead;CompanyPermissions,CanWrite;RegistrationPermissions,CanRead|1,OperationalMapPermissions,CanRead;1,OperationalMapPermissions,CanWrite;1,ProcedurePermissions,CanRead;1,ProcedurePermissions,CanWrite;1,ProcedurePermissions,CanCheck;1,ProcedurePermissions,CanDelete;1,ProjectPermissions,CanRead;1,ProjectPermissions,CanRegister;1,ProjectPermissions,CanWithdraw;1,EvalSessionPermissions,CanAdministrate;2,OperationalMapPermissions,CanRead;2,OperationalMapPermissions,CanWrite;2,ProcedurePermissions,CanRead;2,ProcedurePermissions,CanWrite;2,ProcedurePermissions,CanCheck;2,ProcedurePermissions,CanDelete;2,ProjectPermissions,CanRead;2,ProjectPermissions,CanRegister;2,ProjectPermissions,CanWithdraw;2,EvalSessionPermissions,CanAdministrate;3,OperationalMapPermissions,CanRead;3,OperationalMapPermissions,CanWrite;3,ProcedurePermissions,CanRead;3,ProcedurePermissions,CanWrite;3,ProcedurePermissions,CanCheck;3,ProcedurePermissions,CanDelete;3,ProjectPermissions,CanRead;3,ProjectPermissions,CanRegister;3,ProjectPermissions,CanWithdraw;3,EvalSessionPermissions,CanAdministrate;4,OperationalMapPermissions,CanRead;4,OperationalMapPermissions,CanWrite;4,ProcedurePermissions,CanRead;4,ProcedurePermissions,CanWrite;4,ProcedurePermissions,CanCheck;4,ProcedurePermissions,CanDelete;4,ProjectPermissions,CanRead;4,ProjectPermissions,CanRegister;4,ProjectPermissions,CanWithdraw;4,EvalSessionPermissions,CanAdministrate;5,OperationalMapPermissions,CanRead;5,OperationalMapPermissions,CanWrite;5,ProcedurePermissions,CanRead;5,ProcedurePermissions,CanWrite;5,ProcedurePermissions,CanCheck;5,ProcedurePermissions,CanDelete;5,ProjectPermissions,CanRead;5,ProjectPermissions,CanRegister;5,ProjectPermissions,CanWithdraw;5,EvalSessionPermissions,CanAdministrate;6,OperationalMapPermissions,CanRead;6,OperationalMapPermissions,CanWrite;6,ProcedurePermissions,CanRead;6,ProcedurePermissions,CanWrite;6,ProcedurePermissions,CanCheck;6,ProcedurePermissions,CanDelete;6,ProjectPermissions,CanRead;6,ProjectPermissions,CanRegister;6,ProjectPermissions,CanWithdraw;6,EvalSessionPermissions,CanAdministrate;7,OperationalMapPermissions,CanRead;7,OperationalMapPermissions,CanWrite;7,ProcedurePermissions,CanRead;7,ProcedurePermissions,CanWrite;7,ProcedurePermissions,CanCheck;7,ProcedurePermissions,CanDelete;7,ProjectPermissions,CanRead;7,ProjectPermissions,CanRegister;7,ProjectPermissions,CanWithdraw;7,EvalSessionPermissions,CanAdministrate'),
    (17                    , GETDATE()   , GETDATE()   , N'Оценител към сесия'      , N'CompanyPermissions,CanRead;CompanyPermissions,CanWrite;RegistrationPermissions,CanRead|1,OperationalMapPermissions,CanRead;1,OperationalMapPermissions,CanWrite;1,ProcedurePermissions,CanRead;1,ProcedurePermissions,CanWrite;1,ProcedurePermissions,CanCheck;1,ProcedurePermissions,CanDelete;1,ProjectPermissions,CanRead;1,ProjectPermissions,CanRegister;1,ProjectPermissions,CanWithdraw;1,EvalSessionPermissions,CanEvaluate;2,OperationalMapPermissions,CanRead;2,OperationalMapPermissions,CanWrite;2,ProcedurePermissions,CanRead;2,ProcedurePermissions,CanWrite;2,ProcedurePermissions,CanCheck;2,ProcedurePermissions,CanDelete;2,ProjectPermissions,CanRead;2,ProjectPermissions,CanRegister;2,ProjectPermissions,CanWithdraw;2,EvalSessionPermissions,CanEvaluate;3,OperationalMapPermissions,CanRead;3,OperationalMapPermissions,CanWrite;3,ProcedurePermissions,CanRead;3,ProcedurePermissions,CanWrite;3,ProcedurePermissions,CanCheck;3,ProcedurePermissions,CanDelete;3,ProjectPermissions,CanRead;3,ProjectPermissions,CanRegister;3,ProjectPermissions,CanWithdraw;3,EvalSessionPermissions,CanEvaluate;4,OperationalMapPermissions,CanRead;4,OperationalMapPermissions,CanWrite;4,ProcedurePermissions,CanRead;4,ProcedurePermissions,CanWrite;4,ProcedurePermissions,CanCheck;4,ProcedurePermissions,CanDelete;4,ProjectPermissions,CanRead;4,ProjectPermissions,CanRegister;4,ProjectPermissions,CanWithdraw;4,EvalSessionPermissions,CanEvaluate;5,OperationalMapPermissions,CanRead;5,OperationalMapPermissions,CanWrite;5,ProcedurePermissions,CanRead;5,ProcedurePermissions,CanWrite;5,ProcedurePermissions,CanCheck;5,ProcedurePermissions,CanDelete;5,ProjectPermissions,CanRead;5,ProjectPermissions,CanRegister;5,ProjectPermissions,CanWithdraw;5,EvalSessionPermissions,CanEvaluate;6,OperationalMapPermissions,CanRead;6,OperationalMapPermissions,CanWrite;6,ProcedurePermissions,CanRead;6,ProcedurePermissions,CanWrite;6,ProcedurePermissions,CanCheck;6,ProcedurePermissions,CanDelete;6,ProjectPermissions,CanRead;6,ProjectPermissions,CanRegister;6,ProjectPermissions,CanWithdraw;6,EvalSessionPermissions,CanEvaluate;7,OperationalMapPermissions,CanRead;7,OperationalMapPermissions,CanWrite;7,ProcedurePermissions,CanRead;7,ProcedurePermissions,CanWrite;7,ProcedurePermissions,CanCheck;7,ProcedurePermissions,CanDelete;7,ProjectPermissions,CanRead;7,ProjectPermissions,CanRegister;7,ProjectPermissions,CanWithdraw;7,EvalSessionPermissions,CanEvaluate')

SET IDENTITY_INSERT [PermissionTemplates] OFF
GO