namespace ShoppingCart.Service.OrderApi.Utility
{
    public class StaticData
    {
        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_ReadyForPickup = "ReadyForPickup";
        public const string Status_Completed = "Completed";
        public const string Status_Refunded = "Refunded";
        public const string Status_Cancelled = "Cancelled";


        public const string PaymentStatus_Pending = "Pending";
        public const string PaymentStatus_Paid = "Paid";
        public const string PaymentStatus_Refunded = "Refunded";
        public const string PaymentMethod_CashOnDelivery = "CashOnDelivery";

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
    }
}
