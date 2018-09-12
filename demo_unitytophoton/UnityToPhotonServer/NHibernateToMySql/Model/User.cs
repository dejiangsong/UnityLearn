using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 映射类
/// 该数据库为demo，表为create table user(`id` int unsigned auto_increment,`name` varchar(30) not null unique,`password` varchar(30),`msg` varchar(100),primary key(`id`));
/// </summary>
namespace NHibernateToMySql.Model {
    public class User {
        /* 属性需要设置为虚属性 */
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual string Msg { get; set; }
    }
}
