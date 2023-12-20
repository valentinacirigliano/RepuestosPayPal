using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repuestos2023.Utilities
{
    public static  class WC
    {
        public const string Role_User_Individual = "Individual";
        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";

        // Estados de la orden
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProgress = "InProgress";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        // Estados de pago
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayedPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";

        // Nuevos estados específicos de PayPal (adaptar según necesidades específicas)
        public const string PayPalStatusCompleted = "Completed";
        public const string PayPalStatusRefunded = "Refunded";
        public const string PayPalStatusPartiallyRefunded = "PartiallyRefunded";
        public const string PayPalStatusCancelled = "Cancelled";
        public const string PayPalStatusExpired = "Expired";
    }
}
