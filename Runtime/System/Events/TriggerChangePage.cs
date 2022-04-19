using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ULI_UTILS
namespace Uli.Events
{
    public class TriggerChangePage : Uli.Events.GameEvent
    {
        public string pageState;
        public TriggerChangePage(string targetPage) 
        {
            this.pageState = targetPage;
        }
    }
}
#endif