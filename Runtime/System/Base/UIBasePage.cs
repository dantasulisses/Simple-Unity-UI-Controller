using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
//Do some alias to avoid breaking the inspector
using InfoBox = UnityEngine.TooltipAttribute;
#endif

namespace Uli.UI
{
    public abstract class UIBasePage : MonoBehaviour
    {
        [InfoBox("On which screens states this page is displayed")]
        public List<string> showOn;

        [SerializeField] protected bool isPageEnabled = false;
        protected UIBaseController controller;

        public bool IsPageEnabled() => isPageEnabled;
        public void Initialize(UIBaseController myController) 
        {
            controller = myController;
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
        }
        private void SetPageEnableStatus(bool doEnable) 
        {
            if (isPageEnabled && !doEnable)
            {
                DoPageClose();
            }
            else if (!isPageEnabled && doEnable)
            {
                DoPageOpen();
            }
        }
        public abstract void DoPageOpen();
        public abstract void DoPageClose();
    }
}