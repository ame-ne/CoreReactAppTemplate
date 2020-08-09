using System;

namespace CoreReactApp.Domain.Interfaces
{
    public interface IDbTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
