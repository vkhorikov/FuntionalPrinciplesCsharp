using CustomerManagement.Logic.Utils;

namespace CustomerManagement.Logic.Common
{
    public class Repository<T>
        where T : Entity
    {
        protected readonly UnitOfWork _unitOfWork;

        protected Repository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T GetById(long id)
        {
            return _unitOfWork.Get<T>(id);
        }

        public void Save(T entity)
        {
            _unitOfWork.SaveOrUpdate(entity);
        }
    }
}
