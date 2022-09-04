using UnityEngine;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
//Do some alias to avoid breaking the inspector
using InfoBox = UnityEngine.TooltipAttribute;
using Title = UnityEngine.HeaderAttribute;
#endif

namespace Uli.UI
{
    /// <summary>
    /// Controls page flows inside another UIPage (ex: Tabs inside another menu)
    /// </summary>
    public class UISubPagesController : UIBaseController
    {
        [InfoBox("The page which is owner of these subpages")]
        [SerializeField] private UIBasePage ownerPage;
        
        [InfoBox("Determines which is the default page that this subpage groups defaults to")]
        [SerializeField] private string defaultPage;
        [InfoBox("Stores a reference of the current open sub page")]
        [SerializeField] private string currentSubPage;
        [InfoBox("Stores a reference of the last opened sub page, in case you want to go back")]
        [SerializeField] private string wasOnSubPage = string.Empty;

#if ULI_UTILS
        protected override void AddEventListeners()
        {
            Uli.Events.EventManager.Instance.AddListener<Events.TriggerChangeSubPage>(OnTriggerChangeSubPage);
        }

        protected override void RemoveEventListeners()
        {
            Uli.Events.EventManager.Instance?.RemoveListener<Events.TriggerChangeSubPage>(OnTriggerChangeSubPage);
        }
        
        private void OnTriggerChangeSubPage(Uli.Events.TriggerChangeSubPage e) 
        {
            GoToPage(e.subPageState);
        }
#endif
        public void OpenDefaultPage()
        {
            GoToPage(defaultPage);
        }

        public override void GoToPreviousPage() 
        {
            GoToPage(wasOnSubPage);
        }
        
        public override void GoToPage(string subpage)
        {
            if(!ownerPage.isPageEnabled)
                return;
            
            wasOnSubPage = currentSubPage;
            currentSubPage = subpage;
            for (int x = 0; x < controlledPages.Count; x++)
            {
                controlledPages[x].UpdatePageState(subpage);
            }
        }
    }
}