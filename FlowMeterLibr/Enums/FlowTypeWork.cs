using System.ComponentModel;

namespace FlowMeterLibr.TO
{
    public enum FlowTypeWork
    {
        [Description("Режим не определён")]
        None = -1,
        /// <summary>
        ///     в режиме настройки
        /// </summary>
        [Description("В режиме настройки")]
        ServiceWork = 0,

        /// <summary>
        ///     в режиме нормальной работы
        /// </summary>
        [Description("В режиме нормальной работы")]
        NormalWork = 1,

        /// <summary>
        ///     Авария
        /// </summary>
        [Description("[ВНИМАНИЕ] Авария")]
        ErrorWork = 3
    }
}