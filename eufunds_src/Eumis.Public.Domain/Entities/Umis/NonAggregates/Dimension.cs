using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public enum Dimension
    {
        [Description("Област на интервенция")]
        InterventionField = 1,

        [Description("Форма на финансиране")]
        FormOfFinance = 2,

        [Description("Вид на територията")]
        TerritorialDimension = 3,
        
        [Description("Механизми за териториално изпълнение")]
        TerritorialDeliveryMechanism = 4,
        
        [Description("Тематична цел")]
        ThematicObjective = 5,
        
        [Description("Вторична тема на ЕСФ")]
        ESFSecondaryTheme = 6,

        [Description("Икономическа дейност")]
        EconomicDimension = 7
    }
}
