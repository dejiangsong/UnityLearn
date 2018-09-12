namespace Common {
    public enum ParameterCode:byte {    // 区分传递的参数
        Username,   // 用户名
        Password,   // 密码
        X,          // 玩家自身位置
        Y,
        Z,
        UsernameList,    // 用户名列表
        PlayerDataList      // 玩家数据列表
    }
}
