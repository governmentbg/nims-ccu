--executed on 2018/05/30 ~ 16:30

update cr
set
    Status = 3,
    ApprovalDate = NULL
from CertReports cr
join MapNodes p on cr.ProgrammeId = p.MapNodeId
where exists
    (select * from (
        values  ('ОПИК', 15, 1, 4, '2018-05-30 08:15:20.7806727')
    )
    as v(Programme, OrderNum, OrderVersionNum, OldStatus, OldApprovalDate)
    where
        p.ShortName = v.Programme and
        cr.OrderNum = v.OrderNum and
        cr.OrderVersionNum = v.OrderVersionNum and
        cr.Status = v.OldStatus and
        cr.ApprovalDate = v.OldApprovalDate
    )