UPDATE CertReports
SET
	Status = 3,
	ApprovalDate = NULL
WHERE CertReportId IN (174, 228)
