using Eumis.Domain.OperationalMap.ProgrammeDeclarations;

namespace Eumis.Web.Api.OperationalMap.Programmes.DataObjects
{
    public class ProgrammeDeclarationItemDO
    {
        public ProgrammeDeclarationItemDO()
        {
        }

        public ProgrammeDeclarationItemDO(ProgrammeDeclarationItem programmeDeclarationItem, int programmeId, byte[] version)
        {
            this.ProgrammeDeclarationItemId = programmeDeclarationItem.ProgrammeDeclarationItemId;
            this.ProgrammeDeclarationId = programmeDeclarationItem.ProgrammeDeclarationId;
            this.ProgrammeId = programmeId;
            this.OrderNum = programmeDeclarationItem.OrderNum;
            this.Content = programmeDeclarationItem.Content;
            this.IsActive = programmeDeclarationItem.IsActive;
            this.Version = version;
        }

        public ProgrammeDeclarationItemDO(int programmeDeclarationId, int programmeId, byte[] version)
        {
            this.ProgrammeDeclarationId = programmeDeclarationId;
            this.ProgrammeId = programmeId;
            this.IsActive = true;
            this.Version = version;
        }

        public int ProgrammeDeclarationItemId { get; set; }

        public int ProgrammeDeclarationId { get; set; }

        public int ProgrammeId { get; set; }

        public int? OrderNum { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }
    }
}
