using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using NHibernateToMySql.Manager;
using Photon.SocketServer;

/// <summary>
/// Sample示例
/// </summary>
namespace MyGameServer.Handler {
    class LoginHandler : HandlerBase {

        public LoginHandler() {
            OpCode = OperationCode.Login;       // 构造函数中标识自己是什么操作码
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer) {
            // 取数据
            Dictionary<byte, object> data = operationRequest.Parameters;
            object username = null;
            object password = null;
            bool isGetName = data.TryGetValue((byte)ParameterCode.Username, out username);
            bool isGetPassword = data.TryGetValue((byte)ParameterCode.Password, out password);

            if (isGetName && isGetPassword) {
                // 新建响应
                OperationResponse response = new OperationResponse(operationRequest.OperationCode);     // 使用请求码指定响应的是哪个请求;

                // 查询是否已经登录
                foreach (var p in MyGameServer.Instance.PeerList) {
                    if (p.username == (string)username) {
                        response.ReturnCode = (short)ReturnCode.ExistUser;
                        peer.SendOperationResponse(response, sendParameters);       //发起响应
                        return;     // 不再往下执行
                    }
                }

                // 数据库查询
                UserManager userManager = new UserManager();
                bool isSuccess = userManager.VerifyUser((string)username, (string)password);
                MyGameServer.log.Info("账户密码验证：" + isSuccess);
                if (isSuccess)      // 如果登录成功，设置连接的用户名
                    peer.username = (string)username;

                // 响应
                if (isSuccess)
                    response.ReturnCode = (short)ReturnCode.Success;
                else
                    response.ReturnCode = (short)ReturnCode.Fail;
                peer.SendOperationResponse(response, sendParameters);       //发起响应
            }
        }
    }
}
