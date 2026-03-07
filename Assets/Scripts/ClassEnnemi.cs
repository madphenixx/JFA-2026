using UnityEngine;

public class ClassEnnemi : MonoBehaviour
{
    public int pv=10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pv == 0)
        {
            //On détruit l'objet, ajoute des points au score et acctionne l'animation de mort
        }
    }
}
