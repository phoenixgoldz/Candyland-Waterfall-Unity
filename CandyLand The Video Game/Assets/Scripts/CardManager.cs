using UnityEngine;

public class CardManager : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public card drawCard()
    { 
        card card = new card();
        if (Random.Range(0,100 ) <= -10)
        {
            card.color = TILE_TYPE.SPECIAL;
            card.special = (SPECIAL_TYPE)Random.Range(0, 5);
        }
        else
        {
            card.color = (TILE_TYPE)Random.Range(0, 6);
            card.special = SPECIAL_TYPE.NONE;
            if (Random.Range(0, 100) <= 30)
            { 
                card.doubleCard = true; 
            }
        }
       
        

        return card;
    }
}
