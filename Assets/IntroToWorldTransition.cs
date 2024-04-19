using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PuzzleManagement;


public class IntroToWorldTransition : MonoBehaviour
{
    public GameObject proc;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Completed());
    }

    // Update is called once per frame

    IEnumerator Completed() {
        yield return new WaitForSeconds(0.01f);
        AudioManager.Instance.CheckMusic();
        yield return new WaitForSeconds(130f);
        proc.GetComponent<PuzzleProc>().PuzzleInit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            proc.GetComponent<PuzzleProc>().PuzzleInit();
        }
    }
}
