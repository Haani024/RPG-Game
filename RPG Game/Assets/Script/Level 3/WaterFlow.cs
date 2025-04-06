using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public bool hasFlowedThroughCorrectPath = false;
    public GameObject waterSource;
    public GameObject waterDestination;

    private void Update()
    {
        if (hasFlowedThroughCorrectPath && Vector3.Distance(waterSource.transform.position, waterDestination.transform.position) < 1f)
        {
            Debug.Log("Water reached destination!");
        }
    }
}
