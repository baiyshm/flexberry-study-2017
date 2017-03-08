﻿/*flexberryautogenerated="true"*/
namespace NewPlatform.RecordBook
{
    using System;
    using ICSSoft.STORMNET.Web.Controls;

    using Resources;

    public partial class ПредметL : BaseListForm<Предмет>
    {
        /// <summary>
        /// Конструктор без параметров,
        /// инициализирует свойства, соответствующие конкретной форме.
        /// </summary>
        public ПредметL() : base(Предмет.Views.ПредметL)
        {
            EditPage = ПредметE.FormPath;
        }
                
        /// <summary>
        /// Путь до формы.
        /// </summary>
        public static string FormPath
        {
            get { return "~/forms/Predmet/PredmetL.aspx"; }
        }

        /// <summary>
        /// Вызывается самым первым в Page_Load.
        /// </summary>
        protected override void Preload()
        {
        }

        /// <summary>
        /// Вызывается самым последним в Page_Load.
        /// </summary>
        protected override void Postload()
        {
        }
    }
}