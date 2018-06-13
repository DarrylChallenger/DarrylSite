using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using DarrylSite.Models;

namespace DarrylSite.Classes
{
    public static class GlobalConstants
    {
        public static StripeConfig StripeConfig = new StripeConfig();
        public static HttpClient httpStripeClient = new HttpClient();

        public static PayPalConfig PayPalConfig = new PayPalConfig();
        public static HttpClient httpPayPalClient = new HttpClient();
    }
}