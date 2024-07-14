using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyvliePosFix : MonoBehaviour
{
    [SerializeField] private GameObject startingPos;
    [SerializeField] private GameObject syvlie;
    // Start is called before the first frame update
    void Start()
    {
        syvlie.transform.position = startingPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
