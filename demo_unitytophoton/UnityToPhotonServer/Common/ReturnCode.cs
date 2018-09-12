using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common {
    public enum ReturnCode:short {      // 返回码
        Success,
        Fail,
        ExistUser       // 用户已登录
    }
}
