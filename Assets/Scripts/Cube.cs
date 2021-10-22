using UnityEngine;

internal class Cube : MonoBehaviour
{
    private const float ShiftY = 1f;

    private bool isPlayer = false;

    private void Start()
    {
        isPlayer = CheckParentPlayer(transform);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGame)
        {
            CheckPositionY();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CheckParentPlayer(collision.transform) && !isPlayer)
        {
            SetParent(collision.transform);
            isPlayer = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle") && isPlayer)
        {
            if (Mathf.Abs(other.transform.position.y - transform.position.y) < 0.9f)
                transform.parent = null;
        }

        if (other.CompareTag("finish"))
            GameManager.Instance.IsGame = false;
    }

    private bool CheckParentPlayer(Transform t)
    {
        return t.parent && t.parent.CompareTag("Player");
    }

    private void SetParent(Transform t)
    {
        Vector3 parentPosition = t.parent.position;
        parentPosition.y += ShiftY;
        t.parent.position = parentPosition;

        transform.parent = t.parent;
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
    }

    private void CheckPositionY()
    {
        if (transform.position.y <= 0) Destroy(gameObject);
    }
}
