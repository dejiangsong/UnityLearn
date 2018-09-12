using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Tools {
    /// <summary>
    /// 用于方便的获取字典里的值，并做是否取到的判断
    /// </summary>
    public class DictTool {
        public static T2 GetValue<T1, T2>(Dictionary<T1,T2> dict,T1 key) {
            T2 value;
            bool isSuccess = dict.TryGetValue(key,out value);
            if (isSuccess) {
                return value;
            } else {
                return default(T2);     // 如果没取到值，返回对应变量类型的默认值
            }
        }
    }
}
