using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float clickDelay = 0.3f;
    private float doubleClickTime = 0;
    private int index = 0;
    private bool oneClick = false;
    private bool paused = false;

    [SerializeField]
    private Object json_ball_path;
    private Vector3[] ballPath;
    private TrailRenderer trailRenderer;

    void Start()
    {
        BallCoords ballCoords = JsonUtility.FromJson<BallCoords>(File.ReadAllText("Assets/Resources/" + json_ball_path.name + ".json"));
        ballPath = ballCoords.GetBallPath();
        transform.position = ballPath[0];

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.Clear();
    }

    void Update()
    {

        if (Input.GetMouseButtonUp(0) && Manager.currentBall == gameObject && !Manager.mouseOverUI)
        {

            if (!oneClick)
            {
                oneClick = true;

                doubleClickTime = Time.time;

                DetectClick();

            }
            else
            {
                oneClick = false;

                DetectClick();
            }
        }
        if (oneClick)
        {
            if ((Time.time - doubleClickTime) > clickDelay)
            {

                oneClick = false;

            }
        }
    }

    private void DetectClick()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Ball")
            {
                if (paused)
                {
                    paused = false;
                }
                if (oneClick && ((transform.position == ballPath[0]) || (transform.position == ballPath[ballPath.Length - 1])))
                {
                    StartCoroutine(MoveBall(ballPath));
                }
                if (!oneClick)
                {
                    StopAllCoroutines();
                    transform.position = ballPath[0];

                    trailRenderer.Clear();
                }
            }
        }

    }

    private IEnumerator MoveBall(Vector3[] path)
    {
        paused = false;

        if (Manager.sliderValue > 0)
        {
            transform.position = ballPath[index];
            trailRenderer.Clear();

            for (int i = 0; i < path.Length; i++)
            {
                while (paused)
                {
                    yield return null;
                }

                Vector3 startPos = transform.position;
                float timer = 0f;
                while (timer <= 1f)
                {
                    timer += Time.deltaTime * speed * Manager.sliderValue;
                    Vector3 newPos = Vector3.Lerp(startPos, path[i], timer);
                    transform.position = newPos;

                    yield return new WaitForEndOfFrame();
                }

                transform.position = path[i];

                startPos = path[i];
            }
        }

        yield return false;
    }

    public void PauseMovement()
    {
        paused = true;
    }
}