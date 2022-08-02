update cr
set
	Status = 3,
	ApprovalDate = NULL
from CertReports cr
join MapNodes p on cr.ProgrammeId = p.MapNodeId
where p.MapNodeId = 6 and
	exists (select * from (
					values	(3, 2),
							(4, 3),
							(5, 1),
							(6, 3),
							(7, 1),
							(8, 2)
					)
				as v(OrderNum, OrderVersionNum)
				where cr.OrderNum = v.OrderNum and
					cr.OrderVersionNum = v.OrderVersionNum
            )