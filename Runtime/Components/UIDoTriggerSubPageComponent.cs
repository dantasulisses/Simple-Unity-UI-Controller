using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ULI_UTILS
namespace Uli.UI.Components
{
    public class UIDoTriggerSubPageComponent : MonoBehaviour
    {
        public string targetPage;
        public void DispatchEvent() 
        {
            Uli.Events.EventManager.Instance.DispatchEvent(new Uli.Events.TriggerChangeSubPage(targetPage));
        }
    }
}
#endif
