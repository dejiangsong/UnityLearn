using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Text;

namespace NHibernateToMySql.Utils {
    class NHibernateHelper {
        private static ISessionFactory _sessionFactory;
        private static ISessionFactory sessionFactory {
            get {
                if (_sessionFactory == null) {
                    var configuration = new Configuration();
                    configuration.Configure();
                    //configuration.AddAssembly("NHibernateToMySql");     // 指定程序集（若在配置文件中新增mapping指定过程序集，此段代码可省略）

                    _sessionFactory = configuration.BuildSessionFactory();      // 创建bhibernate和mysql会话的工厂
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession() {
            return sessionFactory.OpenSession();        // 打开一个跟数据库的会话
        }
    }
}

