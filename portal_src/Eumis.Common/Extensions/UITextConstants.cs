namespace Eumis.Common.Extensions
{
    /// <summary>
    /// Константи
    /// </summary>
    public class UITextConstants
    {
        /// <summary>
        /// надпис:зареждане
        /// </summary>
        public const string CascadingDropDownLoadingText = "зареждане...";

        /// <summary>
        /// надпис:грешка при зареждане
        /// </summary>
        public const string CascadingDropDownErrorText = "грешка при зареждане.";

        /// <summary>
        /// код
        /// </summary>
        public const string CascadingDropDownParameterName = "code";

        /// <summary>
        /// деактиватор
        /// </summary>
        public const string CascadingDropDownDisabledCssClassName = "disabled";

        /// <summary>
        /// надпис:Моля, изберете
        /// </summary>
        public static string DropDownEmptyItemText
        {
            get
            {
                return Resources.Global.PleaseSelectText;
            }
        }

        public static string DropDownClearItemText
        {
            get
            {
                return Resources.Global.ClearSelectText;
            }
        }
    }
}