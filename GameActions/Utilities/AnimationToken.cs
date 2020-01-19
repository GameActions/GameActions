using UnityEngine;

namespace GameActions.Utilities
{
    public class AnimationToken
    {
        public struct Token
        {
            private int TokenNumber;
            AnimationToken TokenPtr;

            public bool IsValid
            {
                get => TokenNumber == TokenPtr.CurrentValue && Application.isPlaying;
            }

            public Token(AnimationToken TokenPtr)
            {
                TokenNumber = ++TokenPtr.CurrentValue;
                this.TokenPtr = TokenPtr;
            }
        }

        private int CurrentValue;

        public Token GetToken()
        {
            return new Token(this);
        }
    }
}
