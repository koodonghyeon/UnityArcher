using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class cLoading : MonoBehaviour
{
    public static string nextScene;




   public Text _Text;

    public Slider slider;
    bool IsDone = false;
    float fTime = 0f;
    AsyncOperation async_operation;

    void Start()
    {
        StartCoroutine(StartLoad("SampleScene"));
        StartCoroutine(StartText());
    }

    void Update()
    {
        fTime += Time.deltaTime;
        slider.value = fTime;

        if (fTime >= 5)
        {
            async_operation.allowSceneActivation = true;
        }
    }
    IEnumerator StartText()
    {
     
        _Text.text = "숲으로 들어가는중.";
        yield return new WaitForSeconds(1.0f);
        _Text.text = "숲으로 들어가는중..";
        yield return new WaitForSeconds(1.0f);
        _Text.text = "숲으로 들어가는중...";
        yield return new WaitForSeconds(1.0f);
        _Text.text = "숲으로 들어가는중.";
        yield return new WaitForSeconds(1.0f);
        _Text.text = "숲으로 들어가는중..";
 

    }

    public IEnumerator StartLoad(string strSceneName)
    {
        async_operation = SceneManager.LoadSceneAsync(strSceneName);
        async_operation.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;

            while (async_operation.progress < 0.9f)
            {
                slider.value = async_operation.progress;

                yield return true;
            }
        }
    }
}


