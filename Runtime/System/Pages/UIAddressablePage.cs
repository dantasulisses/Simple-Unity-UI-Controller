#if PACKAGE_ADDRESSABLES
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#else
//Do some alias to avoid breaking the inspector
using InfoBox = UnityEngine.TooltipAttribute;
#endif

namespace Uli.UI
{
    [TypeInfoBox("This UIPage should be used pointing to a 'UIPage' addressable, so the page can be dinamically loaded and still work in conjunction with the UIController")]
    public class UIAddressablePage : UIBasePage
    {
        private const float DELAY_TO_RELEASE = 1;
        
        public AssetReference assetReference;
        private GameObject spawnedPage;
        
        public override void DoPageOpen()
        {
            if (spawnedPage != null)
                return;
            
            isPageEnabled = true;
            LoadPageAsync();
        }

        private async void LoadPageAsync()
        {
            spawnedPage = await assetReference.InstantiateAsync(Vector3.zero, Quaternion.identity, transform).Task;
            var uiPage = spawnedPage.GetComponent<UIPage>();
            uiPage.Initialize(controller);
            
            //We directly resize to make a stretch all on the spawned page
            var pageRect = uiPage.GetComponent<RectTransform>();
            pageRect.localScale = Vector3.one;
            pageRect.anchoredPosition = new Vector3(0, 0, 0);
            pageRect.anchorMin = new Vector2(0, 0);
            pageRect.anchorMax = new Vector2(1, 1);
            
            uiPage.DoPageOpen();
        }

        public override void DoPageClose()
        {
            if (spawnedPage == null)
                return;

            isPageEnabled = false;
            UnloadPageAsync();
        }

        private async void UnloadPageAsync()
        {
            spawnedPage.GetComponent<UIPage>().DoPageClose();
            //Wait some seconds to guarantee all transitions being done
            await Task.Delay(Mathf.RoundToInt(DELAY_TO_RELEASE * 1000));
            assetReference.ReleaseInstance(spawnedPage);
        }
    }
}
#endif