using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Provides data for <see cref="AuthenticationManager.IsEmailExistsResponseInvoke"/> event.
    /// </summary>
    public class IsEmailExistsEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the answer of the email exists.
        /// </summary>
        public bool Answer { get; private set; }

        /// <summary>
        /// Initiallizes a new instance of the <see cref="IsNicknameExistsEventArgs"/> class.
        /// </summary>
        /// <param name="answer">The answer to the query about the existence of the email.</param>
        public IsEmailExistsEventArgs(bool answer)
        {
            Answer = answer;
        }
    }
}
