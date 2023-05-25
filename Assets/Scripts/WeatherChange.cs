using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherChange : MonoBehaviour
{
    
    [SerializeField]
    int weatherNum = 0;
    
    [SerializeField]
    int timeNum = 0;

    [SerializeField]
    Transform Rain;
    Transform Snow;
    
    // Start is called before the first frame update
    void Start()
    {
        Rain = transform.Find("RainParticleSystem");
        Snow = transform.Find("SnowParticleSystem");
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    
    IEnumerator Wait()
    {
       timeNum = Random.Range(1,60);
       yield return new WaitForSeconds(timeNum);
       Weather();
    }

    public void Weather()
    {
        weatherNum = Random.Range(1,10);
        if(weatherNum <= 5)
            Rain.gameObject.SetActive(true);
        else
            Rain.gameObject.SetActive(false);

        if(5 < weatherNum && weatherNum <= 7)
            Snow.gameObject.SetActive(true);
        else
            Snow.gameObject.SetActive(false);
    }
    
    void Update()
    {
        
    }
}
