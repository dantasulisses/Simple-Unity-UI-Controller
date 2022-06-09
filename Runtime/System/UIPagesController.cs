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
    public class UIPagesController : MonoBehaviour
    {
        [Title("The pages that respond to this controller")]
        public List<UIPage> controlledPages = new List<UIPage>();

#if ULI_UTILS
        [InfoBox("Determines if this PagesController will consume events of TriggerChangePage triggered by the EventManager")]
        [SerializeField] private bool listenToGlobalEvents;
#endif
        [InfoBox("Stores a reference of the current open page")]
        [SerializeField] private string currentPage;
        [InfoBox("Stores a reference of the last opened page, in case you want to go back")]
        [SerializeField] private string wasOnPage = string.Empty;

        private void Start()
        {
            foreach (var page in controlledPages)
            {
                page.Initialize(this);
            }
            //Starts the UI with the first state
            GoToPage(currentPage);
        }
#if ULI_UTILS
        private void OnEnable()
        {
            if(listenToGlobalEvents)
                Uli.Events.EventManager.Instance.AddListener<Events.TriggerChangePage>(OnTriggerChangePage);
        }
        private void OnDisable()
        {
            if (listenToGlobalEvents)
                Uli.Events.EventManager.Instance?.RemoveListener<Events.TriggerChangePage>(OnTriggerChangePage);
        }
        private void OnTriggerChangePage(Uli.Events.TriggerChangePage e) 
        {
            GoToPage(e.pageState);
        }
#endif
        public void GoToPreviousPage() 
        {
            GoToPage(wasOnPage);
        }
        public void GoToPage(string page)
        {
            wasOnPage = currentPage;
            currentPage = page;
            for (int x = 0; x < controlledPages.Count; x++)
            {
                controlledPages[x].UpdatePageState(page);
            }
        }
        public void AddPage(UIPage page) 
        {
            if(page == null || controlledPages.Contains(page))
                return;

            controlledPages.Add(page);
        }
        public void RemovePage(UIPage page) 
        {
            if (page == null || !controlledPages.Contains(page))
                return;

            controlledPages.Remove(page);
        }
    }
}