﻿/*flexberryautogenerated="false"*/

namespace IIS.BusinessCalendar
{
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Web.Controls;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;
    using System;
    public partial class ExceptionDayE : BaseEditForm<ExceptionDay>
    {
        private WorkTimeDefinition fWorkTimeDefinition;
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
            if (!Page.IsPostBack)
            {
                this.BindGrid();
            }
        }

        /// <summary>
        /// Здесь лучше всего изменять свойства контролов на странице,
        /// которые не обрабатываются WebBinder.
        /// </summary>
        protected override void PostApplyToControls()
        {
            if((DataObject == null)||(DataObject.WorkTimeDefinition == null))
            {
                fWorkTimeDefinition = new WorkTimeDefinition();
            } else
            {
                fWorkTimeDefinition = DataObject.WorkTimeDefinition;
            }
            if (!Page.IsPostBack)
            {
                this.BindGrid();
            }
            Page.Validate();
        }

        /// <summary>
        /// Вызывается самым последним в Page_Load.
        /// </summary>
        protected override void Postload()
        {
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
            return base.SaveObject();
        }

        protected void BindGrid()
        {
            if(DataObject != null)
            {
                if(DataObject.WorkTimeDefinition != null)
                {
                    ICSSoft.STORMNET.Business.DataServiceProvider.DataService.LoadObject(WorkTimeDefinition.Views.WorkTimeDefinitionE, DataObject.WorkTimeDefinition, false, false);
                    List<WorkTimeSpan> wts = DataObject.WorkTimeDefinition.WorkTimeSpan.Cast<WorkTimeSpan>().ToList();
                    tWorkTimeSpan.DataSource = wts;
                    tWorkTimeSpan.DataBind();
                }  
            } 
        }
        protected void tWorkTimeSpan_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            tWorkTimeSpan.EditIndex = e.NewEditIndex;
            this.BindGrid();
        }

        protected void tWorkTimeSpan_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            tWorkTimeSpan.EditIndex = -1;
            this.BindGrid();
        }

        protected void tWorkTimeSpan_RowUpdated(object sender, System.Web.UI.WebControls.GridViewUpdatedEventArgs e)
        {

        }

        protected void tWorkTimeSpan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (sender as GridView).Rows[e.RowIndex];
            this.editTimeSpan(new ICSSoft.STORMNET.KeyGen.KeyGuid(e.NewValues[2].ToString()), Convert.ToDecimal(e.NewValues[0]), Convert.ToDecimal(e.NewValues[1]));
            tWorkTimeSpan.EditIndex = -1;
            this.BindGrid();
        }

        protected void editTimeSpan(ICSSoft.STORMNET.KeyGen.KeyGuid key,decimal startTime,decimal endTime)
        {
            ICSSoft.STORMNET.Business.DataServiceProvider.DataService.LoadObject(WorkTimeDefinition.Views.WorkTimeDefinitionE, DataObject.WorkTimeDefinition, false, false);
            WorkTimeDefinition wtd = DataObject.WorkTimeDefinition;
            if (wtd != null)
            {
                WorkTimeSpan wts = (WorkTimeSpan)wtd.WorkTimeSpan.GetByKey(key);
                if(wts == null)
                {
                    wts = new WorkTimeSpan();
                    fWorkTimeDefinition.WorkTimeSpan.Add(wts);
                }
                ((WorkTimeSpan)wts).StartTime = startTime;
                ((WorkTimeSpan)wts).EndTime = endTime;
                DataObject.WorkTimeDefinition.WorkTimeSpan.SetByKey(key, wts);
                DataObject.WorkTimeDefinition.SetStatus(ObjectStatus.Altered);
                DataObject.SetStatus(ObjectStatus.Altered);
            }
        }
    }
}