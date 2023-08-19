using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASP_Net_Core_example.Models
{
    public interface IUserInfoRepository
    {
        Task AddUserInfo(UserInfo userInfo);
    }

    public class UserInfoRepository : IUserInfoRepository
    {
        //для работы с БД нужна ссылка на контекст
        private readonly MyAppContext _appContext;

        public UserInfoRepository(MyAppContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// метод для добавления в БД новой сущности
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public async Task AddUserInfo(UserInfo userInfo)
        {
            //добавляем новую запись в таблицу
            var entry = _appContext.Entry(userInfo);
            if (entry.State == EntityState.Detached)
            {
                await _appContext.UserInfos.AddAsync(userInfo);

                await _appContext.SaveChangesAsync();
            }
        }
    }
}
