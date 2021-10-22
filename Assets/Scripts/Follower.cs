using UnityEngine;
using PathCreation;

internal class Follower : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private float speed;

    private float distance;

    private void Update()
    {
        
            Follow();
    }

    private void Follow()
    {
        if (GameManager.Instance.IsGame)
        {
            distance += speed * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distance);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distance);
        }
    }
}
