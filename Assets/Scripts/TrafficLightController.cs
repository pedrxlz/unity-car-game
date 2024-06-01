using UnityEngine;
using System.Collections;

public class TrafficLightController : MonoBehaviour
{
    public GameObject trafficLight;

    public Material greenMaterial;
    public Material yellowMaterial;
    public Material redMaterial;
    public Material greenLightningMaterial;
    public Material yellowLightningMaterial;
    public Material redLightningMaterial;

    private enum TrafficLightState { Green, Yellow, Red }
    private TrafficLightState currentState;

    public float greenLightDuration = 5f;
    public float yellowLightDuration = 2f;
    public float redLightDuration = 5f;

    private void Start()
    {
        StartCoroutine(TrafficLightCycle());
    }

    private IEnumerator TrafficLightCycle()
    {
        while (true)
        {
            // Green Light
            SetTrafficLightState(TrafficLightState.Green);
            yield return new WaitForSeconds(greenLightDuration);

            // Yellow Light
            SetTrafficLightState(TrafficLightState.Yellow);
            yield return new WaitForSeconds(yellowLightDuration);

            // Red Light
            SetTrafficLightState(TrafficLightState.Red);
            yield return new WaitForSeconds(redLightDuration);
        }
    }

    private void SetTrafficLightState(TrafficLightState state)
    {
        Material[] materials = trafficLight.GetComponent<Renderer>().materials;

        currentState = state;
        materials[1] = null;
        materials[2] = null;
        materials[3] = null;
        materials[4] = null;
        materials[5] = null;

        switch (state)
        {
            case TrafficLightState.Green:
                materials[1] = greenLightningMaterial;
                materials[2] = yellowMaterial;
                materials[3] = redMaterial;
                break;
            case TrafficLightState.Yellow:
                materials[1] = greenMaterial;
                materials[2] = yellowLightningMaterial;
                materials[3] = redMaterial;
                break;
            case TrafficLightState.Red:
                materials[1] = greenMaterial;
                materials[2] = yellowMaterial;
                materials[3] = redLightningMaterial;
                break;
        }

        trafficLight.GetComponent<Renderer>().materials = materials;
    }
}
