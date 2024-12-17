using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private GameObject _questionSpawnerGameObject;  // GameObject contenant le script QuestionSpawner

    private bool _hasBeenUsed = false;  // Emp�che l'interaction multiple avec le m�me objet

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasBeenUsed)
        {
            _hasBeenUsed = true;  // Marquer cet objet comme "utilis�"
            if (_questionSpawnerGameObject != null)
            {
                QuestionSpawner questionSpawner = _questionSpawnerGameObject.GetComponent<QuestionSpawner>();
                if (questionSpawner != null)
                {
                    questionSpawner.StartQuestionnaire();  // D�clencher une question
                }
            }
            else
            {
                Debug.LogError("QuestionSpawner GameObject n'est pas assign�.");
            }
        }
    }

    public void SetQuestionSpawner(GameObject questionSpawnerGameObject)
    {
        _questionSpawnerGameObject = questionSpawnerGameObject;
    }
}
