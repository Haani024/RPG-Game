using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<PadController> wallPads;       // Assign all 5 wall pads
    public Transform bossDoor;
    private float[] displayTimes = { 3f, 2f, 1.2f }; // Level 1 → 3
    private bool puzzleStarted = false;


    private int currentLevel = 0;
    private int maxLevels => displayTimes.Length; // Add this

    private Dictionary<int, Color> colorMap = new();
    private List<int> sequence = new();
    private List<int> playerInput = new();
    private int sequenceLength = 5;
    private bool acceptingInput = false;
    private int inputIndex = 0;

    void Start()
    {
        colorMap.Add(0, Color.green);
        colorMap.Add(1, Color.yellow);
        colorMap.Add(2, Color.black);
    }
    
   

    public void StartPuzzle()
    {
        Debug.Log("StartPuzzle() called"); // Debug
        puzzleStarted = true;
        currentLevel = 0;
        inputIndex = 0;
        sequence.Clear();
        playerInput.Clear();

        StartCoroutine(PlayLevel());
    }



    IEnumerator PlayLevel()
    {
        Debug.Log($"Level {currentLevel + 1} starting...");
        sequence.Clear();
        inputIndex = 0;
        acceptingInput = false;

        for (int i = 0; i < sequenceLength; i++)
        {
            int colorIndex = Random.Range(0, colorMap.Count);
            sequence.Add(colorIndex);
        }


        for (int i = 0; i < wallPads.Count; i++)
        {
            wallPads[i].ShowSequenceColor(colorMap[sequence[i]]);
        }

        yield return new WaitForSeconds(displayTimes[currentLevel]);

        // Hide all wall pads
        foreach (PadController pad in wallPads)
        {
            pad.ResetPad();
        }

        acceptingInput = true;
    }

    public void PlayerStep(int colorIndex)
    {
        if (!acceptingInput || inputIndex >= sequenceLength)
            return;

        wallPads[inputIndex].SetPlayerColor(colorMap[colorIndex]);
        playerInput.Add(colorIndex);
        inputIndex++;

        if (playerInput.Count < sequenceLength) return;

        // Check result
        bool correct = true;
        for (int i = 0; i < sequenceLength; i++)
        {
            if (playerInput[i] != sequence[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            currentLevel++;
            if (currentLevel >= displayTimes.Length)
            {
                PuzzleComplete();
            }
            else
            {
                acceptingInput = false;
                inputIndex = 0;
                sequence.Clear();
                playerInput.Clear();
                StartCoroutine(WaitAndStartNextLevel());
            }
        }
        else
        {
            StartCoroutine(ResetAttempt());
        }
    }
    
    public void RestartPuzzle()
    {
        Debug.Log("Puzzle manually restarted.");
        StopAllCoroutines();

        currentLevel = 0;
        inputIndex = 0;
        sequence.Clear();
        playerInput.Clear();

        foreach (var pad in wallPads)
        {
            pad.ResetPad();
        }

        StartCoroutine(PlayLevel());
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"E pressed - puzzleStarted: {puzzleStarted}, acceptingInput: {acceptingInput}");

            if (puzzleStarted)
            {
                Debug.Log("Restarting puzzle...");
                RestartPuzzle();
            }
        }
    }


   




    IEnumerator ResetAttempt()
    {
        acceptingInput = false;
        yield return new WaitForSeconds(1f);

        foreach (PadController pad in wallPads)
        {
            pad.ResetPad();
        }

        inputIndex = 0;
        acceptingInput = true;
    }

    void PuzzleComplete()
    {
        Debug.Log("Puzzle complete. Boss door opening.");
        puzzleStarted = false;

        if (bossDoor != null)
        {
            bossDoor.gameObject.SetActive(false);
        }
    }

    
    IEnumerator WaitAndStartNextLevel()
    {
        Debug.Log($"Starting Level {currentLevel + 1}...");
        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayLevel());
    }

}





