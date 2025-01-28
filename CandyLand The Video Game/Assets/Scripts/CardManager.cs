using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] public List<Sprite> cardImagesSingle;
    [SerializeField] public List<Sprite> cardImagesDouble;
    [SerializeField] public List<Sprite> cardImagesSpecial;
 
    public card drawCard()
    { 
        card card = new card();
        if (Random.Range(0,100 ) <= 10)
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

        if (card.doubleCard)
        {
            switch (card.color)
            {
                case TILE_TYPE.RED:
                    card.cardImg = cardImagesDouble[0];
                    break;
                case TILE_TYPE.PURPLE:
                    card.cardImg = cardImagesDouble[1];
                    break;
                case TILE_TYPE.YELLOW:
                    card.cardImg = cardImagesDouble[2];
                    break;
                case TILE_TYPE.BLUE:
                    card.cardImg = cardImagesDouble[3];
                    break;
                case TILE_TYPE.ORANGE:
                    card.cardImg = cardImagesDouble[4];
                    break;
                case TILE_TYPE.GREEN:
                    card.cardImg = cardImagesDouble[5];
                    break;
            }
        }
        else if (card.special != SPECIAL_TYPE.NONE)
        {
            switch (card.special)
            {
                case SPECIAL_TYPE.GINGERBREAD:
                    card.cardImg = cardImagesSpecial[0];
                    break;
                case SPECIAL_TYPE.CANDYCANE:
                    card.cardImg = cardImagesSpecial[1];
                    break;
                case SPECIAL_TYPE.LOLLIPOP:
                    card.cardImg = cardImagesSpecial[2];
                    break;
                case SPECIAL_TYPE.GUMDROP:
                    card.cardImg = cardImagesSpecial[3];
                    break;
                case SPECIAL_TYPE.ICECREAM:
                    card.cardImg = cardImagesSpecial[4];
                    break;
                case SPECIAL_TYPE.PEANUT:
                    card.cardImg = cardImagesSpecial[5];
                    break;
            }
        }
        else
        {
            switch (card.color)
            {
                case TILE_TYPE.RED:
                    card.cardImg = cardImagesSingle[0];
                    break;
                case TILE_TYPE.PURPLE:
                    card.cardImg = cardImagesSingle[1];
                    break;
                case TILE_TYPE.YELLOW:
                    card.cardImg = cardImagesSingle[2];
                    break;
                case TILE_TYPE.BLUE:
                    card.cardImg = cardImagesSingle[3];
                    break;
                case TILE_TYPE.ORANGE:
                    card.cardImg = cardImagesSingle[4];
                    break;
                case TILE_TYPE.GREEN:
                    card.cardImg = cardImagesSingle[5];
                    break;
            }
        }

        return card;
    }
}
