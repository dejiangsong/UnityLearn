namespace Common {
    public enum EventCode:byte {        // 区分事件操作类型
        NewPlayer,      // 新登录用户的事件
        SyncOthersPosition       // 同步其它用户的事件
    }
}
