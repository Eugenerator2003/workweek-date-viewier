using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp.Repositories
{
    public interface IInstanceRepistory<T>
    {
        T Load();

        void Save(T instance);
    }
}
