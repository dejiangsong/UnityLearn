using System;
using System.Collections.Generic;
using System.IO;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using Photon.SocketServer;
using Common;
using MyGameServer.Handler;
using MyGameServer.Threads;

namespace MyGameServer {
    // 所有的Server端的主类都要继承ApplicationBase
    public class MyGameServer : ApplicationBase {

        // 将该类设为单例模式（便于其它地方访问，因为ApplicationBase中也有Instance，故使用new隐藏基类）
        public new static MyGameServer Instance {
            get;
            private set;
        }
        // 得到当前日志类对象
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();       // readonly：只能设置一次
        // handler的集合字典
        public Dictionary<OperationCode, HandlerBase> HandlerDict = new Dictionary<OperationCode, HandlerBase>();
        // 存储所有已登录的玩家
        public List<ClientPeer> PeerList = new List<ClientPeer>();

        // 使用线程去同步其它玩家位置
        SyncOthersPositionThread syncOthersPositionThread = new SyncOthersPositionThread();


        // 当一个客户端请求连接时执行
        // PeerBase是每个连接的客户端连接的对象，返回的peer会被Photon自动管理起来
        protected override PeerBase CreatePeer(InitRequest initRequest) {
            log.Info("1个客户端已连接");
            ClientPeer peer = new ClientPeer(initRequest);
            PeerList.Add(peer);     // 添加新的客户端连接到字典中
            return peer;
        }

        // 初始化
        protected override void Setup() {
            Instance = this;        // 设为单例模式

            initLog();      // 日志初始化
            initHandler();  // 初始化handler字典

            syncOthersPositionThread.Run();     // 启动同步其它玩家位置的线程
        }

        // Server端关闭的时候执行
        protected override void TearDown() {
            syncOthersPositionThread.Stop();        // 停止同步其它玩家位置的线程
            log.Info("Server stop!");
        }


        #region 其它的初始化
        /// <summary>
        /// 日志的初始化
        /// </summary>
        private void initLog() {
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");      // 配置log4net输出日志的目录：/Photon/deploy/log/，ApplicationPath：应用的根目录，这里是deploy部署根目录
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));     // BinaryPath：当前目录路径；Path.Combine：连接字符串，屏蔽掉平台差异
            if (configFileInfo.Exists) {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);     // 设置使用什么日志的类库实例
                XmlConfigurator.ConfigureAndWatch(configFileInfo);      // 让log4net这个类库读取配置文件
            }
            log.Info("Setup Completed!");       // 打印日志信息
        }

        /// <summary>
        /// handler字典的初始化
        /// </summary>
        private void initHandler() {
            // Temple：如下方式新建继承HandlerBase基类，并添加到HandlerDict字典中
            LoginHandler loginHandler = new LoginHandler();
            HandlerDict.Add(loginHandler.OpCode, loginHandler);     // 添加登录请求处理handler到字典中
            RegisterHandler registerHandler = new RegisterHandler();
            HandlerDict.Add(registerHandler.OpCode, registerHandler);
            SyncPositionHandler syncPositionHandler = new SyncPositionHandler();
            HandlerDict.Add(syncPositionHandler.OpCode, syncPositionHandler);
            SyncPlayerHandler syncPlayerHandler = new SyncPlayerHandler();
            HandlerDict.Add(syncPlayerHandler.OpCode, syncPlayerHandler);
        }
        #endregion
    }
}
