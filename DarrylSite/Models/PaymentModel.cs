using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DarrylSite.Models
{
    public class StripeConstants
    {
        public static string token { get; set; }
        public static string publicKey { get; set; }
        public static string secretKey { get; set; }
        public static string clientId { get; set; }
    }

    public class PayPalModel { }
    public class StripeModel
    {
        public StripeModel()
        {
            charge = null;
            stripeError = null;
            // set these two values to ture to ensure proper behavior
            livemode = true;
            used = true;
            checkoutAmount = 998;

        }
        //public StripeConstants stripeConstants { get; set; }
        public string publicKey { get; set; }
        public string clientId { get; set; }
        public StripeCharge charge { get; set; }
        public StripeError stripeError { get; set; }
        public string tokenId { get; set; }
        public bool livemode { get; set; }
        public string type { get; set; }
        public bool used { get; set; }
        public string stripeEmail { get; set; }
        public int checkoutAmount { get; set; }
    }
    public class PaymentModel
    {
        public PaymentModel()
        {
            PayPalData = new PayPalModel();
            StripeData = new StripeModel();
            //StripeData.publicKey = StripeConstants.publicKey;
            StripeData.clientId = StripeConstants.clientId;
            appError = null;
        }
        public PayPalModel PayPalData { get; set; }
        public StripeModel StripeData { get; set; }
        public string appError { get; set; }
        // fields for Checkout simple form
        public string stripeToken { get; set; }
        public string stripeEmail { get; set; }
        public bool livemode { get; set; }

    }

    [DataContract(Name="error")]
    public class stripeErrorPayload
    {
        public stripeErrorModel error { get; set; }
    }

    public class stripeErrorModel
    {
        public string type { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }

    [DataContract]
    public class stripeReturnCredsModel
    {
        [DataMember] string access_token { get; set; }
        [DataMember] string livemode { get; set; }
        [DataMember] string refresh_token { get; set; }
        [DataMember] string token_type { get; set; }
        [DataMember] string stripe_publishable_key { get; set; }
        [DataMember] string stripe_user_id { get; set; }
        [DataMember] string scope { get; set; }
    }
}
