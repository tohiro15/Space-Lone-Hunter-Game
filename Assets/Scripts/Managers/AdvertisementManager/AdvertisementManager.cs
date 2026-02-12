using UnityEngine;
using YG;

public class AdvertisementManager : MonoBehaviour
{
    static public AdvertisementManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else if(Instance == this)
        {
            Destroy(Instance);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void StartInterstitial()
    {
        YG2.InterstitialAdvShow();
    }
}
