using Common;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyGameServer.Threads {
    class SyncOthersPositionThread {
        private Thread t;

        public void Run() {
            t = new Thread(updatePosition);
            t.IsBackground = true;
            t.Start();
        }

        public void Stop() {
            t.Abort();
        }

        private void updatePosition() {
            Thread.Sleep(3000);     // 等待3秒后开始

            while (true) {
                Thread.Sleep(200);      // 每次200毫秒同步一次
                sendPosition();
            }
        }

        private void sendPosition() {
            // 添加所有的已登录客户端的数据到玩家数据列表中
            List<PlayerData> playerDataList = new List<PlayerData>();
            foreach (var peer in MyGameServer.Instance.PeerList) {
                if (!string.IsNullOrEmpty(peer.username)) {
                    PlayerData playerData = new PlayerData();
                    playerData.Username = peer.username;
                    playerData.Position = new Vector3Data() { X = peer.x, Y = peer.y, Z = peer.z };
                    playerDataList.Add(playerData);
                }
            }

            // 序列化玩家数据列表
            StringWriter sw = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));
            serializer.Serialize(sw, playerDataList);
            sw.Close();
            string playerDataListString = sw.ToString();

            // 添加到字典中
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.PlayerDataList, playerDataListString);

            // 给每个客户端都发送每个客户端玩家的数据
            foreach (var peer in MyGameServer.Instance.PeerList) {
                if (!string.IsNullOrEmpty(peer.username)) {
                    EventData ed = new EventData((byte)EventCode.SyncOthersPosition);
                    ed.SetParameters(data);
                    peer.SendEvent(ed, new SendParameters());
                }
            }
        }
    }
}