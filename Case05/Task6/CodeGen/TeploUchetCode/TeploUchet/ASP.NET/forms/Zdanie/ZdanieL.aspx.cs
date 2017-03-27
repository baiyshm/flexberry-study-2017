﻿/*flexberryautogenerated="false"*/
namespace TeploCorp.TeploUchet
{
    using System;
    using ICSSoft.STORMNET.Web.Controls;

    using Resources;
    using System.Web;
    using ICSSoft.STORMNET.FunctionalLanguage.SQLWhere;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using System.Linq;
    using ICSSoft.STORMNET.FunctionalLanguage;
    using ICSSoft.STORMNET;

    public partial class ЗданиеL : BaseListForm<Здание>
    {
        /// <summary>
        /// Конструктор без параметров,
        /// инициализирует свойства, соответствующие конкретной форме.
        /// </summary>
        public ЗданиеL() : base(Здание.Views.ЗданиеL)
        {
            EditPage = ЗданиеE.FormPath;
        }
                
        /// <summary>
        /// Путь до формы.
        /// </summary>
        public static string FormPath
        {
            get { return "~/forms/Zdanie/ZdanieL.aspx"; }
        }

        /// <summary>
        /// Вызывается самым первым в Page_Load.
        /// </summary>
        protected override void Preload()
        {       
            string strUser = HttpContext.Current.User.Identity.Name;
            var _dataService = (SQLDataService)DataServiceProvider.DataService;
            var _Inspector = _dataService.Query<Инспектор>(Инспектор.Views.ИнспекторL).FirstOrDefault(x => x.Логин == strUser); // получаем объект инспектор по логину

            if (_Inspector != null)
            {
                SQLWhereLanguageDef langdef = SQLWhereLanguageDef.LanguageDef;
                string strDistrictName = _Inspector.Район.Название; //название района инспектора

                Function lf = langdef.GetFunction(langdef.funcAND,
                                    langdef.GetFunction(langdef.funcEQ,
                                        new VariableDef(langdef.StringType, Information.ExtractPropertyPath<Здание>(x => x.Район.Название)),
                                        strDistrictName),
                                    langdef.GetFunction(langdef.funcEQ,
                                        new VariableDef(langdef.StringType, Information.ExtractPropertyPath<Здание>(x => x.Актуален)),
                                        true));
                WebObjectListView1.LimitFunction = lf;
            };
        }

        /// <summary>
        /// Вызывается самым последним в Page_Load.
        /// </summary>
        protected override void Postload()
        {
        }
    }
}