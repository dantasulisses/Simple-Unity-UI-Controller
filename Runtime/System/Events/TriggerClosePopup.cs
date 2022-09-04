using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ULI_UTILS
namespace Uli.Events
{
    public class TriggerClosePopup : Uli.Events.GameEvent
    {
        public bool closeAllPopups;
        public TriggerClosePopup(bool closeAllPopups = false) 
        {
            this.closeAllPopups = closeAllPopups;
        }
    }
}
#endif