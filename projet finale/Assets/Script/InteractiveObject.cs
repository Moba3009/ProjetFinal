using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private GameObject _questionSpawnerGameObject;  // GameObject contenant le script QuestionSpawner

    private bool _hasBeenUsed = false;  // Empêche l'interaction multiple avec le même objet

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasBeenUsed)
        {
            _hasBeenUsed = true;  // Marquer cet objet comme "utilisé"
            if (_questionSpawnerGameObject != null)
            {
                QuestionSpawner questionSpawner = _questionSpawnerGameObject.GetComponent<QuestionSpawner>();
                if (questionSpawner != null)
                {
                    questionSpawner.StartQuestionnaire();  // Déclencher une question
                }
            }
            else
            {
                Debug.LogError("QuestionSpawner GameObject n'est pas assigné.");
            }
        }
    }

    public void SetQuestionSpawner(GameObject questionSpawnerGameObject)
    {
        _questionSpawnerGameObject = questionSpawnerGameObject;
    }
}
