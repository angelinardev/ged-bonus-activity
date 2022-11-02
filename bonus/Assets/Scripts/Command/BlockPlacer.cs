using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : ICommand
{
     Vector3 position;
    Transform item;

    GameObject copy;
    GameObject newitem;
     public BlockPlacer(Vector3 position, Transform item)
   {
        this.position = position;
        this.item = item;

        var allBlocks= GameObject.FindGameObjectsWithTag("Block");
        for (int i=0; i< allBlocks.Length; i++)
        {
            ItemPlacer.PlaceItem(allBlocks[i].transform);
        }
        copy = allBlocks[0];
   }
     public void Execute()
   {
        ItemPlacer.RemoveItem(position);
   }
   public void Undo()
   {
        ItemPlacer.AddItem(copy, position);
   }
   public void SetItem(Vector3 data)
   {
        position = data;
   }
}
