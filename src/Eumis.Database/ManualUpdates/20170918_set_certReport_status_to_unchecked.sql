update cr
set
    Status = 3,
    ApprovalDate = NULL
from CertReports cr
join MapNodes p on cr.ProgrammeId = p.MapNodeId
where p.MapNodeId = 2 and
    exists (select * from (
                    values  (5, 1),
                            (6, 1),
                            (9, 1)
                    )
                as v(OrderNum, OrderVersionNum)
                where cr.OrderNum = v.OrderNum and
                    cr.OrderVersionNum = v.OrderVersionNum
            )