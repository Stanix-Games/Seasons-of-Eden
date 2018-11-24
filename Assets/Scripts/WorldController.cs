using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    private static int colorMultShaderIndex = Shader.PropertyToID("_Color");

    [SerializeField] private List<GameObject> worldLayers;


    [Header("World time")]

    /**
     * Describes how many game-time units there are in one real-life second
     */
    [SerializeField] private float realTimeToGame = 60 * 60 * 8;

    /**
     * Describes how many game units there are in one in-game day
     * 
     * Defaults to 24 hours * 60 minutes * 60 seconds - basically same as real life
     */
    [SerializeField] private float timeUnitsInDay = 24 * 60 * 60;


    [Header("World lightning")]

    /**
     * Minimum light that could be ( light of moon reflections )
     */
    [SerializeField] private float ambientBaseLight = 0.1f;

    /**
     * Angle of sun's minimun shine angle. Any angle lower than this will have 0 light from sun
     * <see cref="sunAngleTo"/>
     */
    [SerializeField] private float sunAngleFrom = 20;
    /**
     * Angle of sun's maximum shine angle. Any angle lower than this will have 0 light from sun
     * <see cref="sunAngleFrom"/>
     */
    [SerializeField] private float sunAngleTo = 60;


    /**
     * Describes how many units did pass from last day
     */
    private float currentDayTime;

    private List<Renderer> worldRenderers;

    void Start()
    {
        worldRenderers = worldLayers.Select(it => it.GetComponent<Renderer>()).ToList();
    }

    void Update()
    {
        currentDayTime += Time.deltaTime * realTimeToGame;
        currentDayTime %= timeUnitsInDay;

        // Assumes that we are at 0 lat/long
        float invSunAngle = 90 - Mathf.Abs(timeUnitsInDay * 0.5f - currentDayTime) / timeUnitsInDay * 180;
        float light = Mathf.Clamp((invSunAngle - sunAngleFrom) / sunAngleTo, ambientBaseLight, 1f);

        foreach (var renderer in worldRenderers)
        {
            renderer.material.SetColor(colorMultShaderIndex, new Color(light, light, light));
        }
    }
}
