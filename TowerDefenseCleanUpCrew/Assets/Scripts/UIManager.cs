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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        yield return new WaitForSeconds(breakTimes);
        text.enabled = false;
        yield return new WaitForSeconds(breakTimes);
        text.enabled = true;
        yield return new WaitForSeconds(breakTimes);
        text.enabled = false;
        yield return new WaitForSeconds(breakTimes);
        text.enabled = true;
        yield return new WaitForSeconds(breakTimes);
        text.enabled = false;


        yield return null;
    }
}
