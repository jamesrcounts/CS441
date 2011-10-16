/***********************************************************************************
 * Author(s): Miguel Gonzales & Andrea Tan
 * Date: Sept 28 2011
 * Modified date: Oct 9 2011
 * Description: this defines an interface for the screens we use in the application.
 *              A screen is a user control that represents a view in the application.
 *              All screens inherit from this.
 *             
 * 
 ************************************************************************************/

namespace TheNewPhotoBuddy.Screens
{
    public interface IScreen
    {
        string DisplayName { get; set; }
    }
}
