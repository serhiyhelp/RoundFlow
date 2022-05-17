using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    public float period = 0.7f;
    public float deviation = 0.3f;

    [Space]
    public GameObject section;

    private float time;
    private float currentPeriod;

    private readonly List<GameObject> pool = new(12);

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= currentPeriod)
        {
            time = 0;
            currentPeriod = Random.Range(period - deviation, period + deviation);
            
            var angle = Random.value * 360f;
            
            var newSection = GetSection();
            newSection.transform.Rotate(0, 0, angle);
        }
    }

    private GameObject GetSection()
    {
        var candidate = pool.FirstOrDefault(x => !x.activeSelf);
        if (candidate is null)
        {
            candidate = Instantiate(section, transform);
            pool.Add(candidate);
        }
        else
        {
            print("reuse");
            candidate.SetActive(true);
            candidate.transform.localPosition = Vector3.zero;
        }

        return candidate;

    }
}
