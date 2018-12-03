using System.Collections;
using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.DomainService
{
    public interface ICalenderRepository
    {
        #region Create
        /// <summary>
        /// Creates calender in database
        /// </summary>
        /// <param name="calenderItem"> Calender to be inserted in database</param>
        /// <returns> Created calender</returns>
        CalenderItem CreateCalender(CalenderItem calenderItem);
        #endregion


        #region ReadAll
        /// <summary>
        /// Reads all calenders from databse
        /// </summary>
        /// <returns> A IEnumerable of all calenders</returns>
        IEnumerable<CalenderItem> ReadCalender();
        #endregion
        
    }
}