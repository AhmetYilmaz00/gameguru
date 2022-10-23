using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class MenuController : MonoBehaviour
    {
        public void GridGameStart()
        {
            SceneManager.LoadScene("GridGameScene");
        }

        public void StackGameStart()
        {
            SceneManager.LoadScene("StackGameScene");

        }
    }
}
