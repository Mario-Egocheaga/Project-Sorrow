using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShadowDetection;

public class LightGeneration : MonoBehaviour
{
    public Transform parent;

    public GameObject[] lightSources;
    public float spawnRadius;
    public int numLights;
    public ShadowDetection.ShadowDetection shadow;



    // Start is called before the first frame update
    void Awake()
    {
        parent = this.transform;
        SpawnLights();
    }

    void SpawnLights()
    {
        for (int i = 0; i < numLights; i++)
        {
            GameObject lightProp = lightSources[Random.Range(0, lightSources.Length)];
            GameObject lightInstance = Instantiate(lightProp, new Vector3(parent.transform.position.x + Random.Range(-12f, 12f), 0, parent.transform.position.z + Random.Range(-12f, 12f)), Quaternion.identity);
            lightInstance.transform.parent = parent.transform;
            lightInstance.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + Random.Range(0f, 360f), transform.rotation.z);

            Light lightComp = lightInstance.GetComponentInChildren<Light>();

            shadow.Lights.Add(lightComp);
            
        }
    }

}
