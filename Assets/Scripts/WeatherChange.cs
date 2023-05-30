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


    [SerializeField]
    public int weatherNum = 0;

    [SerializeField]
    int timeNum = 0;

    [SerializeField]
    Transform Rain;
    Transform Snow;

    public weatherType currentWeather; 

    // Start is called before the first frame update
    void Start()
    {
        Rain = transform.Find("RainParticleSystem");
        Snow = transform.Find("SnowParticleSystem");
        currentWeather = weatherType.Sun;
        StartCoroutine(Wait());

    }

    // Update is called once per frame

    IEnumerator Wait()
    {
        while (true)
        {
            timeNum = Random.Range(1, 60);
            yield return new WaitForSeconds(timeNum);
            Weather();
        }
    }

    public void Weather()
    {
        weatherNum = Random.Range(1, 10);
        if (weatherNum <= 2){
             Rain.gameObject.SetActive(true);
             Snow.gameObject.SetActive(false);
             currentWeather = weatherType.Rain;
        }
              
        else if (2 < weatherNum && weatherNum <= 4){
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

    }
}
