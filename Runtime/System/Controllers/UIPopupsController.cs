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
    /// Controls the flow of popups, which are stackable by default
    /// </summary>
    public class UIPopupsController : UIBaseController
    {
        /// <summary>
        /// Holds the list of current popup stack
        /// </summary>
        private Stack<string> popupStack = new Stack<string>();

#if ULI_UTILS
        protected override void AddEventListeners()
        {
            Uli.Events.EventManager.Instance.AddListener<Events.TriggerOpenPopup>(OnTriggerOpenPopup);
            Uli.Events.EventManager.Instance.AddListener<Events.TriggerClosePopup>(OnTriggerClosePopup);
        }

        protected override void RemoveEventListeners()
        {
            Uli.Events.EventManager.Instance?.RemoveListener<Events.TriggerOpenPopup>(OnTriggerOpenPopup);
            Uli.Events.EventManager.Instance?.RemoveListener<Events.TriggerClosePopup>(OnTriggerClosePopup);
        }
        
        private void OnTriggerOpenPopup(Uli.Events.TriggerOpenPopup e)
        {
            if (e.closeCurrentOpenPopup)
                RemoveLatestPopupFromStack();
            GoToPage(e.targetPopup);
        }
        
        private void OnTriggerClosePopup(Uli.Events.TriggerClosePopup e)
        {
            if (e.closeAllPopups)
                CloseAllPopups();
            else
                ClosePopup();
        }
#endif
        public override void GoToPreviousPage()
        {
            ClosePopup();
        }

        public void CloseAllPopups()
        {
            popupStack.Clear();
            UpdatePages();
        }

        public void ClosePopup()
        {
            //The close popup function always close the current 
            RemoveLatestPopupFromStack();
            UpdatePages();
        }

        public override void GoToPage(string page)
        {
            popupStack.Push(page);
            UpdatePages();
        }

        private void UpdatePages()
        {
            for (int x = 0; x < controlledPages.Count; x++)
            {
                controlledPages[x].UpdatePageState(GetCurrentStackEntry());
            }
        }

        private void RemoveLatestPopupFromStack()
        {
            if (popupStack.Count > 0)
                popupStack.Pop();
        }

        private string GetCurrentStackEntry()
        {
            if (popupStack.Count > 0)
                return popupStack.Peek();

            return string.Empty;
        }
    }
}