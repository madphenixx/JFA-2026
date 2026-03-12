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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        pvSlider=GameObject.Find("PVPlayer").GetComponent<Slider>();
        scoreText=GameObject.Find("Score").GetComponent<Text>();
        comboText=GameObject.Find("Combo").GetComponent<Text>();
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
}
