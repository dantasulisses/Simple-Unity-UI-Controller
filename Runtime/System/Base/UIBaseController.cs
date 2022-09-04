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
    /// The base of every UIPage controller subtypes
    /// </summary>
    public abstract class UIBaseController : MonoBehaviour
    {
        [Title("The pages that respond to this controller")]
        public List<UIBasePage> controlledPages = new List<UIBasePage>();

#if ULI_UTILS
        [InfoBox("Determines if this PagesController will consume events of TriggerChangePage triggered by the EventManager")]
        [SerializeField] protected bool listenToGlobalEvents;
#endif

        protected void Start()
        {
            foreach (var page in controlledPages)
            {
                page.Initialize(this);
            }
        }
#if ULI_UTILS
        protected void OnEnable()
        {
            if (listenToGlobalEvents)
            {
                AddEventListeners();
            }
        }
        protected void OnDisable()
        {
            if (listenToGlobalEvents)
            {
                RemoveEventListeners();
            }
        }
        protected abstract void AddEventListeners();
        protected abstract void RemoveEventListeners();
#endif
        public abstract void GoToPreviousPage();
        public abstract void GoToPage(string page);
        
        public void AddPage(UIBasePage page) 
        {
            if(page == null || controlledPages.Contains(page))
                return;

            controlledPages.Add(page);
        }
        public void RemovePage(UIBasePage page) 
        {
            if (page == null || !controlledPages.Contains(page))
                return;

            controlledPages.Remove(page);
        }
    }
}