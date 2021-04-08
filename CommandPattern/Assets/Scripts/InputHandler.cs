using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;
    
    private Animator anim;
    private Command keyA, keyZ, keyE, upArrow;
    private List<Command> oldCommands = new List<Command>();

    private Coroutine replayCoroutine;
    private bool shouldStartReplay;
    private bool isReplaying;
    
    // Start is called before the first frame update
    void Start()
    {
        keyA = new PerformJump();
        keyZ = new PerformKick();
        keyE = new PerformPunch();
        upArrow = new MoveForward();

        anim = actor.GetComponent<Animator>();
        Camera.main.GetComponent<CameraFollow360>().player = actor.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReplaying)
        {
            HandleInput();
        }

        StartReplay();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            keyA.Execute(anim, true);
            oldCommands.Add(keyA);
        } 
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            keyZ.Execute(anim, true);
            oldCommands.Add(keyZ);
        } 
        else if (Input.GetKeyDown(KeyCode.E))
        {
            keyE.Execute(anim, true);
            oldCommands.Add(keyE);
        } 
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upArrow.Execute(anim, true);
            oldCommands.Add(upArrow);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shouldStartReplay = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UndoLastCommand();
        }
    }

    void UndoLastCommand()
    {
        if (oldCommands.Count > 0)
        {
            Command c = oldCommands[oldCommands.Count - 1];
            c.Execute(anim, false);
            oldCommands.RemoveAt(oldCommands.Count - 1);
        }
    }

    void StartReplay()
    {
        if (shouldStartReplay && oldCommands.Count > 0)
        {
            shouldStartReplay = false;
            if (replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);    
            }

            replayCoroutine = StartCoroutine(ReplayCommands());
        }
    }

    IEnumerator ReplayCommands()
    {
        isReplaying = true;

        for (int i = 0; i < oldCommands.Count; i++)
        {
            oldCommands[i].Execute(anim, true);
            yield return new WaitForSeconds(1f);
        }

        isReplaying = false;
    }
}
