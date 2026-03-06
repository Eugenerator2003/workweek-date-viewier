using System;
using System.Collections.Generic;
using System.Text;

namespace DateViewier.AndroidApp.Repositories
{
    public interface IOnlyLoadInstanceRepositoy<T>
    {
        T Load();
    }
}
