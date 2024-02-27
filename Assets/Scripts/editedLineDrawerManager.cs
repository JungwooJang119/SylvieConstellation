using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EditedLineDrawerManager : MonoBehaviour {
    private Vector3 startPosition;
    private Vector3 endPosition;
    private LineRenderer lineRenderer;
    private int numLines;
    [SerializeField] private CinemachineVirtualCamera endCamera;
    [SerializeField] private SpriteRenderer BGStarsClose;
    [SerializeField] private SpriteRenderer BGStarsFar;
    [SerializeField] private float time;

    private const float 
        FINISHED = 4.7f;
    // Start is called before the first frame update
    void Awake() {
        endCamera = FindObjectOfType<CinemachineVirtualCamera>();
        BGStarsClose = Camera.main.transform.GetChild(1).GetComponent<SpriteRenderer>();
        BGStarsFar = Camera.main.transform.GetChild(2).GetComponent<SpriteRenderer>();
    }

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        numLines = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void journeyDrawAction(List<Vector3> journey) {
        StartCoroutine(journeyDraw(journey));
    }

    private IEnumerator journeyDraw(List<Vector3> journey) {
        BGStarsClose.transform.localScale = new Vector3(100, 100, 0);
        BGStarsFar.transform.localScale = new Vector3(100, 100, 0);
        endCamera.m_Lens.OrthographicSize = 50;



        yield return new WaitForSeconds(2f);
        for (int i = 0; (i+1) < journey.Count; i++) {
            float t = 0;
            startPosition = journey[i];
            endPosition = journey[i+1];
            numLines++;
            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(numLines - 2, new Vector3(startPosition.x, startPosition.y, 0f));
            Vector3 newpos;
            for (; t < time; t += Time.deltaTime) {
                newpos = Vector3.Lerp(startPosition, endPosition, t/time);
                lineRenderer.SetPosition(numLines - 1, newpos);
                yield return null;
            }
            lineRenderer.SetPosition(numLines - 1, new Vector3(endPosition.x, endPosition.y, 0f));
            lineRenderer.endColor = Color.white;
            yield return new WaitForSeconds(.5f);
        }

        yield return null;
    }
}
