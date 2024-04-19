using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkBottleCollectible : MonoBehaviour
{

    public GameObject Drunk;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Drunk.GetComponent<DrunkGoggles>().SetDrunkIntensity(Drunk.GetComponent<DrunkGoggles>().GetDrunkIntensity() + 1);
            Destroy(this.gameObject);
        }
    }
}
