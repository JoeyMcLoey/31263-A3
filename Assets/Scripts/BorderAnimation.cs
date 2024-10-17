using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderAnimation : MonoBehaviour
{
    public GameObject border1;
    public GameObject border2;
    public float toggleInterval = 0.5f; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ToggleBorders());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ToggleBorders(){
        while (true){
            border1.SetActive(true);
            border2.SetActive(false);
            yield return new WaitForSeconds(toggleInterval);

            border1.SetActive(false);
            border2.SetActive(true);
            yield return new WaitForSeconds(toggleInterval);
        }
    }
}
