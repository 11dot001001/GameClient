using System.Collections.Generic;
using Assets.Scripts.Data;
using ClientModel.Data;
using GameCore.Model;
using Noname.BitConversion;
using Noname.BitConversion.System;
using Noname.BitConversion.System.Collections.Generic;
using Noname.Net.RPC;

public class Client : RPCClient
{
    public RemoteProcedure<string, string, string> SignUp { get; private set; }
    public RemoteProcedure<string, string> LogIn { get; private set; }
    public RemoteProcedure<string> IsEmailExists { get; private set; }
    public RemoteProcedure<string> IsNicknameExists { get; private set; }
    public RemoteProcedure<int> GetOtherAccount { get; private set; }
    public RemoteProcedure FindGame { get; private set; }
    public RemoteProcedure<IEnumerable<int>, int> RequestSendViruses { get; private set; }

    public Client(string ipAddress, int port) : base(ipAddress, port) { }

    protected override void InitializeLocalProcedures()
    {
        ReliableBitConverter<OwnAccount> accountNullableConverter = ReliableBitConverter.GetInstance(NullableBitConverter.GetInstance(OwnAccount.BitConverter));
        ReliableBitConverter<OtherAccount> otherAccountNullableConverter = ReliableBitConverter.GetInstance(NullableBitConverter.GetInstance(OtherAccount.BitConverter));

        DefineLocalProcedure(true, AuthenticationManager.CheckEmailExistsResponse, BooleanBitConverter.Instance);
        DefineLocalProcedure(true, AuthenticationManager.CheckNickameExistsResponse, BooleanBitConverter.Instance);
        DefineLocalProcedure(true, AuthenticationManager.SignUpResponse, ByteBitConverter.Instance, accountNullableConverter);
        DefineLocalProcedure(true, AuthenticationManager.LogInResponse, accountNullableConverter);
        DefineLocalProcedure(true, MessageProcessingManager.InvokeMessageBox, ByteBitConverter.Instance);
        DefineLocalProcedure(true, DataModel.ReceiveOtherAccount, otherAccountNullableConverter);
        DefineLocalProcedure(true, SendGameSettings, ReliableBitConverter.GetInstance(GameSettings.BitConverter));
        DefineLocalProcedure(true, MenuManager.StartGame);
        DefineLocalProcedure(true, GameManager.SendVirusGroup, VirusGroupData.BitConverter.Instance, Int32BitConverter.Instance);
        DefineLocalProcedure(true, GameManager.SendVirusGroupArrived, Int32BitConverter.Instance, Int32BitConverter.Instance);
    }
    protected override void InitializeRemoteProcedures()
    {
        ReliableBitConverter<IEnumerable<int>> iEnumerableBacteriumId = ReliableBitConverter.GetInstance(IEnumerableVariableLengthBitConverter.GetInstance(Int32BitConverter.Instance));

        SignUp = DefineRemoteProcedure(StringBitConverter.UnicodeReliableInstance, StringBitConverter.UnicodeReliableInstance, StringBitConverter.UnicodeReliableInstance);
        LogIn = DefineRemoteProcedure(StringBitConverter.UnicodeReliableInstance, StringBitConverter.UnicodeReliableInstance);
        IsEmailExists = DefineRemoteProcedure(StringBitConverter.UnicodeReliableInstance);
        IsNicknameExists = DefineRemoteProcedure(StringBitConverter.UnicodeReliableInstance);
        GetOtherAccount = DefineRemoteProcedure(Int32BitConverter.Instance);
        FindGame = DefineRemoteProcedure();
        RequestSendViruses = DefineRemoteProcedure(iEnumerableBacteriumId, Int32BitConverter.Instance);
    }

    private void SendGameSettings(GameSettings gameSettings) => GameManager.Instantiate(gameSettings);
}
