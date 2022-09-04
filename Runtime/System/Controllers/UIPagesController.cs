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
    /// Controls page flows based on "groups" (pages)
    /// </summary>
    public class UIPagesController : UIBaseController
    {
        [InfoBox("Stores a reference of the current open page")]
        [SerializeField] private string currentPage;
        [InfoBox("Stores a reference of the last opened page, in case you want to go back")]
        [SerializeField] private string wasOnPage = string.Empty;

#if ULI_UTILS
        protected override void AddEventListeners()
        {
            Uli.Events.EventManager.Instance.AddListener<Events.TriggerChangePage>(OnTriggerChangePage);
            Uli.Events.EventManager.Instance.AddListener<Events.TriggerGoToPreviousPage>(OnTriggerGoToPreviousPage);
        }

        protected override void RemoveEventListeners()
        {
            Uli.Events.EventManager.Instance?.RemoveListener<Events.TriggerChangePage>(OnTriggerChangePage);
            Uli.Events.EventManager.Instance?.RemoveListener<Events.TriggerGoToPreviousPage>(OnTriggerGoToPreviousPage);
        }
        
        private void OnTriggerChangePage(Uli.Events.TriggerChangePage e) 
        {
            GoToPage(e.pageState);
        }
        
        private void OnTriggerGoToPreviousPage(Uli.Events.TriggerGoToPreviousPage e)
        {
            GoToPreviousPage();
        }
#endif
        public override void GoToPreviousPage() 
        {
            GoToPage(wasOnPage);
        }
        
        public override void GoToPage(string page)
        {
            wasOnPage = currentPage;
            currentPage = page;
            for (int x = 0; x < controlledPages.Count; x++)
            {
                controlledPages[x].UpdatePageState(page);
            }
        }
    }
}