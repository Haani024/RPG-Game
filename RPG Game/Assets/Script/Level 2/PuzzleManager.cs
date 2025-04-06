using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<PadController> wallPads; // Assign these in Inspector
    public Transform bossDoor;           // Assign boss door object
    public float displayTime = 3f;

    private List<int> sequence = new();
    private List<int> playerInput = new();

    private int currentLevel = 0; // 0 = easy, 1 = medium, 2 = hard
    private int[] sequenceLengths = { 3, 4, 5 };

    public bool acceptingInput = false;

    public void StartPuzzle()
    {
        currentLevel = 0;
        StartCoroutine(PlayLevel());
    }

    IEnumerator PlayLevel()
    {
        playerInput.Clear();
        sequence.Clear();
        acceptingInput = false;

        yield return new WaitForSeconds(1f);

        int length = sequenceLengths[currentLevel];

        // Generate random sequence
        for (int i = 0; i < length; i++)
        {
            sequence.Add(Random.Range(0, wallPads.Count));
        }

        // Flash sequence on wall pads
        foreach (int index in sequence)
        {
            StartCoroutine(wallPads[index].Flash());
            yield return new WaitForSeconds(0.7f);
        }

        yield return new WaitForSeconds(displayTime);
        acceptingInput = true;
    }

    public void PlayerStep(int padIndex)
    {
        if (!acceptingInput) return;

        playerInput.Add(padIndex);

        for (int i = 0; i < playerInput.Count; i++)
        {
            if (playerInput[i] != sequence[i])
            {
                Debug.Log("Incorrect. Try again.");
                StartCoroutine(RestartLevel());
                return;
            }
        }

        if (playerInput.Count == sequence.Count)
        {
            Debug.Log("Level Complete!");
            currentLevel++;

            if (currentLevel >= sequenceLengths.Length)
            {
                PuzzleComplete();
            }
            else
            {
                StartCoroutine(PlayLevel());
            }
        }
    }

    IEnumerator RestartLevel()
    {
        acceptingInput = false;
        playerInput.Clear();
        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayLevel());
    }

    void PuzzleComplete()
    {
        Debug.Log("Puzzle Finished! Boss door unlocked.");
        if (bossDoor != null)
        {
            bossDoor.gameObject.SetActive(false); // Or play an animation
        }
    }
}
