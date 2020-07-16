using UnityEngine;
using UnityEngine.Advertisements;

public class cAdmobManager : MonoBehaviour
{
    private const string android_game_id = "3709785";
    private const string ios_game_id = "3709784";

    private const string rewarded_video_id = "rewardedVideo";

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
#elif UNITY_IOS
        Advertisement.Initialize(ios_game_id);
#endif
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewarded_video_id))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };

            Advertisement.Show(rewarded_video_id, options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    Debug.Log("The ad was successfully shown.");
                    cPlayerData.GetInstance._CurrnetHP = cPlayerData.GetInstance._MaxHP;
                     cPlayerData.GetInstance._PlayerDead = false;
                    cUIController.GetInstance._EndGame.SetActive(false);
                   cPlayer.GetInstance._Anim.SetTrigger("ReStart");
                    cUIController.GetInstance._JoystickGo.gameObject.SetActive(true);
                    cUIController.GetInstance._JoystickPaneGo.gameObject.SetActive(false);
                    Time.timeScale = 1f;
                    break;
                }
            //case ShowResult.Skipped:
            //    {
            //        Debug.Log("The ad was skipped before reaching the end.");

            //        // to do ...
            //        // 광고가 스킵되었을 때 처리

            //        break;
            //    }
           
        }
    }
}




