using UnityEngine.UI;

namespace Uli.UI
{
    /// <summary>
    /// Popup with an Yes/No Choice for the player
    /// </summary>
    public class PopupChoice : PopupConfirm
    {
        public TMPro.TextMeshProUGUI txtNo;
        
        public Button btNo;

        public virtual void ConfigurePopup(string title, string message, string yes, string no, UnityEngine.Events.UnityAction yesAction, UnityEngine.Events.UnityAction noAction)
        {
            base.ConfigurePopup(title, message, yes, yesAction);
            
            txtNo.text = no;
            btNo.gameObject.SetActive(true);
            btNo.onClick.AddListener(noAction);
        }
        public override void ClearListeners()
        {
            base.ClearListeners();

            btNo.onClick.RemoveAllListeners();
        }
        public override void HideChoices()
        {
            base.HideChoices();

            btNo.gameObject.SetActive(false);
        }
    }
}