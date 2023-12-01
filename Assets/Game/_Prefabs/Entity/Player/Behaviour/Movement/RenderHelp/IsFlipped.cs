using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSpace
{
    public class IsFlipped : MonoBehaviour
    {
        [SerializeField] PlayerData playerData;

        private void Update()
        {
            playerData.playerRenderer.flipX = playerData.direction != 1;
        }
    }
}
