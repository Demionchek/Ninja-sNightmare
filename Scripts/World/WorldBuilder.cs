using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private WorldController _worldController;
    [SerializeField] private GameObject[] ObstaclePlatforms;
    [SerializeField] private GameObject[] EmptyPlatforms;
    [SerializeField] private Transform platformContainer;
    private Transform lastPlatform;
    private bool isObstacle;
    private int counter;

    void Start()
    {
        counter = 0;
        Init();
    }

    private void Init()
    {
        CreateEmptyPlatform();
        CreateEmptyPlatform();
        for (int i = 0; i < 5; i++)
        {
            CreatePlatform();
        }
    }

    public void CreatePlatform()
    {
        if (isObstacle)
        {
            CreateEmptyPlatform();
        }
        else
        {
            CreateObstaclePlatform();
        }
    }

    private void CreateObstaclePlatform()
    {
        Vector3 pos = (lastPlatform == null) ?
            platformContainer.position :
            lastPlatform.GetComponent<PlatformController>().EndPlatform.position;


        int index = Random.Range(0, ObstaclePlatforms.Length);
        GameObject res = Instantiate(ObstaclePlatforms[index], pos, Quaternion.identity, platformContainer);
        res.GetComponent<Spawner>().GetWorldController(_worldController);
        res.GetComponent<PlatformController>().GetWorldController(_worldController);
        res.GetComponent<PlatformController>().GetWorldBuilder(this);
        lastPlatform = res.transform;
        isObstacle = true;
    }

    private void CreateEmptyPlatform()
    {
        Vector3 pos = (lastPlatform == null) ?
            platformContainer.position :
            lastPlatform.GetComponent<PlatformController>().EndPlatform.position;

        int index = Random.Range(0, EmptyPlatforms.Length);
        GameObject res = Instantiate(EmptyPlatforms[index], pos, Quaternion.identity, platformContainer);
        if (counter > 2)
        {
            res.GetComponent<Spawner>().GetWorldController(_worldController);
        }
        res.GetComponent<PlatformController>().GetWorldController(_worldController);
        res.GetComponent<PlatformController>().GetWorldBuilder(this);
        lastPlatform = res.transform;
        isObstacle = false;
        counter++;
    }
}
