using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
//Do some alias to avoid breaking the inspector
using InfoBox = UnityEngine.TooltipAttribute;
#endif

namespace Uli.UI
{
    public class UIPage : MonoBehaviour
    {
        [InfoBox("On which screens states this page is displayed")]
        public List<string> showOn;

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
        [InfoBox("Selectes this object through UnityEventSystem when this page gets active")]
        [SerializeField] private GameObject desiredSelectedObject;

        [SerializeField] protected bool isPageEnabled;
        protected UIPagesController controller;

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
        public void Initialize(UIPagesController myController) 
        {
            controller = myController;
        }
        public virtual void SetPageEnableStatus(bool doEnable) 
        {
            if (isPageEnabled && !doEnable)
                onExitPage.Invoke();
            if (!isPageEnabled && doEnable)
                onEnterPage.Invoke();

            isPageEnabled = doEnable;
            for (int x = 0; x < transitions.Length; x++)
            {
                transitions[x].DoTween(doEnable);
            }
        }
        public void GoToPage(string destination) 
        {
            controller.GoToPage(destination);
        }
        public void GoToPreviousPage() 
        {
            controller.GoToPreviousPage();
        }
        public void UpdatePageState(string currentPage)
        {
            bool doShow = showOn.Contains(currentPage);

            SetPageEnableStatus(doShow);

            if (doShow && selectAnObjectOnEnablePage)
            {
                UnityEngine.EventSystems.EventSystem.current?.SetSelectedGameObject(desiredSelectedObject);
            }
        }
    }
}