using UnityEngine;

internal class Player : MonoBehaviour
{
    private const float LimiterX = 2;

    [SerializeField] private float sideSpeed;
    [SerializeField] private GameObject tracePrefab;

    private Vector2 startTouch, finishTouch;
    private float distance;

    private void Update()
    {
        MoveSide();
        CheckFail();

        SpawnTrace();
    }

    private void MoveSide()
    {
        //if (Input.GetAxis("Horizontal") != 0 && !GameManager.Instance.IsStart)
        //{
        //    GameManager.Instance.IsStart = true;
        //    GameManager.Instance.IsGame = true;
        //}

        //if (GameManager.Instance.IsGame)
        //{
        //    if (Input.GetAxis("Horizontal") > 0 && )
        //        transform.Translate(Vector3.right * sideSpeed * Time.deltaTime);
        //    if (Input.GetAxis("Horizontal") < 0 && transform.localPosition.x > -LimiterX)
        //        transform.Translate(Vector3.left * sideSpeed * Time.deltaTime);
        //}

        if (Input.touchCount > 0)
        {
            if(!GameManager.Instance.IsStart)
            {
                GameManager.Instance.IsStart = true;
                GameManager.Instance.IsGame = true;
            }

            if (GameManager.Instance.IsGame)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                    case TouchPhase.Stationary:
                        startTouch = Camera.main.ScreenToViewportPoint(touch.position);
                        break;
                    case TouchPhase.Moved:
                        finishTouch = Camera.main.ScreenToViewportPoint(touch.position);
                        distance = (finishTouch - startTouch).x;

                        transform.Translate(Vector3.right * distance * sideSpeed * Time.deltaTime);
                        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -LimiterX, LimiterX), transform.localPosition.y, transform.localPosition.z);
                        break;
                }
            }
        }
    }

    private void CheckFail()
    {
        if (!GetComponentInChildren<Cube>() && GameManager.Instance.IsGame)
        {
            GameManager.Instance.IsGame = false;
        }
    }

    private void SpawnTrace()
    {
        if (GameManager.Instance.IsGame)
            Instantiate(tracePrefab, new Vector3(transform.position.x, 0.01f, transform.position.z), tracePrefab.transform.rotation);
    }
}
