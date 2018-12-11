using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.ApplicationService
{
    public interface ICalenderService
    {
        #region Create
        /// <summary>
        /// Creates calender in database
        /// </summary>
        /// <param name="calenderItem"> Calender to be inserted in database</param>
        /// <returns> Created calender</returns>
        CalenderItem CreateCalender(CalenderItem calenderItem);
        #endregion

        #region Read
        /// <summary>
        /// Reads all calenders from database
        /// </summary>
        /// <returns> A List of all calenders</returns>
        List<CalenderItem> GetCalenders();
        #endregion
    }
}