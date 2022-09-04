using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ULI_UTILS
namespace Uli.Events
{
    public class TriggerOpenPopup : Uli.Events.GameEvent
    {
        public string targetPopup;
        public bool closeCurrentOpenPopup;
        public TriggerOpenPopup(string targetPopup, bool closeCurrentOpenPopup = false) 
        {
            this.targetPopup = targetPopup;
            this.closeCurrentOpenPopup = closeCurrentOpenPopup;
        }
    }
}
#endif