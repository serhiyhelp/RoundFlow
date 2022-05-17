using UnityEngine;
using Random = UnityEngine.Random;

public class Section : MonoBehaviour
{
    public float speed = 5f;

    [SerializeField] private GameObject[] walls;
    [SerializeField] private GameObject[] coins;

    [Space]
    [SerializeField] private int maxWalls = 4;
    [SerializeField] private int maxCoins = 2;

    private void OnEnable()
    {
        foreach (var item in walls) item.SetActive(false);
        foreach (var item in coins) item.SetActive(false);

        for (var i = 0; i < maxWalls; i++)
        {
            var index = Random.Range(0, 8);
            walls[index].SetActive(true);
        }

        for (var i = 0; i < maxCoins; i++)
        {
            var index = Random.Range(0, 8);
            if (walls[index].activeSelf == false)
            {
                coins[index].SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (transform.position.z < -2)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * speed * Time.fixedDeltaTime);
    }
}
