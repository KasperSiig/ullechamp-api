using System.Collections;
using System.Collections.Generic;
using Ullechamp_Api.Core.Entity;

namespace Ullechamp_Api.Core.DomainService
{
    public interface ICalenderRepository
    {
        #region Create
        /// <summary>
        /// Creates CalenderItem in database
        /// </summary>
        /// <param name="calenderItem"> CalenderItem to be inserted in database</param>
        /// <returns> Created CalenderItem</returns>
        CalenderItem CreateCalender(CalenderItem calenderItem);
        #endregion

        #region Read
        /// <summary>
        /// Reads all CalenderItems from database
        /// </summary>
        /// <returns> A IEnumerable of all CalenderItems</returns>
        IEnumerable<CalenderItem> ReadCalender();
        #endregion
        
    }
}