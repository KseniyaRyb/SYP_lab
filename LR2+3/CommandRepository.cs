using System;
using System.Collections.Generic;
using System.Linq;

namespace LR1
{
    public class CommandRepository : ICommandsRepository //Реализуем интерфейс
    {

        public DatabaseContext _botDatabaseContext; // Объявляем поле контекста БД.

        public CommandRepository(DatabaseContext context)
        {
            _botDatabaseContext = context; // ASP.NET сам найдет нужный нам класс и подставит его сюда, мы же положим в нужное поле
        }

        public void SaveCommand(CommandEntity commandEntity)
        {
            _botDatabaseContext.Add(commandEntity); // Добавляем сущность в БД
            _botDatabaseContext.SaveChanges(); // Сохраняем изменения
        }

        public void UpdateCommandEntity(CommandEntity commandEntity)
        {
            _botDatabaseContext.Update(commandEntity); // Изменяем сущность
            _botDatabaseContext.SaveChanges(); // Сохраняем изменения
        }

        public CommandEntity GetCommandById(int id)
        {
            return _botDatabaseContext.Find<CommandEntity>(id); // Получаем сущность по ИД
        }

        public void DeleteCommandEntity(int id)
        {
            _botDatabaseContext.Remove(GetCommandById(id)); //Удаляем сущность, найденную через метод GetCommandById по ИД
            _botDatabaseContext.SaveChanges(); //Сохраняем изменения в БД
        }

        public List<CommandEntity> GetAllCommands()
        {
            return _botDatabaseContext.CommandEntities.ToList(); // Берем все сущности из БД и собираем в список
        }

        public List<CommandEntity> FindByCommandBySourceName(string sourcename)
        {
            return _botDatabaseContext.CommandEntities // Берем все сущности из БД
                .Where(entity => entity.SourceNames.Contains(sourcename)) // Через LINQ стаивм условие выборки, все остальное сделает EntityFramework
                .ToList(); // Собираем все в список обратно
        }
    }
}
