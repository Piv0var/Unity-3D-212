using System.Linq;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Light[] dayLights;
    private Light[] nightLights;
    void Start()
    {
        dayLights = GameObject
            .FindGameObjectsWithTag("DayLight")
            .Select(x => x.GetComponent<Light>())
            .ToArray();
        nightLights = GameObject
            .FindGameObjectsWithTag("NightLight")
            .Select(x => x.GetComponent<Light>())
            .ToArray();
        GameState.isDay = true;
        SetLights(GameState.isDay);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            GameState.isDay = !GameState.isDay;
            SetLights(GameState.isDay);
        }
    }
    private void SetLights(bool day)
    {
        foreach(Light light in dayLights)
        {
            light.enabled = day;
        }
        foreach (Light light in nightLights)
        {
            light.enabled = !day;
        }
    }
}
