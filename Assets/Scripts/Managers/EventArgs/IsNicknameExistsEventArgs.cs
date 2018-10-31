using Assets.Scripts;
using Assets.Scripts.Forms_Controller.Startup_Form;
using ClientModel;
using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides data for <see cref="AuthenticationManager.IsNicknameExistsResponseInvoke"/> event.
/// </summary>
public class IsNicknameExistsEventArgs : EventArgs
{
    /// <summary>
    /// Gets the answer of the nickname exists.
    /// </summary>
    public bool Answer { get; }

    /// <summary>
    /// Initiallizes a new instance of the <see cref="IsNicknameExistsEventArgs"/> class.
    /// </summary>
    /// <param name="answer">The answer to the query about the existence of the nickname.</param>
    public IsNicknameExistsEventArgs(bool answer)
    {
        Answer = answer;
    }
}