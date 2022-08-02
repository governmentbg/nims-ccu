using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Eumis.Log.ActionLogger.Enums
{
    [SuppressMessage("", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Classes nested for ease of use.")]
    public static class ActionLogPortalGroups
    {
        [DisplayName("Регистрация")]
        public static class Registrations
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Активиране")]
            public static class Activate
            {
            }

            [DisplayName("Възстановяване на забравена парола")]
            public static class RecoverPassword
            {
            }

            [DisplayName("Рекдация на регистрационни данни")]
            public static class UpdateRegistrationInfo
            {
            }

            [DisplayName("Промяна на парола")]
            public static class ChangeCurrentUserPassword
            {
            }
        }

        [DisplayName("Регистрация за достъп към договор")]
        public static class ContractRegistrations
        {
            [DisplayName("Активиране")]
            public static class Activate
            {
            }

            [DisplayName("Рекдация на регистрационни данни")]
            public static class UpdateRegistrationInfo
            {
            }

            [DisplayName("Промяна на парола")]
            public static class ChangeCurrentUserPassword
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
                [DisplayName("Код за достъп към договор")]
                public static class AccessCodes
                {
                    [DisplayName("Създаване")]
                    public static class Create
                    {
                    }

                    [DisplayName("Редакция")]
                    public static class Update
                    {
                    }
                }
            }
        }

        [DisplayName("Процедури за избор на изпълнител и сключени договори")]
        public static class ContractProcurements
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
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

        [DisplayName("План за разходване на средства към договор")]
        public static class ContractSpendingPlans
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
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

        [DisplayName("Кореспонденция към договор")]
        public static class ContractCommunications
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция")]
            public static class UpdateXml
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }

            [DisplayName("Смяна на статус (Изпратено съобщение)")]
            public static class ChangeStatusToSent
            {
            }
        }

        [DisplayName("Проектно предложение")]
        public static class RegProjectXmls
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

            [DisplayName("Приключване")]
            public static class Finalize
            {
            }

            [DisplayName("Създаване на чернова")]
            public static class MakeDraft
            {
            }

            [DisplayName("Хартиено подаване")]
            public static class Submit
            {
            }

            [DisplayName("Електронно подаване")]
            public static class Register
            {
            }
        }

        [DisplayName("Въпроси и отговори")]
        public static class ProjectCommunicationAnswer
        {
            [DisplayName("Редакция на отговор")]
            public static class Update
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraftAnswer
            {
            }

            [DisplayName("Смяна на статус (За изпращане)")]
            public static class ChangeStatusToAnswerFinalized
            {
            }

            [DisplayName("Смяна на статус (На хартия)")]
            public static class ChangeStatusToPaperAnswer
            {
            }

            [DisplayName("Смяна на статус (Изпратено)")]
            public static class ChangeStatusToAnswer
            {
            }

            [DisplayName("Изтриване")]
            public static class Delete
            {
            }
        }

        [DisplayName("Комуникация между УО и бенефициент към проектно предложение")]
        public static class ProjectManagingAuthorityCommunication
        {
            [DisplayName("Въпрос")]
            public static class Questions
            {
                [DisplayName("Редакция на въпрос")]
                public static class Update
                {
                }

                [DisplayName("Смяна на статус 'Въпрос'")]
                public static class Submit
                {
                }

                [DisplayName("Изтриване")]
                public static class Delete
                {
                }
            }

            [DisplayName("Отговор")]
            public static class Answers
            {
                [DisplayName("Редакция на отговор")]
                public static class Update
                {
                }

                [DisplayName("Смяна на статус 'Отговор'")]
                public static class Submit
                {
                }

                [DisplayName("Изтриване")]
                public static class Delete
                {
                }
            }
        }

        [DisplayName("Пакет отчетни документи към договор")]
        public static class ContractReports
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Създаване чрез копиране")]
            public static class Copy
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

            [DisplayName("Смяна на статус (Изпратен)")]
            public static class ChangeStatusToSent
            {
            }

            [DisplayName("Смяна на статус (Чернова)")]
            public static class ChangeStatusToDraft
            {
            }

            [DisplayName("Редакция")]
            public static class Edit
            {
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

                    [DisplayName("Редакция на Excel с код от СИМЕВ")]
                    public static class UpdateExcelWithSimevCode
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
                }
            }
        }

        [DisplayName("Оферта")]
        public static class RegOfferXmls
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }

            [DisplayName("Редакция на чернова")]
            public static class Update
            {
            }

            [DisplayName("Подаване")]
            public static class Submit
            {
            }

            [DisplayName("Оттегляне")]
            public static class Withdraw
            {
            }
        }

        [DisplayName("Коментар/предложение")]
        public static class PublicDiscussionComment
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }
        }

        [DisplayName("Искане за разяснение")]
        public static class ProcedureDiscussion
        {
            [DisplayName("Създаване")]
            public static class Create
            {
            }
        }
    }
}
