using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    public void Execute();
    public void Undo();
    public void SetItem(Vector3 data);
}