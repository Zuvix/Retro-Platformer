using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ItemBox : Box
{
    [SerializeField]
    Sprite fullSprite;
    [SerializeField]
    Sprite enmptySprite;
    bool isFull = true;
    [SerializeField]
    Item[] items;
    [SerializeField]
    float coinYdistance;
    [SerializeField]
    float coinUpTimer = 0.1f;


    void SpawnItem()
    {
        Item chosenItem=items[0];
        float cumulatedChance = 0f;
        float randomNum=Random.Range(0,100);
        foreach(Item selectedItem in items)
        {
            cumulatedChance += selectedItem.GetChance();
            if (cumulatedChance >=randomNum)
            {
                chosenItem = selectedItem;
                break;
            }        
        }

       Collectible spawnedItem= Instantiate(chosenItem.GetItem()).GetComponent<Collectible>();
       spawnedItem.transform.position = transform.position;
       spawnedItem.Deactivate();
       spawnedItem.transform.DOMoveY(startPosition.y + coinYdistance, coinUpTimer).OnComplete(spawnedItem.Activate);
    }
    protected override void TouchPlayer()
    {
        base.TouchPlayer();
        if (isFull)
        {
            spriteRenderer.sprite = enmptySprite;
            Invoke("SpawnItem",0.1f);
            isFull = false;
        }
    }


}
[System.Serializable]
public class Item
{
    [SerializeField]
    float chance;
    [SerializeField]
    GameObject item;
    public GameObject GetItem()
    {
        return item;
    }
    public float GetChance()
    {
        return chance;
    }

}
