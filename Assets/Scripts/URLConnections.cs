using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLConnections : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToTwitter()
    {
        Application.OpenURL("https://twitter.com/MostlyWizards");
    }

    public void GoToInstagram()
    {
        Application.OpenURL("https://instagram.com/mostlywizards?igshid=1afiq72yplmrp");
    }
}
