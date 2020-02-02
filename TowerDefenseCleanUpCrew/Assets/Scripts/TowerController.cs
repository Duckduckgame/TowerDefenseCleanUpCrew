using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{

    FixingHandler fixingHandler;
    [SerializeField]
    Sprite[] damageSprites;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        fixingHandler = GetComponent<FixingHandler>();
    }

    // Update is called once per frame
    void Update()
    {

        if (fixingHandler.life <= 0)
            spriteRenderer.sprite = damageSprites[4];
        if (fixingHandler.life > 200)
            spriteRenderer.sprite = damageSprites[3];
        if (fixingHandler.life > 400)
            spriteRenderer.sprite = damageSprites[2];
        if (fixingHandler.life > 600)
            spriteRenderer.sprite = damageSprites[1];
        if (fixingHandler.life > 900)
            spriteRenderer.sprite = damageSprites[0];

    }

    void ChangeSprites()
    {

    }
}
