using UnityEngine;

internal class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    internal static GameManager Instance { get; private set; }

    internal bool IsGame { get; set; } = false;
    internal bool IsStart { get; set; } = false;
    internal bool IsWin { get; private set; } = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!IsGame && IsStart)
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        Cube[] cubes = player.GetComponentsInChildren<Cube>();
        int cubesCount = cubes.Length;

        IsWin = cubesCount > 0;
        UIManager.Instance.ShowPanel(IsWin, cubesCount);
    }
}
