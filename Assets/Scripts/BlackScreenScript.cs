using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackScreenScript : MonoBehaviour
{

    public Image blackSquare;
    
    // Start is called before the first frame update
    private void Start()
    {
        blackSquare = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeInOut(bool fadeToBlack = true, int fadeSpeed = 3)
    {

        var objectColor = blackSquare.color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackSquare.color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackSquare.color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackSquare.color = objectColor;
                yield return null;
            }
            
        }
        
        yield return new WaitForEndOfFrame();
    }
}
