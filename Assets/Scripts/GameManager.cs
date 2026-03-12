using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Ne pas oublier d'équilibrer et de mettre à jour la valeur max du slider
    public static float pv;
    public static Slider pvSlider;
    public static int score;
    public static Text scoreText;
    public static int combo;
    public static Text comboText;
    public static Transform scoreTr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        pvSlider=GameObject.Find("PVPlayer").GetComponent<Slider>();
        scoreText=GameObject.Find("Score").GetComponent<Text>();
        comboText=GameObject.Find("Combo").GetComponent<Text>();

        scoreTr = GameObject.Find("Score").transform;

        pv=10;
        score=0;
        combo=0;
    }

    // Update is called once per frame
    void Update()
    {
        if (pv == 0)
        {
            // Deathscreen et on peut reload la scene à laquelle on était avec le bouton retry
            // Garder le même GameManager pendant toute la partie ??? -> Dontdestroyonload
            //Faire une variable currentScene pour pouvoir sauvegarder la dernière scène
        }
    }

    public void ShowScoreAdd(float value, bool isPositive)
    {
        foreach (Transform child in scoreTr)
        {
            Text text = child.GetComponent<Text>();
            if (child.gameObject.activeSelf==false)
            {
                if (isPositive)
                {
                    text.text = "+" + value.ToString();
                }
                else
                {
                    text.text = "-" + value.ToString();
                }
                text.gameObject.SetActive(true);
                StartCoroutine(ScoreTime(text));
               break; 
            }
        }
    }

    public IEnumerator ScoreTime(Text text)
    {
        yield return new WaitForSeconds(1);
        text.gameObject.SetActive(false);
    }
}
