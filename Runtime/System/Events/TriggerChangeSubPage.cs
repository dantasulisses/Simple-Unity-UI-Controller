using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ULI_UTILS
namespace Uli.Events
{
    public class TriggerChangeSubPage : Uli.Events.GameEvent
    {
        public string subPageState;
        public TriggerChangeSubPage(string targetSubPage) 
        {
            this.subPageState = targetSubPage;
        }
    }
}
#endif