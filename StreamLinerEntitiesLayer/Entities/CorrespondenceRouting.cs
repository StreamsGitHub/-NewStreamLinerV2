using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerEntitiesLayer.Entities
{
    public class CorrespondenceRouting
    {
        public StatusCorrespondence status { get; set; }
        public StatusCorrespondence statusTran { get; set; }
        public bool isGenrator { get; set; }
    }

    public enum StatusCorrespondence
    {
        Accept = 1,
        Reject = 2,
        Reassigne = 3,
        Complete = 4,
        Routed = 5,
        Recall = 6
    }

    public enum PriortyCorrespondence
    {
        Urgent = 1,
        High = 2,
        Low = 3,

    }
    public enum Notify
    {
        seen = 1,
        unseen = 2,

    }
    public enum OutboundStatus
    {
        Internal = 1,
        External = 2,
    }
}
