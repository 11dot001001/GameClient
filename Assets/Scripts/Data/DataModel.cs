using ClientModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data
{
    public static class DataModel
    {
        private static readonly Dictionary<int, OtherAccount> _otherAccounts = new Dictionary<int, OtherAccount>();
        private static readonly Dictionary<int, OtherAccountRequest> _otherAccountRequestes = new Dictionary<int, OtherAccountRequest>();

        public static OwnAccount AccountInfo { get; private set; }

        public static void Initialize(OwnAccount ownAccount)
        {
            AccountInfo = ownAccount;
            foreach (OtherAccount item in ownAccount.Friends)
                _otherAccounts.Add(item.Id, item);
        }

        public static void Reset()
        {
            AccountInfo = null;
            _otherAccounts.Clear();
            _otherAccountRequestes.Clear();
        }

        public static void GetOtherAccount(int id, Action<OtherAccount> response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            OtherAccount otherAccount;
            if (_otherAccounts.TryGetValue(id, out otherAccount))
            {
                response(otherAccount);
                return;
            }

            OtherAccountRequest otherAccountRequest;
            if (_otherAccountRequestes.TryGetValue(id, out otherAccountRequest))
            {
                otherAccountRequest.Responses.Add(response);
                return;
            }

            _otherAccountRequestes.Add(id, new OtherAccountRequest(response));
            Network.GetOtherAccount(id);
        }

        public static void ReceiveOtherAccount(OtherAccount otherAccount)
        {
            if (otherAccount == null)
                throw new ArgumentNullException();
            OtherAccountRequest request = _otherAccountRequestes[otherAccount.Id];
            _otherAccountRequestes.Remove(otherAccount.Id);
            _otherAccounts.Add(otherAccount.Id, otherAccount);
            foreach (Action<OtherAccount> response in request.Responses)
                response(otherAccount);
        }

        private class OtherAccountRequest
        {
            public List<Action<OtherAccount>> Responses;

            public OtherAccountRequest(Action<OtherAccount> response)
            {
                Responses = new List<Action<OtherAccount>>
                {
                    response
                };
            }
        }
    }

    public class Ui
    {
        OtherAccount _otherAccountUI;

        public Ui(int id)
        {
            DataModel.GetOtherAccount(id, SetAccount);
        }
        public void SetAccount(OtherAccount otherAccount) => _otherAccountUI = otherAccount;
    }
}
