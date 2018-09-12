using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using NHibernateToMySql.Manager;
using NHibernateToMySql.Model;
using Photon.SocketServer;

/// <summary>
/// Sample示例
/// </summary>
namespace MyGameServer.Handler {
    class RegisterHandler : HandlerBase {

        public RegisterHandler() {
            OpCode = OperationCode.Register;       // 构造函数中标识自己是什么操作码
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer) {
            // 取数据
            Dictionary<byte, object> data = operationRequest.Parameters;
            object username = null;
            object password = null;
            bool isGetName = data.TryGetValue((byte)ParameterCode.Username, out username);
            bool isGetPassword = data.TryGetValue((byte)ParameterCode.Password, out password);

            if (isGetName && isGetPassword) {
                // 插入数据
                User newUser = new User() { Name = (string)username, Password = (string)password };
                UserManager userManager = new UserManager();
                bool isSuccess = userManager.Add(newUser);
                MyGameServer.log.Info("注册用户：" + isSuccess);

                // 响应
                OperationResponse response = new OperationResponse(operationRequest.OperationCode);     // 使用请求码指定响应的是哪个请求
                if (isSuccess)
                    response.ReturnCode = (short)ReturnCode.Success;
                else
                    response.ReturnCode = (short)ReturnCode.Fail;
                peer.SendOperationResponse(response, sendParameters);       //发起响应
            }
        }
    }
}
