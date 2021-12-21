using System;
using System.Collections.Generic;

namespace LR1
{
    public interface ICommandsRepository
    {
        public void SaveCommand(CommandEntity commandEntity); //Сохранение сущности команды
        public CommandEntity GetCommandById(int id); //Поиск сущности команд по ИД
        public List<CommandEntity> GetAllCommands(); //Получение всех сущеностей
        public void UpdateCommandEntity(CommandEntity commandEntity); //Обновление сущности команд
        public void DeleteCommandEntity(int id); //Удаление сущности по id
        public List<CommandEntity> FindByCommandBySourceName(string sourceName); //Поиск списка сущностей в соответствии с полем sourceName
    }
}
