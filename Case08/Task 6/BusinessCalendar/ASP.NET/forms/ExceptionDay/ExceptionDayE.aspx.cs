﻿/*flexberryautogenerated="false"*/

namespace IIS.BusinessCalendar
{
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Web.Controls;
    using ICSSoft.STORMNET.Web.AjaxControls;
    
    public partial class ExceptionDayE : BaseEditForm<ExceptionDay>
    {
        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public ExceptionDayE()
            : base(ExceptionDay.Views.ExceptionDayE)
        {
        }

        /// <summary>
        /// Путь до формы.
        /// </summary>
        public static string FormPath
        {
            get { return "~/forms/ExceptionDay/ExceptionDayE.aspx"; }
        }

        /// <summary>
        /// Вызывается самым первым в Page_Load.
        /// </summary>
        protected override void Preload()
        {
        }

        /// <summary>
        /// Здесь лучше всего писать бизнес-логику, оперируя только объектом данных.
        /// </summary>
        protected override void PreApplyToControls()
        {
        }

        /// <summary>
        /// Здесь лучше всего изменять свойства контролов на странице,
        /// которые не обрабатываются WebBinder.
        /// </summary>
        protected override void PostApplyToControls()
        {
            Page.Validate();
        }

        /// <summary>
        /// Вызывается самым последним в Page_Load.
        /// </summary>
        protected override void Postload()
        {
            ICSSoft.STORMNET.Web.Tools.PageContentManager.AttachExternalFile("/shared/script/jquery-1.7.2.min.js");
            ICSSoft.STORMNET.Web.Tools.PageContentManager.AttachExternalFile("/JavaScript/validation.js");
            ICSSoft.STORMNET.Web.Tools.PageContentManager.AttachExternalFile("/JavaScript/exception-day-form.js");
        }

        /// <summary>
        /// Валидация объекта для сохранения.
        /// </summary>
        /// <returns>true - продолжать сохранение, иначе - прекратить.</returns>
        protected override bool PreSaveObject()
        {
            return base.PreSaveObject();
        }

        /// <summary>
        /// Нетривиальная логика сохранения объекта.
        /// </summary>
        /// <returns>Объект данных, который сохранился.</returns>
        protected override DataObject SaveObject()
        {
            if((DataObject != null) && ((DataObject.GetStatus() == ObjectStatus.Created)|| (ctrlWorkTimeSpans.Status == ObjectStatus.Altered)))
            {
                TSSaveHelper.UpdateTimeSpans(DataObject);
            }
            return base.SaveObject();
        }
    }
}