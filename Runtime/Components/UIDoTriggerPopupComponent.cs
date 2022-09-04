using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
//Do some alias to avoid breaking the inspector
using InfoBox = UnityEngine.TooltipAttribute;
using ShowIf = UnityEngine.TooltipAttribute;
using HideIf = UnityEngine.TooltipAttribute;
#endif

#if ULI_UTILS
namespace Uli.UI.Components
{
    public class UIDoTriggerPopupComponent : MonoBehaviour
    {
        [InfoBox("Determines if this component will open or close some popup")]
        public bool openPopup = true;
        [ShowIf(nameof(openPopup))]
        public string targetPopup;
        [ShowIf(nameof(openPopup))]
        public bool alsoCloseCurrentOpenPopup = false;

        [HideIf(nameof(openPopup))]
        public bool closeAllPopups = false;
        
        public void DispatchEvent() 
        {
            if(openPopup)
                Uli.Events.EventManager.Instance.DispatchEvent(new Uli.Events.TriggerOpenPopup(targetPopup, alsoCloseCurrentOpenPopup));
            else
                Uli.Events.EventManager.Instance.DispatchEvent(new Uli.Events.TriggerClosePopup(closeAllPopups));
        }
    }
}
#endif
