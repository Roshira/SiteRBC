namespace SiteRBC.Models
{
    /**
     * @class ErrorViewModel
     * @brief Model class representing error details to be displayed in views.
     * 
     * This class contains properties related to error information, such as the request ID, 
     * which is used to help track requests in case of errors.
     */
    public class ErrorViewModel
    {
        /**
         * @brief Gets or sets the request ID for the error.
         * 
         * This property holds the unique identifier of the request that resulted in an error.
         */
        public string? RequestId { get; set; }

        /**
         * @brief Returns whether the request ID should be shown.
         * @return True if the request ID is not null or empty; otherwise, false.
         * 
         * This property is used to determine if the request ID should be displayed in the view.
         */
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
