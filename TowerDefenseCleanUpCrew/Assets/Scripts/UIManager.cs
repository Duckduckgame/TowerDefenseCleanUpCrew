using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI siegeStart;
    [SerializeField]
    public TextMeshProUGUI cleanStart;
    [SerializeField]
    public TextMeshProUGUI timer;
    [SerializeField]
    public TextMeshProUGUI corpseCount;
    [SerializeField]
    public TextMeshProUGUI fixText;
    [SerializeField]
    public TextMeshProUGUI victory;
    [SerializeField]
    public TextMeshProUGUI defeat;
    public GameObject startCleanPanel;


    public IEnumerator ShowText(TextMeshProUGUI text, float time)
    {
        text.enabled = true;
        yield return new WaitForSeconds(time);
        text.enabled = false;
        yield return null;
    }
    public IEnumerator FlashText(TextMeshProUGUI text, float breakTimes)
    {
        text.enabled = true;
        yield return new WaitForSecondsRealtime(breakTimes);
        text.enabled = false;
        yield return new WaitForSecondsRealtime(breakTimes);
        text.enabled = true;
        yield return new WaitForSecondsRealtime(breakTimes);
        text.enabled = false;
        yield return new WaitForSecondsRealtime(breakTimes);
        text.enabled = true;
        yield return new WaitForSecondsRealtime(breakTimes);
        text.enabled = false;


        yield return null;
    }

    public IEnumerator ShowPanel(GameObject panel, float time)
    {
        panel.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        panel.SetActive(false);
        yield return null;
    }
}
