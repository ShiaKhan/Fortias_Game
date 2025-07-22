using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Model : IData
{
    private PlayerDataSO playerData;
    
    // Start is called before the first frame update
    public void Initialize()
    {
        if (playerData == null)
        {
            playerData= Resources.Load<PlayerDataSO>("PlayerDataSO");
        }
    }
    public Player_Model(){}

    // Update is called once per frame
    void Update()
    {
        
    }
}
