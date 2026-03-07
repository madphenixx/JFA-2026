using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProjectilePlayer : MonoBehaviour
{
    public GameObject cible;
    public Rigidbody2D rb;
    public Vector2 launchDir;
    public Vector2 launchDirNorm;
    public float speed=10;
    public GameObject[] allEnnemies;
    public float distanceMin=1000;


    void Awake()
    {
        allEnnemies = GameObject.FindGameObjectsWithTag("Ennemi");
        foreach (GameObject ennemi in allEnnemies)
        {
            float distance = Vector2.Distance(transform.position, ennemi.transform.position);
            if (distance < distanceMin)
            {
                cible = ennemi;
                distanceMin = distance;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        launchDir = cible.transform.position - gameObject.transform.position;
        launchDirNorm = launchDir.normalized;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            Slider slEnnemi= collision.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Slider>();
            collision.gameObject.GetComponent<ClassEnnemi>().pv=collision.gameObject.GetComponent<ClassEnnemi>().pv-1;
            slEnnemi.value=collision.gameObject.GetComponent<ClassEnnemi>().pv;
            GameManager.combo=GameManager.combo+1;
            GameManager.comboText.text="Combo: "+ GameManager.combo.ToString();

            //A modifier et équilibrer (multiplicateur de combo)}
            GameManager.score=GameManager.score+10*(1+(GameManager.combo/5));
            GameManager.scoreText.text="Score: "+ GameManager.score.ToString();

            Destroy(gameObject);
        }
    }


    void FixedUpdate()
    {
        rb.linearVelocity = launchDirNorm * speed;
    }
    
}
