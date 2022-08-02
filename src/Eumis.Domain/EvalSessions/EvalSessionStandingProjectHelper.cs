using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.EvalSessions
{
    public static class EvalSessionStandingProjectHelper
    {
        public static void DeterminateMoveableProjects(this IList<EvalSessionStandingProjectDO> projects, NewEvalSessionStandingType standingType)
        {
            for (int k = 0; k < projects.Count; k++)
            {
                EvalSessionStandingProjectDO previousProject = null;
                EvalSessionStandingProjectDO nextProject = null;
                EvalSessionStandingProjectDO currentProject = projects[k];

                if (k > 0)
                {
                    previousProject = projects[k - 1];
                }

                if (k + 1 < projects.Count)
                {
                    nextProject = projects[k + 1];
                }

                if (currentProject.Status == EvalSessionProjectStandingStatus.Approved || currentProject.Status == EvalSessionProjectStandingStatus.Reserve)
                {
                    currentProject.CanMoveUp = previousProject != null && GetProjectPoints(currentProject, standingType) == GetProjectPoints(previousProject, standingType);
                    currentProject.CanMoveDown = nextProject != null && GetProjectPoints(currentProject, standingType) == GetProjectPoints(nextProject, standingType);
                }
            }
        }

        private static decimal? GetProjectPoints(EvalSessionStandingProjectDO standingProject, NewEvalSessionStandingType standingType)
        {
            switch (standingType)
            {
                case NewEvalSessionStandingType.Complex:
                    return standingProject.PointsComplex;
                case NewEvalSessionStandingType.TwoStep:
                    return standingProject.PointsTFO;
                case NewEvalSessionStandingType.Preliminary:
                    return standingProject.PointsPreliminary;
                default:
                    throw new DomainException("Unknown project standing type");
            }
        }
    }
}
