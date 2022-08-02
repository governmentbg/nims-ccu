using Eumis.Public.Domain.Helpers;

namespace Eumis.Public.Domain.Entities.Umis.Contracts
{
    public enum ContractExecutionStatus
    {
        [LocalizableDescription("ContractExecutionStatus_Active")]
        Active = 1,

        [LocalizableDescription("ContractExecutionStatus_Paused")]
        Paused = 2,

        [LocalizableDescription("ContractExecutionStatus_Monitored")]
        Monitored = 3,

        [LocalizableDescription("ContractExecutionStatus_Canceled")]
        Canceled = 4,

        [LocalizableDescription("ContractExecutionStatus_Ended")]
        Ended = 5,

        [LocalizableDescription("ContractExecutionStatus_Concluded")]
        Concluded = 6,

        [LocalizableDescription("ContractExecutionStatus_Suspended")]
        Suspended = 7
    }
}
