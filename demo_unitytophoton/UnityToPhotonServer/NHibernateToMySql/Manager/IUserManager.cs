using NHibernateToMySql.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateToMySql.Manager {
    interface IUserManager {
        bool Add(User user);
        void Update(User user);
        void Remove(User user);
        User GetById(int id);
        User GetByName(string name);
        ICollection<User> GetAllUsers();
        bool VerifyUser(string name, string password);
    }
}
