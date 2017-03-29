﻿/*flexberryautogenerated="false"*/

namespace TeploCorp.TeploUchet
{
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Web.Controls;
    using ICSSoft.STORMNET.FunctionalLanguage.SQLWhere;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using System.Linq;
    using ICSSoft.STORMNET.FunctionalLanguage;
    using System.Web;

    public partial class ЗданиеE : BaseEditForm<Здание>
    {
        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public ЗданиеE()
            : base(Здание.Views.ЗданиеE)
        {
        }

        /// <summary>
        /// Путь до формы.
        /// </summary>
        public static string FormPath
        {
            get { return "~/forms/Zdanie/ZdanieE.aspx"; }
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
            ctrlРайон.PropertyToShow = Information.ExtractPropertyPath<Район>(x => x.Название);
            Page.Validate();
        }

        /// <summary>
        /// Вызывается самым последним в Page_Load.
        /// </summary>
        protected override void Postload()
        {
            string strUser = HttpContext.Current.User.Identity.Name; //логин пользователя
            var _dataService = (SQLDataService)DataServiceProvider.DataService; //сервис для получения объекта
            var _Inspector = _dataService.Query<Инспектор>(Инспектор.Views.ИнспекторL).FirstOrDefault(x => x.Логин == strUser); // получаем объект инспектор по логину

            //если есть инспектор с таким логином 
            //ограничиваем по его району
            if (_Inspector != null)
            {
                SQLWhereLanguageDef langdef = SQLWhereLanguageDef.LanguageDef;
                var strDistrictName = _Inspector.Район.Название;
                //_Inspector.Район.Название; //название района инспектора

                Function lf = langdef.GetFunction(langdef.funcAND,
                                    langdef.GetFunction(langdef.funcEQ,
                                        new VariableDef(langdef.StringType, Information.ExtractPropertyPath<Район>(x => x.Название)),
                                        strDistrictName),
                                    langdef.GetFunction(langdef.funcEQ,
                                        new VariableDef(langdef.StringType, Information.ExtractPropertyPath<Район>(x => x.Актуален)),
                                        true));
                ctrlРайон.LimitFunction = lf;
            };
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
    }
}