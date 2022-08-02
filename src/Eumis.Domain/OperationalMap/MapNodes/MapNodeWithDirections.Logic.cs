using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.OperationalMap.MapNodes
{
    public abstract partial class MapNodeWithDirections
    {
        #region MapNodeDirection

        public MapNodeDirection CreateProgrammeDirection(int directionId, int? subDirectionId)
        {
            MapNodeDirection programmePriorityDirection = new MapNodeDirection(directionId, subDirectionId);
            this.MapNodeDirections.Add(programmePriorityDirection);

            this.ModifyDate = DateTime.Now;

            return programmePriorityDirection;
        }

        public MapNodeDirection FindMapNodeDirection(int directionId)
        {
            return this.MapNodeDirections.Where(t => t.MapNodeDirectionId == directionId).FirstOrDefault();
        }

        public void RemoveMapNodeDirection(int directionId)
        {
            var programmePriorityDirection = this.FindMapNodeDirection(directionId);
            if (programmePriorityDirection != null)
            {
                this.MapNodeDirections.Remove(programmePriorityDirection);
            }

            this.ModifyDate = DateTime.Now;
        }

        public MapNodeDirection UpdateMapNodeDirection(int programmePriorityDirectionId, int directionId, int? subDirectionId)
        {
            var programmePriorityDirection = this.FindMapNodeDirection(programmePriorityDirectionId);
            if (programmePriorityDirection != null)
            {
                programmePriorityDirection.SubDirectionId = subDirectionId;
                programmePriorityDirection.DirectionId = directionId;

                this.ModifyDate = DateTime.Now;
            }

            return programmePriorityDirection;
        }

        public IList<string> CanUpdateDirection(int mapNodeDirectionId, int directionId, int? subDirectionId)
        {
            var errors = new List<string>();

            var duplacteExists = this.MapNodeDirections
                .Where(t => t.MapNodeDirectionId != mapNodeDirectionId && t.DirectionId == directionId && t.SubDirectionId == subDirectionId)
                .Any();
            if (duplacteExists)
            {
                errors.Add("Вече съществува запис с това направление и поднаправление");
            }

            return errors;
        }

        public IList<string> CanCreateDirection(int directionId, int? subDirectionId)
        {
            var errors = new List<string>();

            var duplacteExists = this.MapNodeDirections
                .Where(t => t.DirectionId == directionId && t.SubDirectionId == subDirectionId)
                .Any();
            if (duplacteExists)
            {
                errors.Add("Вече съществува запис с това направление и поднаправление");
            }

            return errors;
        }

        #endregion //MapNodeDirection
    }
}
