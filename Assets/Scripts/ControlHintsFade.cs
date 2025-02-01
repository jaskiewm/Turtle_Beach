using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlHintsFade : MonoBehaviour
{
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        for (float alpha = 1f; alpha >= 0.1f; alpha -= 0.1f)
        {
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(0.5f);
        }
        canvasGroup.alpha = 0;
    }
}
