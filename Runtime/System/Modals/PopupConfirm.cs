using UnityEngine;
using UnityEngine.UI;

namespace Uli.UI
{
    /// <summary>
    /// An basic popup which only has one button
    /// </summary>
    public class PopupConfirm : MonoBehaviour
    {
        public Uli.Transition.BaseTransition thisPopup;

        public TMPro.TextMeshProUGUI txtTitle;
        public TMPro.TextMeshProUGUI txtMessage;
        public TMPro.TextMeshProUGUI txtYes;

        public Button btYes;

        public virtual void ConfigurePopup(string title, string message, string yes, UnityEngine.Events.UnityAction yesAction)
        {
            txtTitle.text = title;
            txtMessage.text = message;
            txtYes.text = yes;

            btYes.gameObject.SetActive(true);
            btYes.onClick.AddListener(yesAction);
        }
        public virtual void HideChoices()
        {
            btYes.gameObject.SetActive(false);
        }
        public void OpenPopup()
        {
            thisPopup.DoTween(true);
        }
        public void ClosePopup()
        {
            ClearListeners();
            thisPopup.DoTween(false);
        }
        public virtual void ClearListeners()
        {
            //Limpa todos os listeners
            btYes.onClick.RemoveAllListeners();
        }
    }
}