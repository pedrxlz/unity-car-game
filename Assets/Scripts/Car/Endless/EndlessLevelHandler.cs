using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] sectionsPrefab;

    GameObject[] sectionsPool = new GameObject[20];

    GameObject[] sections = new GameObject[10];

    Transform playerCarTransform;

    WaitForSeconds waitFor100ms = new WaitForSeconds(0.1f);

    public float sectionLength;

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        for (int i = 0; i < sectionsPool.Length; i++)
        {
            sectionsPool[i] = Instantiate(sectionsPrefab[prefabIndex]);
            sectionsPool[i].SetActive(false);

            prefabIndex++;

            if (prefabIndex == sectionsPrefab.Length)
            {
                prefabIndex = 0;
            }
        }

        for (int i = 0; i < sections.Length; i++)
        {
            sections[i] = Instantiate(sectionsPool[Random.Range(0, sectionsPool.Length)]);
            sections[i].transform.position = new Vector3(sectionsPool[i].transform.position.x, 0, i * sectionLength);
            sections[i].SetActive(true);
            
        }

        StartCoroutine(UpdateLessOfTenCO());
    }

    IEnumerator UpdateLessOfTenCO()
    {
        while(true)
        {
            UpdateSectionPositions();
            yield return waitFor100ms;
        }
    }

    void UpdateSectionPositions()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            // Check if section is too far behind
            if (sections[i].transform.position.z - playerCarTransform.position.z < -sectionLength)
            {
                // Store the position of the section and disable it
                Vector3 lastSectionPosition = sections[i].transform.position;
                sections[i].SetActive(false);

                // Get new section & enable it & move it foward
                sections[i] = Instantiate(sectionsPool[Random.Range(0, sectionsPool.Length)]);

                // Move the new section into place and active it
                sections[i].transform.position = new Vector3(lastSectionPosition.x, 0, lastSectionPosition.z + sectionLength * sections.Length );
                sections[i].SetActive(true);

            }
        }
    }
}