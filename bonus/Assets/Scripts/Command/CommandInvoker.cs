using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    Controls inputAction;
    static Queue<ICommand> commandBuffer;
    static List<ICommand> commandHistory;

    static int counter;
    // Start is called before the first frame update
    void Start()
    {
        commandBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();
        
        //inputAction = PlayerInputController.controller.inputAction;
        //inputAction.Editor.Undo.performed += cntxt => UndoCommand();

        //redo action
    }
    public void AddCommand(ICommand command)
    {
        while(commandHistory.Count > counter) //number of items in the list is greater than counter
        {
            commandHistory.RemoveAt(counter);
        }
        commandBuffer.Enqueue(command);
    }
    public void UndoCommand(Vector3 copy)
    {
        if (commandBuffer.Count <=0)
        {
            if (counter >0)
            {
                counter--;
                commandHistory[counter].SetItem(copy);
                commandHistory[counter].Undo();
            }
        }       
    }

    // Update is called once per frame
    void Update()
    {
        if (commandBuffer.Count >0)
        {
            ICommand c = commandBuffer.Dequeue();
            c.Execute();

            commandHistory.Add(c);
            counter++;
            Debug.Log("Command history length:" + commandHistory.Count);
        }
    }
}
