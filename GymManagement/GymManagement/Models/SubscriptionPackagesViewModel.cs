using System;
using System.Collections.Generic;

namespace GymManagement.Models
{
    public class SubscriptionPackagesViewModel
    {
        public Subscription Subscription { get; set; }

        public List<Package> Packages { get; set; }

        public Package Package { get; set; }

        public DateTime Date { get; set; }
    }
}