using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<PadController> wallPads;       // Assign all 5 wall pads
    public Transform bossDoor;
    private float[] displayTimes = { 3f, 2f, 1.2f }; // Level 1 → 3

    private int currentLevel = 0;
    private int maxLevels => displayTimes.Length; // Add this

    private List<Color> availableColors = new List<Color>();
    private List<Color> sequence = new();
    private int sequenceLength = 5;
    private bool acceptingInput = false;
    private int inputIndex = 0;

    void Start()
    {
        // Match the colors used on your floor pads
        availableColors.Add(Color.green);
        availableColors.Add(Color.yellow);
        availableColors.Add(Color.black);
    }

    public void StartPuzzle()
    {
        currentLevel = 0;
        StartCoroutine(PlayLevel());
    }

    IEnumerator PlayLevel()
    {
        Debug.Log($"Level {currentLevel + 1} starting...");
        sequence.Clear();
        inputIndex = 0;
        acceptingInput = false;

        // Generate a full 5-color sequence
        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(availableColors[Random.Range(0, availableColors.Count)]);
        }

        // Display the sequence
        for (int i = 0; i < wallPads.Count; i++)
        {
            wallPads[i].ShowSequenceColor(sequence[i]);
        }

        yield return new WaitForSeconds(displayTimes[currentLevel]);

        // Hide all wall pads
        foreach (PadController pad in wallPads)
        {
            pad.ResetPad();
        }

        acceptingInput = true;
    }

    public void PlayerStep(Color selectedColor)
    {
        if (!acceptingInput || inputIndex >= sequence.Count)
            return;

        wallPads[inputIndex].SetPlayerColor(selectedColor);
        inputIndex++;

        if (inputIndex < sequence.Count) return;

        // Check full input
        bool correct = true;
        for (int i = 0; i < sequenceLength; i++)
        {
            if (wallPads[i].GetCurrentColor() != sequence[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            Debug.Log($"Level {currentLevel + 1} complete!");

            currentLevel++;

            if (currentLevel >= maxLevels)
            {
                PuzzleComplete();
            }
            else
            {
                acceptingInput = false;
                inputIndex = 0;
                sequence.Clear();
                availableColors.Clear();

                StartCoroutine(WaitAndStartNextLevel());
            }
        }



        else
        {
            StartCoroutine(ResetAttempt());
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





