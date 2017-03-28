﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IIS.BusinessCalendar
{
    using System;
    using System.Xml;
    using ICSSoft.STORMNET;
    
    
    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// Временные промежутки.
    /// </summary>
    // *** Start programmer edit section *** (WorkTimeSpan CustomAttributes)

    // *** End programmer edit section *** (WorkTimeSpan CustomAttributes)
    [AutoAltered()]
    [Caption("Временные промежукти")]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("WorkTimeSpanE", new string[] {
            "StartTime as \'Время начала\'",
            "EndTime as \'Время окончания\'"})]
    public class WorkTimeSpan : ICSSoft.STORMNET.DataObject
    {
        
        private decimal fStartTime;
        
        private decimal fEndTime;
        
        private IIS.BusinessCalendar.WorkTimeDefinition fWorkTimeDefinition;
        
        // *** Start programmer edit section *** (WorkTimeSpan CustomMembers)

        // *** End programmer edit section *** (WorkTimeSpan CustomMembers)

        
        /// <summary>
        /// StartTime.
        /// </summary>
        // *** Start programmer edit section *** (WorkTimeSpan.StartTime CustomAttributes)

        // *** End programmer edit section *** (WorkTimeSpan.StartTime CustomAttributes)
        public virtual decimal StartTime
        {
            get
            {
                // *** Start programmer edit section *** (WorkTimeSpan.StartTime Get start)

                // *** End programmer edit section *** (WorkTimeSpan.StartTime Get start)
                decimal result = this.fStartTime;
                // *** Start programmer edit section *** (WorkTimeSpan.StartTime Get end)

                // *** End programmer edit section *** (WorkTimeSpan.StartTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (WorkTimeSpan.StartTime Set start)

                // *** End programmer edit section *** (WorkTimeSpan.StartTime Set start)
                this.fStartTime = value;
                // *** Start programmer edit section *** (WorkTimeSpan.StartTime Set end)

                // *** End programmer edit section *** (WorkTimeSpan.StartTime Set end)
            }
        }
        
        /// <summary>
        /// EndTime.
        /// </summary>
        // *** Start programmer edit section *** (WorkTimeSpan.EndTime CustomAttributes)

        // *** End programmer edit section *** (WorkTimeSpan.EndTime CustomAttributes)
        public virtual decimal EndTime
        {
            get
            {
                // *** Start programmer edit section *** (WorkTimeSpan.EndTime Get start)

                // *** End programmer edit section *** (WorkTimeSpan.EndTime Get start)
                decimal result = this.fEndTime;
                // *** Start programmer edit section *** (WorkTimeSpan.EndTime Get end)

                // *** End programmer edit section *** (WorkTimeSpan.EndTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (WorkTimeSpan.EndTime Set start)

                // *** End programmer edit section *** (WorkTimeSpan.EndTime Set start)
                this.fEndTime = value;
                // *** Start programmer edit section *** (WorkTimeSpan.EndTime Set end)

                // *** End programmer edit section *** (WorkTimeSpan.EndTime Set end)
            }
        }
        
        /// <summary>
        /// мастеровая ссылка на шапку IIS.BusinessCalendar.WorkTimeDefinition.
        /// </summary>
        // *** Start programmer edit section *** (WorkTimeSpan.WorkTimeDefinition CustomAttributes)

        // *** End programmer edit section *** (WorkTimeSpan.WorkTimeDefinition CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "WorkTimeDefinition"})]
        public virtual IIS.BusinessCalendar.WorkTimeDefinition WorkTimeDefinition
        {
            get
            {
                // *** Start programmer edit section *** (WorkTimeSpan.WorkTimeDefinition Get start)

                // *** End programmer edit section *** (WorkTimeSpan.WorkTimeDefinition Get start)
                IIS.BusinessCalendar.WorkTimeDefinition result = this.fWorkTimeDefinition;
                // *** Start programmer edit section *** (WorkTimeSpan.WorkTimeDefinition Get end)

                // *** End programmer edit section *** (WorkTimeSpan.WorkTimeDefinition Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (WorkTimeSpan.WorkTimeDefinition Set start)

                // *** End programmer edit section *** (WorkTimeSpan.WorkTimeDefinition Set start)
                this.fWorkTimeDefinition = value;
                // *** Start programmer edit section *** (WorkTimeSpan.WorkTimeDefinition Set end)

                // *** End programmer edit section *** (WorkTimeSpan.WorkTimeDefinition Set end)
            }
        }
        
        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {
            
            /// <summary>
            /// "WorkTimeSpanE" view.
            /// </summary>
            public static ICSSoft.STORMNET.View WorkTimeSpanE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("WorkTimeSpanE", typeof(IIS.BusinessCalendar.WorkTimeSpan));
                }
            }
        }
    }
    
    /// <summary>
    /// Detail array of WorkTimeSpan.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfWorkTimeSpan CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfWorkTimeSpan CustomAttributes)
    public class DetailArrayOfWorkTimeSpan : ICSSoft.STORMNET.DetailArray
    {
        
        // *** Start programmer edit section *** (IIS.BusinessCalendar.DetailArrayOfWorkTimeSpan members)

        // *** End programmer edit section *** (IIS.BusinessCalendar.DetailArrayOfWorkTimeSpan members)

        
        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type WorkTimeSpan by index.
        /// </summary>
        /// <summary>
        /// Adds object with type WorkTimeSpan.
        /// </summary>
        public DetailArrayOfWorkTimeSpan(IIS.BusinessCalendar.WorkTimeDefinition fWorkTimeDefinition) : 
                base(typeof(WorkTimeSpan), ((ICSSoft.STORMNET.DataObject)(fWorkTimeDefinition)))
        {
        }
        
        public IIS.BusinessCalendar.WorkTimeSpan this[int index]
        {
            get
            {
                return ((IIS.BusinessCalendar.WorkTimeSpan)(this.ItemByIndex(index)));
            }
        }
        
        public virtual void Add(IIS.BusinessCalendar.WorkTimeSpan dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}
