using System;
using Assets.Scripts.Forms_Controller.Message_Box_Form;
using GameCore;


/// <summary>
/// Represents a class to work with authorization.
/// </summary>
public static class MessageProcessingManager
{
    /// <summary>
    /// Instantiate a new instance of the <see cref="MessageBoxFormController"/> class.
    /// </summary>
    /// <param name="messageText">A message text.</param>
    /// <param name="buttonText">A button text.</param>
    private static void Instantiate(string messageText, string buttonText)
    {
        UnityEngine.Object.Instantiate(Main.MessageBoxFormPrefab).Initialize(messageText, buttonText);
    }

    /// <summary>
    /// Invoke a message box by message code. Overload for network.
    /// </summary>
    /// <param name="messageText">Code of the message.</param>
    public static void InvokeMessageBox(byte messageCode)
    {
        InvokeMessageBox((MessageCode)messageCode);
    }
    /// <summary>
    /// Invoke a message box by message text.
    /// </summary>
    /// <param name="messageText">Text of the message.</param>
    public static void InvokeMessageBox(string messageText)
    {
        Instantiate(messageText, "Ok");
    }
    /// <summary>
    /// Invoke a message box by message code.
    /// </summary>
    /// <param name="messageCode"></param>
    public static void InvokeMessageBox(MessageCode messageCode)
    {
        string messageText;
        GetMessageByCode(messageCode, out messageText);
        Instantiate(messageText, "Ok");
    }

    /// <summary>
    /// Gets a text message and button text by message code.
    /// </summary>
    /// <param name="messageCode">Code of the message.</param>
    /// <param name="messageText">A message text.</param>
    /// <param name="buttonText">A button text.</param>
    public static void GetMessageByCode(MessageCode messageCode, out string messageText, ref string buttonText)
    {
        switch (messageCode)
        {
            case MessageCode.LogInError:
                {
                    messageText = "Email or password aren't valid.";
                    break;
                }
            default:
                {
                    messageText = "Error";
                    break;
                }
        }
    }
    /// <summary>
    /// Gets a text message by message code.
    /// </summary>
    /// <param name="messageCode">Code of the message.</param>
    /// <param name="messageText">A message text.</param>
    public static void GetMessageByCode(MessageCode messageCode, out string messageText)

    {
        switch (messageCode)
        {
            case MessageCode.LogInError:
                {
                    messageText = "Email or password aren't valid.";
                    break;
                }
            default:
                {
                    messageText = "Error";
                    break;
                }
        }
    }
}