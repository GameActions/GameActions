using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameActions.Utilities
{
    public class AnimationToken
    {
        public struct Token
        {
            private int TokenNumber;
            AnimationToken TokenPtr;
            Scene Scene;

            public bool IsValid
            {
                get => TokenNumber == TokenPtr.CurrentValue
                        && Application.isPlaying
                        && SceneManager.GetActiveScene() == Scene;
            }

            public Token(AnimationToken TokenPtr, Scene Scene)
            {
                TokenNumber = ++TokenPtr.CurrentValue;
                this.TokenPtr = TokenPtr;
                this.Scene = Scene;
            }
        }

        private int CurrentValue;

        public Token GetToken()
        {
            return new Token(this, SceneManager.GetActiveScene());
        }
    }
}
