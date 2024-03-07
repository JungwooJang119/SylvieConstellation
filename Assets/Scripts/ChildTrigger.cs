using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**Check when Syvlie enters trigger area and send to triggerManager which child was entered**/
    private void OnTriggerEnter2D(Collider2D other) {
        if (other == player.GetComponent<CapsuleCollider2D>()) {
            gameObject.GetComponentInParent<TriggerManager>().whichChild(this.gameObject);
        }
    }
}
