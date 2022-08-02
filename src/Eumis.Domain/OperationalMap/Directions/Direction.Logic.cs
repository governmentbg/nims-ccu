using System;
using System.Linq;
using Eumis.Domain.OperationalMap.MapNodes;

namespace Eumis.Domain.OperationalMap.Directions
{
    public partial class Direction
    {
        public void UpdateAttributes(string name, string nameAlt)
        {
            this.AssertIsDraft();

            this.Name = name;
            this.NameAlt = nameAlt;

            this.ModifyDate = DateTime.Now;
        }

        public void AssertIsDraft()
        {
            if (this.Status != DirectionStatus.Draft)
            {
                throw new DomainException("Status must be draft");
            }
        }

        public void AssertIsEnetered()
        {
            if (this.Status != DirectionStatus.Entered)
            {
                throw new DomainException("Status must be draft");
            }
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEnetered();

            this.ChangeStatus(DirectionStatus.Draft);
        }

        public void ChangeStatusToEntered()
        {
            this.AssertIsDraft();

            this.ChangeStatus(DirectionStatus.Entered);
        }

        private void ChangeStatus(DirectionStatus status)
        {
            this.Status = status;
            this.ModifyDate = DateTime.Now;
        }

        public SubDirection AddSubDirection(string name, string nameAlt)
        {
            this.AssertIsDraft();

            var subDirection = new SubDirection(name, nameAlt);
            this.SubDirections.Add(subDirection);

            this.ModifyDate = DateTime.Now;

            return subDirection;
        }

        public void UpdateSubDirection(int subDirectionId, string name, string nameAlt)
        {
            this.AssertIsDraft();

            var subDirection = this.SubDirections.FirstOrDefault(t => t.SubDirectionId == subDirectionId);
            subDirection.Name = name;
            subDirection.NameAlt = nameAlt;

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveSubDirection(int subDirectionId)
        {
            this.AssertIsDraft();

            var subDirection = this.SubDirections.FirstOrDefault(t => t.SubDirectionId == subDirectionId);
            this.SubDirections.Remove(subDirection);

            this.ModifyDate = DateTime.Now;
        }
    }
}
