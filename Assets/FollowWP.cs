using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP = 0;

    public float speed = 10f;
    public float rotSpeed = 10f;
    public float lookAhead = 10f;

    private GameObject tracker;

    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;

    }

    private void ProgressTracker()
    {
        if(Vector3.Distance(tracker.transform.position,transform.position) > lookAhead)
        {
            return;
        }

        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3)
        {
            currentWP++;
        }

        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }

        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, (speed + 20) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        ProgressTracker();

        //transform.LookAt(waypoints[currentWP].transform);

        Quaternion lookAtWP = Quaternion.LookRotation(tracker.transform.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtWP, rotSpeed * Time.deltaTime);

        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
