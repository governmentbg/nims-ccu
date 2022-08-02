using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Eumis.Log.ActionLogger.Enums
{
    [SuppressMessage("", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Classes nested for ease of use.")]
    public static class ActionLogGroups
    {
        [DisplayName("Основна организация")]
        public static class Programme
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на стаус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на стаус (Въведен)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Органи за управление и контрол")]
                public static class Institutions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Процедурен наръчник")]
                public static class ProgrammeProcedureManuals
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Актуален)")]
                    public static class ChangeStatusToActual
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ProgrammeCheckLists
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Копиране")]
                    public static class Copy
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                        [DisplayName("Версии")]
                        public static class Versions
                        {
                            [DisplayName("Създаване")]
                            public static class Create
                            {
                            }

                            [DisplayName("Изтриване")]
                            public static class Delete
                            {
                            }

                            [DisplayName("Смяна на стаус (Чернова)")]
                            public static class ChangeStatusToDraft
                            {
                            }

                            [DisplayName("Смяна на стаус (Актуален)")]
                            public static class ChangeStatusToActual
                            {
                            }

                            [DisplayName("Редакция на документ")]
                            public static class UpdateXml
                            {
                            }

                            [DisplayName("Приключване на документ")]
                            public static class Submit
                            {
                            }
                        }
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ProgrammeCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи за подаване")]
                public static class ApplicationDocuments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Зареждане от външен файл")]
                    public static class Load
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Направления")]
                public static class Directions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Декларации към формуляр за кандидатстване")]
                public static class AppFormDeclarations
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                        [DisplayName("Редове към номенклатура")]
                        public static class Items
                        {
                            [DisplayName("Създаване")]
                            public static class Create
                            {
                            }

                            [DisplayName("Редакция")]
                            public static class Edit
                            {
                            }

                            [DisplayName("Активиране")]
                            public static class Activate
                            {
                            }

                            [DisplayName("Деактивиране")]
                            public static class Deactivate
                            {
                            }

                            [DisplayName("Изтриване")]
                            public static class Delete
                            {
                            }

                            [DisplayName("Зареждане от външен файл")]
                            public static class Load
                            {
                            }
                        }
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Разпоредител с бюджетни средства")]
        public static class ProgrammePriority
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на стаус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на стаус (Въведен)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Индикатори Таблица 6")]
                public static class IndicatorsTable6
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Присъединяване")]
                    public static class Attach
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Индикатори Таблица 13")]
                public static class IndicatorsTable13
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Присъединяване")]
                    public static class Attach
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Финансиране")]
                public static class Budgets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Направления")]
                public static class Directions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Направление")]
        public static class Direction
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на стаус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на стаус (Въведен)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }
        }

        [DisplayName("Мерни единици")]
        public static class Measures
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Индикатори")]
        public static class Indicators
        {
            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Видове индикатори")]
        public static class IndicatorItemTypes
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Типове разходи")]
        public static class ExpenseTypes
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Активиране")]
            public static class Activate
            {
            }

            [DisplayName("Деактивиране")]
            public static class Deactivate
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Подтипове разход")]
                public static class ExpenseSubTypes
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Схеми за олихвяване")]
        public static class InterestSchemes
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Надбавки")]
        public static class Allowances
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Проценти")]
                public static class AllowanceRates
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Осн. лихвени проценти")]
        public static class BasicInterestRates
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Проценти")]
                public static class InterestRates
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Осн. лихвени проценти")]
        public static class CheckBlankTopics
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Бюджети")]
        public static class Procedures
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Копиране")]
            public static class Copy
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведен)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Проверен)")]
            public static class ChangeStatusToChecked
            {
            }

            [DisplayName("Смяна на статус (Активен)")]
            public static class ChangeStatusToActive
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Прекратен)")]
            public static class ChangeStatusToTerminated
            {
            }

            [DisplayName("Смяна на статус (Анулиран)")]
            public static class ChangeStatusToCanceled
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeContractReportDocumentsSectionStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Активен)")]
            public static class ChangeContractReportDocumentsSectionStatusToActive
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Данни за ИГРП")]
                public static class ProcedureIAWP
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                        [DisplayName("Основни данни")]
                        public static class BasicData
                        {
                        }

                        [DisplayName("Допустими кандидати")]
                        public static class Candidates
                        {
                            [DisplayName("Създаване")]
                            public static class Create
                            {
                            }

                            [DisplayName("Изтриване")]
                            public static class Delete
                            {
                            }
                        }

                        [DisplayName("Директни бенефициенти")]
                        public static class Companies
                        {
                            [DisplayName("Създаване")]
                            public static class Create
                            {
                            }

                            [DisplayName("Изтриване")]
                            public static class Delete
                            {
                            }
                        }
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Източници на финансиране")]
                public static class ProcedureShares
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Компоненти към бюджет")]
                    public static class ProcedureBudgetComponents
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Редакция")]
                        public static class Edit
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }
                    }
                }

                [DisplayName("Местоположение")]
                public static class Locations
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Мониторстат")]
                public static class Monitorstat
                {
                    [DisplayName("Присъединяване на документ")]
                    public static class Attach
                    {
                    }

                    [DisplayName("Изтриване на документ")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Създаване на заявка")]
                    public static class CreateRequest
                    {
                    }

                    [DisplayName("Редакция на заявка")]
                    public static class EditRequest
                    {
                    }

                    [DisplayName("Изтриване на заявка")]
                    public static class DeleteRequest
                    {
                    }

                    [DisplayName("Изпращане на заявка")]
                    public static class SendRequest
                    {
                    }

                    [DisplayName("Икономическа дейност")]
                    public static class EconomicActivities
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }
                    }
                }

                [DisplayName("Присъединени инвестиционни приоритети")]
                public static class InvestmentPriorities
                {
                    [DisplayName("Присъединяване")]
                    public static class Attach
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Присъединени специфични цел")]
                public static class SpecificTargets
                {
                    [DisplayName("Присъединяване")]
                    public static class Attach
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Разход")]
                public static class BudgetLevel1
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Подразход")]
                public static class BudgetLevel2
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Детайл на подразход")]
                public static class BudgetLevel3
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Валидация на бюджет")]
                public static class BudgetValidation
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Област на интервенция")]
                public static class InterventionField
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Вид на територията")]
                public static class TerritorialDimension
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Тематична цел")]
                public static class ThematicObjective
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Икономическа дейност")]
                public static class EconomicDimension
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Форма на финансиране")]
                public static class FormOfFinance
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Механизми за териториално изпълнение")]
                public static class TerritorialDeliveryMechanism
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Вторична тема на ЕСФ")]
                public static class ESFSecondaryTheme
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Индикатори")]
                public static class Indicators
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Присъединяване")]
                    public static class Attach
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Срокове")]
                public static class TimeLimits
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Допълнителни полета")]
                public static class SpecFields
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Вътрешни документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Насоки за кандидатстване")]
                public static class AppGuidelines
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи за подаване")]
                public static class AppDocs
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Отчетни документи")]
                public static class ContractReportDocuments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Оценителни таблици")]
                public static class EvalTables
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Приключване на документ")]
                    public static class Submit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Въпроси и отговори")]
                public static class Questions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Обществено обсъждане")]
                public static class ProcedurePublicDiscussion
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Смяна на статус (Публикуванa)")]
                    public static class ChangeStatusToPublished
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус на секция Коментари и препоръки (Публикуванa)")]
                    public static class ChangeCommentsSectionStatusToPublished
                    {
                    }

                    [DisplayName("Смяна на статус на секция Коментари и препоръки (Чернова)")]
                    public static class ChangeCommentsSectionStatusToDraft
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                        [DisplayName("Проект на Насоки")]
                        public static class Guideline
                        {
                            [DisplayName("Създаване")]
                            public static class Create
                            {
                            }

                            [DisplayName("Редакция")]
                            public static class Edit
                            {
                            }

                            [DisplayName("Изтриване")]
                            public static class Delete
                            {
                            }

                            [DisplayName("Активиране")]
                            public static class Activate
                            {
                            }

                            [DisplayName("Деактивиране")]
                            public static class Deactivate
                            {
                            }
                        }

                        [DisplayName("Коментари и предложения")]
                        public static class Comment
                        {
                            [DisplayName("Създаване")]
                            public static class Create
                            {
                            }

                            [DisplayName("Редакция")]
                            public static class Edit
                            {
                            }

                            [DisplayName("Изтриване")]
                            public static class Delete
                            {
                            }

                            [DisplayName("Активиране")]
                            public static class Activate
                            {
                            }

                            [DisplayName("Деактивиране")]
                            public static class Deactivate
                            {
                            }
                        }
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ProcedureCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Разяснения кък процедура")]
                public static class ProcedureDiscussion
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Публикуванa)")]
                    public static class ChangeStatusToPublished
                    {
                    }

                    [DisplayName("Смяна на статус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Публикуване на всички дискусии")]
                    public static class PublishAll
                    {
                    }
                }

                [DisplayName("Направления")]
                public static class Directions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Декларации")]
                public static class Declarations
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }
            }
        }

        [DisplayName("Обществени поръчки")]
        public static class Procurements
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Активна)")]
            public static class ChangeStatusToActive
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToCanceled
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Обособени позиции")]
                public static class DifferentiatedPositions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Индикативни годишни работни програми")]
        public static class IndicativeAnnualWorkingProgrammes
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Генерирана таблица")]
                public static class Table
                {
                }
            }

            [DisplayName("Смяна на статус")]
            public static class ChangeStatus
            {
                [DisplayName("Публикувана")]
                public static class ToPublished
                {
                }

                [DisplayName("Архивирана")]
                public static class ToArchived
                {
                }

                [DisplayName("Анулирана")]
                public static class ToCanceled
                {
                }
            }

            [DisplayName("Генериране")]
            public static class Generate
            {
                [DisplayName("На таблица ИГРП")]
                public static class IawpTable
                {
                }

                [DisplayName("На таблица ИГРП интегрирани процедури")]
                public static class IawpTableForIntegratedProcedures
                {
                }
            }
        }

        [DisplayName("Кандидати")]
        public static class Companies
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Местоположение")]
                public static class LocalActionGroupMunicipalities
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Проектни предложения")]
        public static class Projects
        {
            [DisplayName("Регистриране")]
            public static class Register
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Кореспонденция")]
                public static class ManagingAuthorityCommunications
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Анулиране")]
                    public static class Cancel
                    {
                    }

                    [DisplayName("Редакция на съобщение")]
                    public static class QuestionUpdated
                    {
                    }

                    [DisplayName("Смяна на статус (Изпратено съобщение)")]
                    public static class ChangeStatusToQuestion
                    {
                    }

                    [DisplayName("Отговори")]
                    public static class Answers
                    {
                        [DisplayName("Редакция на документ")]
                        public static class Update
                        {
                        }

                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Смяна на статус (Изпратен въпрос)")]
                        public static class ChangeStatusToAnswer
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }
                    }
                }
            }

            [DisplayName("Оттегляне")]
            public static class Withdraw
            {
            }

            [DisplayName("Заявки към Мониторстат")]
            public static class MonitorstatRequests
            {
                [DisplayName("Създаване")]
                public static class Create
                {
                }

                [DisplayName("Редакция")]
                public static class Edit
                {
                }

                [DisplayName("Изтриване")]
                public static class Delete
                {
                }

                [DisplayName("Изпращане")]
                public static class Send
                {
                }

                [DisplayName("Автоматично създаване и изпращане")]
                public static class SendAutomaticRequest
                {
                }
            }
        }

        [DisplayName("Потребителски профили")]
        public static class Users
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Възстановяване")]
            public static class Recover
            {
            }

            [DisplayName("Заключване")]
            public static class Lock
            {
            }

            [DisplayName("Отключване")]
            public static class Unlock
            {
            }

            [DisplayName("Смяна на парола")]
            public static class ChangePassword
            {
            }
        }

        [DisplayName("Шаблони за групи")]
        public static class PermissionTemplates
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }
        }

        [DisplayName("Групи потребители")]
        public static class UserTypes
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Потребителски организации")]
        public static class UserOrganizations
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Пакети за актуализация")]
        public static class RequestPackages
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведен)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Проверен)")]
            public static class ChangeStatusToChecked
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Анулиран)")]
            public static class ChangeStatusToCanceled
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Потребители")]
                public static class Users
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Проверена)")]
                    public static class ChangeStatusToChecked
                    {
                    }

                    [DisplayName("Смяна на статус (Активна)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Смяна на статус (Отхвърлена)")]
                    public static class ChangeStatusToRejected
                    {
                    }
                }

                [DisplayName("Заявка за права")]
                public static class UserPermissionRequests
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Заявка за рег. данни")]
                public static class UserRegDataRequests
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Оценителни сесии")]
        public static class EvalSessions
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Активна)")]
            public static class ChangeStatusToActive
            {
            }

            [DisplayName("Смяна на статус (Приключена)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Приключена от МИГ)")]
            public static class ChangeStatusToEndedByLAG
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToCanceled
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Членове")]
                public static class Users
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Проектни предложения")]
                public static class Projects
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Анулиране")]
                    public static class Cancel
                    {
                    }

                    [DisplayName("Възстановяване")]
                    public static class Restore
                    {
                    }

                    [DisplayName("Версии на проектно предложение")]
                    public static class Versions
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Създаване от регистрационните данни")]
                        public static class CreateFromRegData
                        {
                        }

                        [DisplayName("Редакция на бележка")]
                        public static class UpdateNote
                        {
                        }

                        [DisplayName("Редакция на документ")]
                        public static class UpdateXml
                        {
                        }

                        [DisplayName("Активиране")]
                        public static class Activate
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }
                    }

                    [DisplayName("Комуникация с кандидата")]
                    public static class Communications
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Редакция")]
                        public static class Edit
                        {
                        }

                        [DisplayName("Смяна на статус (Анулирана)")]
                        public static class ChangeStatusToCanceled
                        {
                        }

                        [DisplayName("Смяна на статус (Изпратено съобщение)")]
                        public static class ChangeStatusToQuestion
                        {
                        }

                        [DisplayName("Редакция на съобщение")]
                        public static class QuestionUpdated
                        {
                        }

                        [DisplayName("Регистриране на отговор на хартия")]
                        public static class AnswerRegistered
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }
                    }

                    [DisplayName("Класирания")]
                    public static class Standings
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }
                    }
                }

                [DisplayName("Разпределения")]
                public static class Distributions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Отказване")]
                    public static class Refuse
                    {
                    }
                }

                [DisplayName("Резултати от оценка")]
                public static class AdminAdmissResults
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Публикуване")]
                    public static class Publish
                    {
                    }

                    [DisplayName("Анулиране")]
                    public static class Cancel
                    {
                    }
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Доклади")]
                public static class Reports
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Анулиране")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Обобщена оценки")]
                public static class Evaluations
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Групово създаване")]
                    public static class BulkCreate
                    {
                    }
                }

                [DisplayName("Оценителни листове")]
                public static class Sheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Смяна на статус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус (Прекъснат)")]
                    public static class ChangeStatusToPaused
                    {
                    }

                    [DisplayName("Продължаване")]
                    public static class Continue
                    {
                    }
                }

                [DisplayName("Становища")]
                public static class Standpoints
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Смяна на статус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }
                }

                [DisplayName("Автоматични класирания")]
                public static class Standings
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Отказване")]
                    public static class Refuse
                    {
                    }

                    [DisplayName("Разместване")]
                    public static class Rearrange
                    {
                        [DisplayName("Нагоре")]
                        public static class MoveUp
                        {
                        }

                        [DisplayName("Надолу")]
                        public static class MoveDown
                        {
                        }

                        [DisplayName("Приложено")]
                        public static class Applied
                        {
                        }
                    }
                }

                [DisplayName("Автоматично създаване на версия на ПП, оценителен лист и обобщена оценка")]
                public static class ExecuteAutomaticProjectEvaluation
                {
                }
            }
        }

        [DisplayName("Новини")]
        public static class News
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Публикуванa)")]
            public static class ChangeStatusToPublished
            {
            }

            [DisplayName("Смяна на статус (Архивиранa)")]
            public static class ChangeStatusToArchived
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Декларации")]
        public static class Declarations
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Смяна на статус (Публикуванa)")]
            public static class ChangeStatusToPublished
            {
            }

            [DisplayName("Смяна на статус (Архивиранa)")]
            public static class ChangeStatusToArchived
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Междинни суми")]
        public static class ProgrammeGroups
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Съобщения")]
        public static class Messages
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изпращане")]
            public static class Send
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Нотификации")]
        public static class UserNotifications
        {
            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Отбележи като прочетено")]
            public static class MarkAsRead
            {
            }
        }

        [DisplayName("Настройки за нотификация")]
        public static class NotificationSettings
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }
                }

                [DisplayName("Избрани договори")]
                public static class AttachedContracts
                {
                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Избрани процедури")]
                public static class AttachedProcedures
                {
                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Избрани приоритетни оси")]
                public static class AttachedProgrammePriorities
                {
                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Избрани оперативни програми")]
                public static class AttachedProgrammes
                {
                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }

            [DisplayName("Изпращане")]
            public static class Send
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Ръководства")]
        public static class Guidances
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Договори")]
        public static class Contracts
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Отбелязване като проверен")]
            public static class MarkAsChecked
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Версии")]
                public static class Versions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Техническа редакция")]
                    public static class TechnicalEdit
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Въведен)")]
                    public static class ChangeStatusToEntered
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Отбелязване като проверен")]
                    public static class MarkAsChecked
                    {
                    }
                }

                [DisplayName("Процедури за избор на изпълнител и сключени договори")]
                public static class Procurements
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Въведен)")]
                    public static class ChangeStatusToEntered
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Отбелязване като проверен")]
                    public static class MarkAsChecked
                    {
                    }
                }

                [DisplayName("Планове за разходване на средствата")]
                public static class SpendingPlans
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Въведен)")]
                    public static class ChangeStatusToEntered
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Отбелязване като проверен")]
                    public static class MarkAsChecked
                    {
                    }
                }

                [DisplayName("Комуникации")]
                public static class Communications
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Смяна на статус (Изпратено съобщение)")]
                    public static class ChangeStatusToSent
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Профили за достъп към договор")]
                public static class Registrations
                {
                    [DisplayName("Създаване и присъединяване")]
                    public static class CreateAndAttach
                    {
                    }

                    [DisplayName("Присъединяване")]
                    public static class Attach
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }

                    [DisplayName("Редактиране")]
                    public static class Edit
                    {
                    }
                }

                [DisplayName("Кодове за достъп към договор")]
                public static class AccessCodes
                {
                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Деактивиране")]
                    public static class Deactivate
                    {
                    }
                }

                [DisplayName("Външни верификатори")]
                public static class Users
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи към договор за предоставяне на БФП")]
                public static class GrantDocuments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи към процедури за избор на изпълнител")]
                public static class ProcurementDocuments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ContractCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ContractProcurementCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Профили за достъп към договор")]
        public static class ContractRegistrations
        {
            [DisplayName("Редакция")]
            public static class Edit
            {
            }
        }

        [DisplayName("Пакет отчетни документи")]
        public static class ContractReports
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Въведен)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToSentChecked
            {
            }

            [DisplayName("Смяна на статус (В проверка)")]
            public static class ChangeStatusToUnchecked
            {
            }

            [DisplayName("Смяна на статус (Приет)")]
            public static class ChangeStatusToAccepted
            {
            }

            [DisplayName("Смяна на статус (Отхвърлен)")]
            public static class ChangeStatusToRefused
            {
            }

            [DisplayName("Връщане в статус (В проверка)")]
            public static class ReturnStatusToUnchecked
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Основни данни по време на проверка")]
                public static class BasicCheckData
                {
                }

                [DisplayName("Технически отчет")]
                public static class ContractReportTechnical
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Въведен)")]
                    public static class ChangeStatusToEntered
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус (Актуален)")]
                    public static class ChangeStatusToActual
                    {
                    }

                    [DisplayName("Смяна на статус (Върнат)")]
                    public static class ChangeStatusToReturned
                    {
                    }
                }

                [DisplayName("Финансов отчет")]
                public static class ContractReportFinancial
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Въведен)")]
                    public static class ChangeStatusToEntered
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус (Актуален)")]
                    public static class ChangeStatusToActual
                    {
                    }

                    [DisplayName("Смяна на статус (Върнат)")]
                    public static class ChangeStatusToReturned
                    {
                    }
                }

                [DisplayName("Искане за плащане")]
                public static class ContractReportPayment
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Въведен)")]
                    public static class ChangeStatusToEntered
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус (Актуален)")]
                    public static class ChangeStatusToActual
                    {
                    }

                    [DisplayName("Смяна на статус (Върнат)")]
                    public static class ChangeStatusToReturned
                    {
                    }
                }

                [DisplayName("Микроданни")]
                public static class ContractReportMicro
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция на Excel")]
                    public static class UpdateExcel
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Въведен)")]
                    public static class ChangeStatusToEntered
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус (Актуален)")]
                    public static class ChangeStatusToActual
                    {
                    }

                    [DisplayName("Смяна на статус (Върнат)")]
                    public static class ChangeStatusToReturned
                    {
                    }

                    [DisplayName("Нова версия на микроданни")]
                    public static class NewVersion
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Редакция")]
                        public static class Update
                        {
                        }

                        [DisplayName("Смяна на статус (Актуален)")]
                        public static class ChangeStatusToActual
                        {
                        }

                        [DisplayName("Изтриване на микроданни")]
                        public static class Delete
                        {
                        }
                    }
                }

                [DisplayName("Проверка на финансов отчет")]
                public static class ContractReportFinancialCheck
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Актуална)")]
                    public static class ChangeStatusToActive
                    {
                    }
                }

                [DisplayName("Верифицирано искане за плащане")]
                public static class ContractReportPaymentCheck
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Актуално)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Смяна на статус (Архивирано)")]
                    public static class ChangeStatusToArchived
                    {
                    }
                }

                [DisplayName("Проверка на микроданни")]
                public static class ContractReportMicroCheck
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Актуална)")]
                    public static class ChangeStatusToActive
                    {
                    }
                }

                [DisplayName("Проверка на технически отчет")]
                public static class ContractReportTechnicalCheck
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Актуална)")]
                    public static class ChangeStatusToActive
                    {
                    }
                }

                [DisplayName("Верифицирани РОД")]
                public static class ContractReportFinancialCSDBudgetItem
                {
                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Технически проверен")]
                    public static class TechCheck
                    {
                    }

                    [DisplayName("Редакция при сертификация")]
                    public static class CertUpdate
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус на сертификация (Приключен)")]
                    public static class ChangeCertStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус на сертификация (Чернова)")]
                    public static class ChangeCertStatusToDraft
                    {
                    }
                }

                [DisplayName("Свързани документи")]
                public static class AttachedDocuments
                {
                    [DisplayName("Свързани документи за коригиране на верифицирани суми на ниво РОД")]
                    public static class AttachedFinancialCorrections
                    {
                        [DisplayName("Присъединяване")]
                        public static class Attach
                        {
                        }

                        [DisplayName("Премахване")]
                        public static class Detach
                        {
                        }
                    }
                }

                [DisplayName("Верифицирани Индикатори")]
                public static class ContractReportIndicator
                {
                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }
                }

                [DisplayName("Верифициранo АП")]
                public static class ContractReportAdvancePaymentAmount
                {
                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Редакция при сертификация")]
                    public static class CertUpdate
                    {
                    }

                    [DisplayName("Смяна на статус на сертификация (Приключен)")]
                    public static class ChangeCertStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус на сертификация (Чернова)")]
                    public static class ChangeCertStatusToDraft
                    {
                    }
                }

                [DisplayName("АП")]
                public static class ContractReportAdvanceNVPaymentAmount
                {
                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ContractReportCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Планове към проверки на място")]
        public static class SpotCheckPlans
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Данни за плана")]
                public static class PlanData
                {
                }

                [DisplayName("Обхват")]
                public static class Items
                {
                    [DisplayName("Добавяне")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Обекти на проверка")]
                public static class Targets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи към план")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Проверки на място")]
        public static class SpotChecks
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Проверка")]
                public static class CheckData
                {
                }

                [DisplayName("Обхват")]
                public static class Items
                {
                    [DisplayName("Добавяне")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Констатации")]
                public static class Ascertainments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Обекти на проверка")]
                public static class Targets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Препоръки")]
                public static class Recommendations
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                        [DisplayName("Препоръки")]
                        public static class Ascertainments
                        {
                            [DisplayName("Добавяне")]
                            public static class Create
                            {
                            }

                            [DisplayName("Изтриване")]
                            public static class Delete
                            {
                            }
                        }
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи към проверка")]
                public static class CheckDocuments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи към план")]
                public static class PlanDocuments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class SpotCheckCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Одити")]
        public static class Audits
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Одит")]
                public static class AuditData
                {
                }

                [DisplayName("Обхват на проверката")]
                public static class Items
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Проверявани проекти")]
                public static class Projects
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Констатации")]
                public static class Ascertainments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи към одит")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Финансови корекции за системни пропуски")]
        public static class FlatFinancialCorrections
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на статус (Актуална)")]
            public static class ChangeStatusToActual
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Елемент от тип 'Програма' към финансова корекция за системни пропуски")]
                public static class ProgrammeItem
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Елемент от тип 'Приоритетна ос' към финансова корекция за системни пропуски")]
                public static class ProgrammePriorityItem
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Елемент от тип 'Процедура' към финансова корекция за системни пропуски")]
                public static class ProcedureItem
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Елемент от тип 'Договор за БФП' към финансова корекция за системни пропуски")]
                public static class ContractItem
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Елемент от тип 'Договор с изпълнител' към финансова корекция за системни пропуски")]
                public static class ContractContractItem
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Финансови корекции")]
        public static class FinancialCorrections
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Анулиране")]
                public static class Cancel
                {
                }

                [DisplayName("Изтриване")]
                public static class Delete
                {
                }

                [DisplayName("Смяна на статус (Въведена)")]
                public static class ChangeStatusToEntered
                {
                }

                [DisplayName("Версии")]
                public static class Versions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Актуална)")]
                    public static class ChangeStatusToActual
                    {
                    }
                }
            }
        }

        [DisplayName("Реално изплатени суми")]
        public static class ActuallyPaidAmounts
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Документи")]
                public static class Documents
                {
                }
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Асоцииране с ИП")]
            public static class AssignContractReportPayment
            {
            }

            [DisplayName("Премахване на асоциирано ИП")]
            public static class DissociateContractReportPayment
            {
            }

            [DisplayName("Смяна на асоциирано ИП")]
            public static class ChangeContractReportPayment
            {
            }

            [DisplayName("Документи")]
            public static class Documents
            {
                [DisplayName("Създаване")]
                public static class Create
                {
                }

                [DisplayName("Редакция")]
                public static class Edit
                {
                }

                [DisplayName("Изтриване")]
                public static class Delete
                {
                }
            }
        }

        [DisplayName("Изравнителни документи")]
        public static class CompensationDocuments
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Изравнителен документ")]
                public static class CompensationDocData
                {
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Възстановени суми")]
        public static class DebtReimbursedAmounts
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }
        }

        [DisplayName("Възстановени суми по договор")]
        public static class ContractReimbursedAmounts
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Обвързване с дълг")]
            public static class AttachToContractDebt
            {
            }
        }

        [DisplayName("Възстановени суми по ФИ")]
        public static class FIReimbursedAmounts
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }
        }

        [DisplayName("Сигнали за нередности")]
        public static class IrregularitySignals
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на статус (Активен)")]
            public static class ChangeStatusToActive
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Анулиран)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Сигнал за нередност")]
                public static class SignalData
                {
                }

                [DisplayName("Документи към сигнал")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Замесени лица")]
                public static class InvolvedPersons
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class IrregularitySignalCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("База данни нередности")]
        public static class Irregularities
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Анулиране")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Уведомления за нередност")]
                public static class Versions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Активиране")]
                    public static class Activate
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                        [DisplayName("Редакция на данни")]
                        public static class EditData
                        {
                        }

                        [DisplayName("Замесени лица")]
                        public static class InvolvedPersons
                        {
                            [DisplayName("Създаване")]
                            public static class Create
                            {
                            }

                            [DisplayName("Редакция")]
                            public static class Edit
                            {
                            }

                            [DisplayName("Изтриване")]
                            public static class Delete
                            {
                            }
                        }

                        [DisplayName("Документи")]
                        public static class Documents
                        {
                            [DisplayName("Създаване")]
                            public static class Create
                            {
                            }

                            [DisplayName("Редакция")]
                            public static class Edit
                            {
                            }

                            [DisplayName("Изтриване")]
                            public static class Delete
                            {
                            }
                        }
                    }
                }

                [DisplayName("Финансови корекции")]
                public static class FinancialCorrections
                {
                    [DisplayName("Добавяне")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Годишни счетоводни отчети")]
        public static class AnnualAccountReports
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Отключен)")]
            public static class ChangeStatusToOpened
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Корекции на верифицирани суми на ниво РОД")]
                public static class FinancialCorrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Одобряване")]
                    public static class Certify
                    {
                    }

                    [DisplayName("Неодобряване")]
                    public static class Uncertify
                    {
                    }

                    [DisplayName("Разходооправдателни документи")]
                    public static class CSDs
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }

                        [DisplayName("Одобряване")]
                        public static class Certify
                        {
                        }

                        [DisplayName("Неодобряване")]
                        public static class Uncertify
                        {
                        }
                    }
                }

                [DisplayName("Корекции на сертифицирани суми на ниво РОД")]
                public static class FinancialCertCorrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Разходооправдателни документи")]
                    public static class CSDs
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }
                    }
                }

                [DisplayName("Корекции на препотвърдени сертифицирани суми на ниво РОД")]
                public static class CertRevalidationFinancialCorrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Разходооправдателни документи")]
                    public static class CSDs
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }
                    }
                }

                [DisplayName("Корекции на верифицирани суми на други нива")]
                public static class Corrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Одобряване")]
                    public static class Certify
                    {
                    }

                    [DisplayName("Неодобряване")]
                    public static class Uncertify
                    {
                    }
                }

                [DisplayName("Корекции на сертифицирани суми на други нива")]
                public static class CertCorrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Корекции на препотвърдени сертифицирани суми на други нива")]
                public static class CertRevalidationCorrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи СО")]
                public static class CertificationDocuments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи OО")]
                public static class AuditDocuments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("ДС от предишни периоди")]
                public static class AttachedCertReports
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Коментари приложение 5")]
                public static class Appendices5
                {
                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }
                }

                [DisplayName("Коментари приложение 8")]
                public static class Appendices8
                {
                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }
                }
            }
        }

        [DisplayName("Доклади по сертификация")]
        public static class CertReports
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Непроверен)")]
            public static class ChangeStatusToUnchecked
            {
            }

            [DisplayName("Смяна на статус (Одобрен)")]
            public static class ChangeStatusToApproved
            {
            }

            [DisplayName("Смяна на статус (Частично одобрен)")]
            public static class ChangeStatusToPartialyApproved
            {
            }

            [DisplayName("Смяна на статус (Неодобрен)")]
            public static class ChangeStatusToUnapproved
            {
            }

            [DisplayName("Смяна на статус (Върнат)")]
            public static class ChangeStatusToReturned
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Искания за плащане")]
                public static class Payments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Одобряване")]
                    public static class Certify
                    {
                    }

                    [DisplayName("Неодобряване")]
                    public static class Uncertify
                    {
                    }

                    [DisplayName("Разходооправдателни документи")]
                    public static class CSDs
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }

                        [DisplayName("Одобряване")]
                        public static class Certify
                        {
                        }

                        [DisplayName("Неодобряване")]
                        public static class Uncertify
                        {
                        }
                    }
                }

                [DisplayName("Искания за плащане по чл.131")]
                public static class AdvancePayments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Одобряване")]
                    public static class Certify
                    {
                    }

                    [DisplayName("Неодобряване")]
                    public static class Uncertify
                    {
                    }

                    [DisplayName("Авансови плащания")]
                    public static class Amounts
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }

                        [DisplayName("Одобряване")]
                        public static class Certify
                        {
                        }

                        [DisplayName("Неодобряване")]
                        public static class Uncertify
                        {
                        }
                    }
                }

                [DisplayName("Корекции на верифицирани суми на ниво РОД")]
                public static class FinancialCorrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Одобряване")]
                    public static class Certify
                    {
                    }

                    [DisplayName("Неодобряване")]
                    public static class Uncertify
                    {
                    }

                    [DisplayName("Разходооправдателни документи")]
                    public static class CSDs
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }

                        [DisplayName("Одобряване")]
                        public static class Certify
                        {
                        }

                        [DisplayName("Неодобряване")]
                        public static class Uncertify
                        {
                        }
                    }
                }

                [DisplayName("Препотвърждавания на ниво РОД")]
                public static class FinancialRevalidations
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Одобряване")]
                    public static class Certify
                    {
                    }

                    [DisplayName("Неодобряване")]
                    public static class Uncertify
                    {
                    }

                    [DisplayName("Разходооправдателни документи")]
                    public static class CSDs
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }

                        [DisplayName("Одобряване")]
                        public static class Certify
                        {
                        }

                        [DisplayName("Неодобряване")]
                        public static class Uncertify
                        {
                        }
                    }
                }

                [DisplayName("Корекции на сертифицирани суми на ниво РОД")]
                public static class FinancialCertCorrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Разходооправдателни документи")]
                    public static class CSDs
                    {
                        [DisplayName("Създаване")]
                        public static class Create
                        {
                        }

                        [DisplayName("Изтриване")]
                        public static class Delete
                        {
                        }
                    }
                }

                [DisplayName("Корекции на верифицирани суми на други нива")]
                public static class Corrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Одобряване")]
                    public static class Certify
                    {
                    }

                    [DisplayName("Неодобряване")]
                    public static class Uncertify
                    {
                    }
                }

                [DisplayName("Препотвърждавания на други нива")]
                public static class Revalidations
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Одобряване")]
                    public static class Certify
                    {
                    }

                    [DisplayName("Неодобряване")]
                    public static class Uncertify
                    {
                    }
                }

                [DisplayName("Корекции на сертифицирани суми на други нива")]
                public static class CertCorrections
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи СО")]
                public static class CertificationDocuments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("ДС от предишни периоди")]
                public static class AttachedCertReports
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Дългове по договори")]
                public static class ContractDebts
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Възстановени суми")]
                public static class DebtReimbursedAmounts
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class CertReportCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Дългове към договор")]
        public static class ContractDebts
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Изтриване")]
                public static class Delete
                {
                }

                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Анулиране")]
                public static class Cancel
                {
                }

                [DisplayName("Версии")]
                public static class Versions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Частична редакция")]
                    public static class EditPartial
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Актуална)")]
                    public static class ChangeStatusToActual
                    {
                    }
                }

                [DisplayName("Лихви")]
                public static class Interests
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Дългове по ФКСП")]
        public static class CorrectionDebts
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Изтриване")]
                public static class Delete
                {
                }

                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Анулиране")]
                public static class Cancel
                {
                }

                [DisplayName("Версии")]
                public static class Versions
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Актуална)")]
                    public static class ChangeStatusToActual
                    {
                    }
                }
            }
        }

        [DisplayName("Проверки на СО")]
        public static class CertAuthorityChecks
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Проверка")]
                public static class CheckData
                {
                }

                [DisplayName("Обхват на проверката")]
                public static class Items
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Констатации")]
                public static class Ascertainments
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Възстановени от ЕК суми")]
        public static class EuReimbursedAmounts
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Сума")]
                public static class AmountData
                {
                }

                [DisplayName("Доклади по сертификация")]
                public static class CertReports
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Комуникация сертифициращ орган")]
        public static class CertAuthorityCommunications
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция на документ")]
            public static class UpdateXml
            {
            }

            [DisplayName("Смяна на статус (Изпратено съобщение)")]
            public static class ChangeStatusToSent
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Комуникация одитен орган")]
        public static class AuditAuthorityCommunications
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция на документ")]
            public static class UpdateXml
            {
            }

            [DisplayName("Смяна на статус (Изпратено съобщение)")]
            public static class ChangeStatusToSent
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Коригиране на верифицирани суми на ниво РОД")]
        public static class ContractReportFinancialCorrection
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Коригирани верифицирани РОД")]
                public static class CSD
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Редакция при сертификация")]
                    public static class CertUpdate
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус на сертификация (Приключен)")]
                    public static class ChangeCertStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус на сертификация (Чернова)")]
                    public static class ChangeCertStatusToDraft
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ContractReportFinancialCorrectionCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Коригиране на верифицирани индикатори")]
        public static class ContractReportTechnicalCorrection
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Коригирани верифицирани индикатори")]
                public static class Indicator
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }
                }
            }
        }

        [DisplayName("Коригиране на верифицирани суми на други нива")]
        public static class ContractReportCorrections
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Редакция при сертификация")]
            public static class CertUpdate
            {
            }

            [DisplayName("Смяна на статус на сертификация (Приключен)")]
            public static class ChangeCertStatusToEnded
            {
            }

            [DisplayName("Смяна на статус на сертификация (Чернова)")]
            public static class ChangeCertStatusToDraft
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Корекция на верифицирани суми на други нива")]
                public static class ContractReportCorrectionData
                {
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ContractReportCorrectionCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Препотвърждаване на верифицирани суми на ниво РОД")]
        public static class ContractReportFinancialRevalidation
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Препотвърдени верифицирани РОД")]
                public static class CSD
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Редакция при сертификация")]
                    public static class CertUpdate
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }

                    [DisplayName("Смяна на статус на сертификация (Приключен)")]
                    public static class ChangeCertStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус на сертификация (Чернова)")]
                    public static class ChangeCertStatusToDraft
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ContractReportFinancialRevalidationCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Препотвърждаване на верифицирани суми на други нива")]
        public static class ContractReportRevalidations
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Редакция при сертификация")]
            public static class CertUpdate
            {
            }

            [DisplayName("Смяна на статус на сертификация (Приключен)")]
            public static class ChangeCertStatusToEnded
            {
            }

            [DisplayName("Смяна на статус на сертификация (Чернова)")]
            public static class ChangeCertStatusToDraft
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Корекция на верифицирани суми на други нива")]
                public static class ContractReportRevalidationData
                {
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Контролен лист")]
                public static class ContractReportRevalidationCheckSheets
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Смяна на стаус (Анулиран)")]
                    public static class ChangeStatusToCanceled
                    {
                    }

                    [DisplayName("Смяна на стаус (В изпълнение)")]
                    public static class ChangeStatusToActive
                    {
                    }

                    [DisplayName("Връщане")]
                    public static class Return
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Изравняване на сертифицирани суми на ниво РОД")]
        public static class ContractReportFinancialCertCorrection
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Коригирани сертифицирани РОД")]
                public static class CSD
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Редакция при сертификация")]
                    public static class CertUpdate
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }
                }
            }
        }

        [DisplayName("Изравняване на сертифицирани суми на други нива")]
        public static class ContractReportCertCorrections
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Корекция на верифицирани суми на други нива")]
                public static class ContractReportCertCorrectionData
                {
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Коригиране от СО на сертифицирани суми на ниво РОД")]
        public static class ContractReportCertAuthorityFinancialCorrection
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Коригирани сертифицирани РОД")]
                public static class CSD
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Редакция при сертификация")]
                    public static class CertUpdate
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }
                }
            }
        }

        [DisplayName("Коригиране от СО на сертифицирани суми на други нива")]
        public static class ContractReportCertAuthorityCorrections
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Корекция от СО на верифицирани суми на други нива")]
                public static class ContractReportCertAuthorityCorrectionData
                {
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Коригиране от СО на препотвърдени сертифицирани суми на ниво РОД")]
        public static class ContractReportRevalidationCertAuthorityFinancialCorrection
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Приключен)")]
            public static class ChangeStatusToEnded
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Основни данни")]
                public static class BasicData
                {
                }

                [DisplayName("Коригирани сертифицирани РОД")]
                public static class CSD
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }

                    [DisplayName("Редакция при сертификация")]
                    public static class CertUpdate
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }

                    [DisplayName("Смяна на статус (Приключен)")]
                    public static class ChangeStatusToEnded
                    {
                    }

                    [DisplayName("Смяна на статус (Чернова)")]
                    public static class ChangeStatusToDraft
                    {
                    }
                }
            }
        }

        [DisplayName("Коригиране от СО на препотвърдени сертифицирани суми на други нива")]
        public static class ContractReportRevalidationCertAuthorityCorrections
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Корекция от СО на препотвърдени суми на други нива")]
                public static class ContractReportRevalidationCertAuthorityCorrectionData
                {
                }

                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }
        }

        [DisplayName("Файлове от САП")]
        public static class SapFiles
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Импортиране")]
            public static class Import
            {
            }
        }

        [DisplayName("Прогнози")]
        public static class Prognoses
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Смяна на статус (Въведена)")]
            public static class ChangeStatusToEntered
            {
            }

            [DisplayName("Смяна на статус (Анулирана)")]
            public static class ChangeStatusToRemoved
            {
            }
        }

        [DisplayName("Контролен лист")]
        public static class CheckSheets
        {
            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Версии")]
                public static class Versions
                {
                    [DisplayName("Редакция на документ")]
                    public static class UpdateXml
                    {
                    }
                }

                [DisplayName("Смяна на статус (Приключен)")]
                public static class ChangeStatusToEnded
                {
                }

                [DisplayName("Смяна на статус (Прекъснат)")]
                public static class ChangeStatusToPaused
                {
                }
            }
        }

        [DisplayName("Вход в системата")]
        public static class Login
        {
            [DisplayName("Успешен")]
            public static class Successful
            {
            }
        }

        [DisplayName("Данни от Мониторстат")]
        public static class MonitorstatSurvey
        {
            [DisplayName("Импортиране на отчети")]
            public static class Import
            {
            }
        }

        [DisplayName("Извличане на данни от RegiX")]
        public static class RegiXService
        {
            [DisplayName("Справка за валидност на физическо лице")]
            public static class ValidPerson
            {
            }

            [DisplayName("Справка за лице по документи за самоличност")]
            public static class PersonalIdentity
            {
            }

            [DisplayName("Справка за актуално състояние")]
            public static class ActualState
            {
            }

            [DisplayName("Справка по код на Булстат")]
            public static class StateOfPlay
            {
            }

            [DisplayName("Справка за вписано юридическо лице с нестопанска цел")]
            public static class NpoRegistration
            {
            }
        }

        [DisplayName("Нотификации към чек листове")]
        public static class CheckSheetNotifications
        {
            [DisplayName("Известяване на следващи отговарящи потребители")]
            public static class NotifyNextRespondents
            {
            }
        }

        [DisplayName("Обща кореспонденция")]
        public static class ProcedureMassCommunication
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Получатели")]
                public static class Recipients
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }

            [DisplayName("Изпращане")]
            public static class Send
            {
            }
        }

        [DisplayName("Обща комуникация с УО")]
        public static class ProjectMassManagingAuthorityCommunication
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Документи")]
                public static class Documents
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }

                [DisplayName("Получатели")]
                public static class Recipients
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Edit
                    {
                    }

                    [DisplayName("Изтриване")]
                    public static class Delete
                    {
                    }
                }
            }

            [DisplayName("Изпращане")]
            public static class Send
            {
            }
        }
    }
}
