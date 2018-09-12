namespace Common {
    public enum OperationCode:byte {    // 区分请求操作类型
        Login,
        Register,
        SyncPosition,       // 同步位置
        SyncPlayer          // 同步玩家
    }
}
