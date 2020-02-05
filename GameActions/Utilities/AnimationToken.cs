using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameActions.Utilities
{
    public class AnimationToken
    {
        public struct Token
        {
            private readonly int TokenNumber;
            private readonly AnimationToken TokenPtr;
            private readonly Scene Scene;

            public bool IsValid
            {
                get => (TokenPtr == null || TokenNumber == TokenPtr.CurrentValue)
                        && Application.isPlaying
                        && SceneManager.GetActiveScene() == Scene;
            }

            public bool IsSceneUnchangedAndPlaying
            {
                get => Application.isPlaying && SceneManager.GetActiveScene() == Scene;
            }

            public Token(AnimationToken TokenPtr)
            {
                if (TokenPtr == null)
                    TokenNumber = 0;
                else
                    TokenNumber = ++TokenPtr.CurrentValue;
                this.TokenPtr = TokenPtr;
                Scene = SceneManager.GetActiveScene();
            }

            public static Token Default => new Token(null);
        }

        private int CurrentValue;

        public Token GetToken()
        {
            return new Token(this);
        }

        public static Token DefaultToken => Token.Default;
    }
}
