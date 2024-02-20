using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksterStarController : MonoBehaviour
{

    private Color off; //blue
    private Color on; //red
    public GameObject[] children;
    public GameObject[] connectedStars;
    public GameObject child1;
    public GameObject child2;
    private SpriteRenderer spriteRenderer1;
    private SpriteRenderer spriteRenderer2;


    void Start()
    {
        
        off = Color.blue;
        on = Color.red;

        foreach (GameObject connected in connectedStars) {
            child1 = connected.transform.GetChild(0).gameObject;
            child2 = connected.transform.GetChild(1).gameObject;

            spriteRenderer1 = child1.GetComponent<SpriteRenderer>();
            spriteRenderer2 = child2.GetComponent<SpriteRenderer>();

            spriteRenderer1.color = off;
            spriteRenderer2.color = off;
            
        }


    }

    public void clicked() {        
        print("clicked");
        ToggleConnectedStars();
    }

    

     private void ToggleConnectedStars() {
         foreach (GameObject connected in connectedStars) {
            child1 = connected.transform.GetChild(0).gameObject;
            child2 = connected.transform.GetChild(1).gameObject;

            spriteRenderer1 = child1.GetComponent<SpriteRenderer>();
            spriteRenderer2 = child2.GetComponent<SpriteRenderer>();

            ToggleColor(spriteRenderer1);
            ToggleColor(spriteRenderer2);
            
        }
     }


     private void ToggleColor(SpriteRenderer spriteRenderer) {
         if (spriteRenderer.color == off) {
             spriteRenderer.color = on;
         } else {
            spriteRenderer.color = off;
        }
     }


    
    

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
