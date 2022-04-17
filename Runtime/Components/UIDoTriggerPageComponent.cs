using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ULI_UTILS
namespace Uli.UI.Components
{
    public class UIDoTriggerPageComponent : MonoBehaviour
    {
        public string targetPage;
        public void TriggerGlobalPageEvent() 
        {
            Uli.Events.EventManager.Instance.TriggerEvent(new Uli.Events.TriggerChangePage(targetPage));
        }
    }
}
#endif
