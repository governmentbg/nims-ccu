update cr
set
    Status = 3,
    ApprovalDate = NULL
from CertReports cr
join MapNodes p on cr.ProgrammeId = p.MapNodeId
where exists
    (select * from (
        values  ('ОПИК', 11, 1, 5, '2017-10-31 20:39:16.2568334')
    )
    as v(Programme, OrderNum, OrderVersionNum, OldStatus, OldApprovalDate)
    where
        p.ShortName = v.Programme and
        cr.OrderNum = v.OrderNum and
        cr.OrderVersionNum = v.OrderVersionNum and
        cr.Status = v.OldStatus and
        cr.ApprovalDate = v.OldApprovalDate
    )