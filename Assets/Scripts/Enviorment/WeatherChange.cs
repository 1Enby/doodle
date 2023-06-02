using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum weatherType
    {
        Rain,
        Snow,
        Sun

    }
public class WeatherChange : MonoBehaviour
{

    public GameObject Audio;


    [SerializeField]
    public float weatherNum = 0.0f;

    [SerializeField]
    int timeNum = 0;

    [SerializeField]
    Transform Rain;
    Transform Snow;

    public weatherType currentWeather; 

    ScaleFromMicrophone mic_script;

    // Start is called before the first frame update
    void Start()
    {
        Rain = transform.Find("RainParticleSystem");
        Snow = transform.Find("SnowParticleSystem");
        currentWeather = weatherType.Sun;
        // StartCoroutine(Wait());
        

        mic_script = GetComponent<ScaleFromMicrophone>();

        
    }

    // Update is called once per frame

    IEnumerator Wait()
    {
        while (true)
        {
            // timeNum = Random.Range(1, 60);
            // yield return new WaitForSeconds(timeNum);
            Weather();
        }
    }

    public void Weather()
    {
        weatherNum = mic_script.loudness;
        if (weatherNum <= 0.001f){
             Rain.gameObject.SetActive(true);
             Snow.gameObject.SetActive(false);
             currentWeather = weatherType.Rain;
        }
              
        else if (0.001f < weatherNum && weatherNum <= 0.002f){
            Snow.gameObject.SetActive(true);
            Rain.gameObject.SetActive(false);
            currentWeather = weatherType.Snow;
        }
        else{
            Snow.gameObject.SetActive(false);
            Rain.gameObject.SetActive(false);

            currentWeather = weatherType.Sun;
        }
    }

    void Update()
    {
        Weather();
    }
}
