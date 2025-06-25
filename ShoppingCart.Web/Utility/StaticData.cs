namespace ShoppingCart.Web.Utility
{
    public class StaticData
    {
        /// <summary>
        /// this static property use for API base URL 
        /// </summary>
        public static string CouponApiBase { get; set; }
        public static string AuthApiBase { get; set; }
        public static string ProductApiBase { get; set; }
        public static string CartApiBase { get; set; }
        public static string OrdersApiBase { get; set; }

        /// <summary>
        /// this const variable use for user role assign
        /// </summary>
        public const string RoleAdmin="ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JWTToken";

        /// <summary>
        /// this const variable use for order status
        /// </summary>
        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_ReadyForPickup = "ReadyForPickup";
        public const string Status_Completed = "Completed";
        public const string Status_Refunded = "Refunded";
        public const string Status_Cancelled = "Cancelled";
        public const string Status_All = "All";
        /// <summary>
        /// this const variable use for order payment status
        /// </summary>
        public const string PaymentStatus_Pending = "Pending";
        public const string PaymentStatus_Paid = "Paid";
        public const string PaymentStatus_Refunded = "Refunded";
        public const string PaymentMethod_CashOnDelivery = "CashOnDelivery";

        /// <summary>
        /// API Enum Verbs 
        /// </summary>
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
