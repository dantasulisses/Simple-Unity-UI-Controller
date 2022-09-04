using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
//Do some alias to avoid breaking the inspector
using InfoBox = UnityEngine.TooltipAttribute;
#endif

namespace Uli.UI
{
    public class UIPage : UIBasePage
    {
        [InfoBox("Transition behavior(s) that this page does when displayed/disappeared")]
        [SerializeField] private Transition.BaseTransition[] transitions;

        [InfoBox("Actions called when the page was disabled and then gets enabled")]
        [SerializeField] private UnityEvent onEnterPage;

        [InfoBox("Actions called when the page was enabled and then gets disabled")]
        [SerializeField] private UnityEvent onExitPage;

        [SerializeField] private bool selectAnObjectOnEnablePage = false;
#if ODIN_INSPECTOR
        [ShowIf(nameof(selectAnObjectOnEnablePage))]
#endif
        [InfoBox("Selects this object through UnityEventSystem when this page gets active")]
        [SerializeField] private GameObject desiredSelectedObject;

        #region INSPECTOR
#if ODIN_INSPECTOR
        [Button]
        private void SetPageOn() 
        {
            SetPageStateDirectly(true);
        }
        [Button]
        private void SetPageOff() 
        {
            SetPageStateDirectly(false);
        }
        private void SetPageStateDirectly(bool state) 
        {
            isPageEnabled = state;
            foreach (var transition in transitions)
            {
                transition.SetState(state);
            }
        }
#endif
        #endregion INSPECTOR

        public override void DoPageOpen()
        {
            isPageEnabled = true;
            onEnterPage.Invoke();
            
            ExecuteTransitions();
            
            if (selectAnObjectOnEnablePage && EventSystem.current != null)
            {
                EventSystem.current.SetSelectedGameObject(desiredSelectedObject);
            }
        }

        public override void DoPageClose()
        {
            isPageEnabled = false;
            onExitPage.Invoke();

            ExecuteTransitions();
        }

        private void ExecuteTransitions()
        {
            for (int x = 0; x < transitions.Length; x++)
            {
                transitions[x].DoTween(isPageEnabled);
            }
        }
    }
}