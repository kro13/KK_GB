using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Util
{
    public class AssetBundlesManager
    {
        private static int version = 0;
        private static AssetBundle bundle;

        private static int res;

        private static GameManager owner;

        public static void Init(GameManager gm)
        {
            owner = gm;
        }
      
        public static AssetBundle LoadAssetBundle(string bundleName)
        {
#if UNITY_EDITOR
            string assetsFolderURL = "file://"+Application.streamingAssetsPath+"/"+bundleName;
            //string assetsFolderURL = "https://commondatastorage.googleapis.com/itchio/html/211360/StreamingAssets"+"/"+bundleName;

#else
            string assetsFolderURL = Application.streamingAssetsPath+"/"+bundleName;
#endif
            IEnumerator e = DownloadAndCache(assetsFolderURL);
            //IEnumerator e = TestCoroutine();
            Coroutine coroutine = owner.StartCoroutine(e);
            while (e.MoveNext())
            {
            }

            return bundle;
        }

        private static IEnumerator TestCoroutine()
        {
            WaitForSeconds delay = new WaitForSeconds(1f);

            for (int x = 0; x < 5; x++)
            {
                res = x;
                yield return null;
            }
            yield return res;
        }

        private  static IEnumerator DownloadAndCache(string bundleUrl)
        {
            /*while (!Caching.ready)
            {
                yield return null;
            }
            Caching.CleanCache();*/
            using (WWW www = new WWW(bundleUrl))
            {
                
                if (www.error != null)
                {
                    throw new Exception("WWW download had an error:" + www.error);
                }
                bundle = www.assetBundle;
                yield return www;
            } 
            // memory is freed from the web stream (www.Dispose() gets called implicitly)
        }

    }
}