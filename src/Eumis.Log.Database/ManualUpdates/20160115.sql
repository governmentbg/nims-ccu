--executed on 2016/01/15 ~ 17:00
UPDATE [dbo].[ActionLogs]
SET [ActionLogType] = 2
WHERE [Action] like 'ContractReports.Edit.ContractReport%' and [Username] is null and [ActionLogType] = 1
GO



UPDATE [dbo].[ActionLogs]
SET [Action] = REPLACE ([Action] , N'UpdateXml' , N'Delete')
WHERE [Action] like 'ContractReports.Edit.ContractReport%UpdateXml' and [Username] is null and PostData is null
GO



UPDATE [dbo].[ActionLogs]
SET [ActionLogType] = 2, [Action] = REPLACE([Action], N'Contracts.Edit.Communications', 'ContractCommunications')
WHERE [Action] like '%Contracts.Edit.Communications%' and [Username] is null and [ActionLogType] = 1
GO