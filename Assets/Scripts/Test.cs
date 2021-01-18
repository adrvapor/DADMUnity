using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Input.compass.enabled = true;
        Input.location.Start(10, 0.01f);

        //Input.gyro.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.gyro.gravity.ToString());
        //Debug.Log(MicInput.MicLoudnessinDecibels);
        //Debug.Log(Input.acceleration.sqrMagnitude.ToString("#####.##"));
        Debug.Log(Input.compass.trueHeading);
    }
}
